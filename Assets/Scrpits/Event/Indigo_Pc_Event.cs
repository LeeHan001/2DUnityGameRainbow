using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Indigo_Pc_Event : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;

    private PcEventManager thePc;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private Inventory theInven;

    public Choice choice;
    private ChoiceManager theChoice;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        thePc = FindObjectOfType<PcEventManager>();
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    public void Update()
    {
        if (thePlayer.IndigoMap_Check == 1 && thePc.Button_Count == 10)
        {
            thePlayer.Pc_Check = false;

            thePlayer.IndigoMap_Check++;

            StartCoroutine(PcCoroutine());

            thePc.Advertise1.SetActive(true);

        }

        if (thePlayer.IndigoMap_Check == 2 && thePc.Button_Count == 50)
        {
            thePlayer.Pc_Check = false;

            thePlayer.IndigoMap_Check++;

            StartCoroutine(PcCoroutine());

            thePc.Advertise2.SetActive(true);
        }

        if (thePlayer.IndigoMap_Check == 3 && thePc.Button_Count == 100)
        {
            thePlayer.Pc_Check = false;

            thePlayer.IndigoMap_Check++;
            thePlayer.Quest_Check++;
            StartCoroutine(PcCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E) && theInven.activated == false)
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

        if(thePlayer.IndigoMap_Check == 0)
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);

            theOrder.Move();

            flag = true;
        }
        else if (thePlayer.IndigoMap_Check != 0 && thePlayer.IndigoMap_Check != 4 && thePlayer.Pc_Check)
        {
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);

                theOrder.Move();

                flag = true;

            }
            else
            {
                thePc.PC_On();
            }
        }

        else if(thePlayer.IndigoMap_Check == 4 || !thePlayer.Pc_Check)
        {
            theDM.ShowDialogue(dialogue_4);
            yield return new WaitUntil(() => !theDM.talking);

            theOrder.Move();

            flag = true;
        }
    }

    IEnumerator PcCoroutine()
    {
        thePc.PC_Off();

        theDM.ShowDialogue(dialogue_5);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

        flag = true;
    }
}
