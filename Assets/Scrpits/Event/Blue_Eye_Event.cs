using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Eye_Event : MonoBehaviour
{
    public AudioManager theAudio;
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public GameObject Eye;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    public bool Dialogue_flag;
    public bool Eye_flag;
    public bool flag = false;
    public bool flag1 = false;


    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void FixedUpdate()
    {
        if (flag == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (flag1 == false && collision.gameObject.name == "Player")
        {
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        flag1 = true;

        theOrder.PreLoadCharacter();

        if(!Dialogue_flag)
        {
            theOrder.NotMove();

            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);

            if (Eye_flag == true)
            {
                theAudio.Play("EyeSound");
                Eye.SetActive(true);
                yield return new WaitForSeconds(1f);
                Eye.SetActive(false);
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
            }
        }
        else
        {
            theAudio.Play("EyeSound");
            Eye.SetActive(true);
            yield return new WaitForSeconds(1f);
            Eye.SetActive(false);
        }

        theOrder.Move();

        flag = true;
    }
}
