using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    public bool item;

    public int itemcheck;
    public int questcheck;

    public string Dir;

    bool flag = false;


    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();

        if(item == true)
        {
            if (thePlayer.item_Check >= itemcheck)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (thePlayer.Quest_Check >= questcheck)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (item == true)
        {
            if (thePlayer.item_Check >= itemcheck)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (thePlayer.Quest_Check >= questcheck)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (item == true)
        {
            if (thePlayer.item_Check != itemcheck && !flag && collision.gameObject.name == "Player")
            {
                flag = true;
                StartCoroutine(EventCoroutine());
            }
        }
        else
        {
            if (thePlayer.Quest_Check != questcheck && !flag && collision.gameObject.name == "Player")
            {
                flag = true;
                StartCoroutine(EventCoroutine());
            }
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        if(Dir == "DOWN")
        {
            theOrder.Move("Player", "DOWN");
            theOrder.Move("Player", "DOWN");
        }
        else if(Dir == "UP")
        {
            theOrder.Move("Player", "UP");
            theOrder.Move("Player", "UP");
        }
        else if (Dir == "RIGHT")
        {
            theOrder.Move("Player", "RIGHT");
            theOrder.Move("Player", "RIGHT");
        }
        else if (Dir == "LEFT")
        {
            theOrder.Move("Player", "LEFT");
            theOrder.Move("Player", "LEFT");
        }

        yield return new WaitForSeconds(0.5f);

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theOrder.Move();

        flag = false;
    }
}
