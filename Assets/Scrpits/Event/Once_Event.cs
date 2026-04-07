using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Once_Event : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    public int Once_Check;

    bool flag = false;


    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();

        if (thePlayer.Once_Check > Once_Check)
        {
                Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (thePlayer.Once_Check > Once_Check)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (thePlayer.Once_Check == Once_Check && !flag && collision.gameObject.name == "Player")
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
        
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        thePlayer.Once_Check++;

        theOrder.Move();

        flag = false;
    }
}
