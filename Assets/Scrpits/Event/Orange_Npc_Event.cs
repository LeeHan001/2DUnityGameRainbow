using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange_Npc_Event : MonoBehaviour
{
    public Dialogue dialogue_0;
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
    public Dialogue dialogue_7;
    public Dialogue dialogue_8;

    private Inventory theInven;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    public Choice choice_1;
    public Choice choice_2;
    public Choice choice_3;
    public Choice choice_4;
    private ChoiceManager theChoice;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;


    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E)) && theInven.activated == false)
            {
                vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                switch (direction)
                {
                    case "UP":
                        if (vector.y == 1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(EventCoroutine());
                        }
                        break;
                    case "DOWN":
                        if (vector.y == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(EventCoroutine());
                        }
                        break;
                    case "RIGHT":
                        if (vector.x == 1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(EventCoroutine());
                        }
                        break;
                    case "LEFT":
                        if (vector.x == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(EventCoroutine());
                        }
                        break;
                    default:
                        //StartCoroutine(EventCoroutine());
                        break;
                }
            }
        }
    }

    IEnumerator EventCoroutine()
    {
        yield return new WaitForSeconds(0.01f);

        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        if (thePlayer.Car_item == 2)
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice_1);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_2);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                thePlayer.Car_item = 1;
            }
        }
        else if (thePlayer.Candy_item == 2)
        {
            theDM.ShowDialogue(dialogue_4);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice_2);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_2);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                thePlayer.Candy_item = 1;
            }
        }
        else if (thePlayer.Dool_item == 2)
        {
            theDM.ShowDialogue(dialogue_5);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice_3);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_2);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                thePlayer.Dool_item = 1;
            }
        }
        else if (thePlayer.item_Check == 3 && thePlayer.Quest_Check == 2)
        {
            theDM.ShowDialogue(dialogue_6);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice_4);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_2);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                theDM.ShowDialogue(dialogue_7);
                yield return new WaitUntil(() => !theDM.talking);
                thePlayer.Quest_Check++;
            }
        }
        else if (thePlayer.item_Check == 3 && thePlayer.Quest_Check == 3)
        {
            theDM.ShowDialogue(dialogue_8);
            yield return new WaitUntil(() => !theDM.talking);
        }
        else
        {
            theDM.ShowDialogue(dialogue_0);
            yield return new WaitUntil(() => !theDM.talking);
        }

        theOrder.Move();

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
