using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    public Choice choice;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private SaveNLoad theSaveNLoad;
    private ChoiceManager theChoice;
    private Inventory theInven;

    public int Check;

    bool flag = false;


    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E)) && theInven.activated == false)
            {
                if (thePlayer.Save_Check == 0 && !flag)
                {
                    flag = true;
                    StartCoroutine(FristSave());
                    thePlayer.Save_Check++;
                }
                else if (thePlayer.Save_Check >= 1 && !flag)
                {
                    flag = true;
                    StartCoroutine(CoroutineSave());
                }
            }

        }
    }

    IEnumerator FristSave()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choiceing);

        if(theChoice.GetResult() == 1)
        {
            theSaveNLoad.CallSave();

            theDM.ShowDialogue(dialogue_2);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else
        {
            theDM.ShowDialogue(dialogue_3);

            yield return new WaitUntil(() => !theDM.talking);
        }

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theOrder.Move();

        flag = false;
    }

    IEnumerator CoroutineSave()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choiceing);

        if (theChoice.GetResult() == 1)
        {
            theSaveNLoad.CallSave();

            theDM.ShowDialogue(dialogue_2);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else
        {
            theDM.ShowDialogue(dialogue_3);

            yield return new WaitUntil(() => !theDM.talking);
        }
        yield return new WaitForSeconds(0.1f);

        theOrder.Move();

        yield return new WaitForSeconds(0.1f);

        flag = false;
    }
}
