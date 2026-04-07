using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private AudioManager theAudio;

    public GameObject EyeEvent;

    private bool flag = false;

    public int QuestCheck;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();

        if (thePlayer.Quest_Check > QuestCheck)
        {
            EyeEvent.SetActive(false);
        }

        if (thePlayer.Quest_Check > QuestCheck)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
            thePlayer.Quest_Check++;
        }
    }

    IEnumerator EventCoroutine()
    {
        thePlayer.Event = true;

        theAudio.Play("TransformSound");
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        yield return new WaitForSeconds(4f);

        EyeEvent.SetActive(false);

        yield return new WaitForSeconds(2f);

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);


        theOrder.Move("Player", "DOWN");
        theOrder.Move("Player", "DOWN");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");

        yield return new WaitForSeconds(0.1f);

        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "DOWN");
        theOrder.Move("Player", "DOWN");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_2);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

        thePlayer.Event = false;
    }
}
