using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange_NPC_Item_Event : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private Inventory theInven;

    public int itemID;
    public int _count;

    public Choice choice;
    private ChoiceManager theChoice;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
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

        if (thePlayer.Car_item == 1 && thePlayer.Candy_item == 1 && thePlayer.Dool_item == 1 && thePlayer.item_Check < 3)
        {
            theDM.ShowDialogue(dialogue_2);

            yield return new WaitUntil(() => !theDM.talking);

            
            theChoice.ShowChoice(choice);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_3);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                thePlayer.item_Check++;
                Inventory.instance.GetAnItem(itemID, _count);
                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);
            }
        
        }
        else if (thePlayer.Car_item == 1 && thePlayer.Candy_item == 1 && thePlayer.Dool_item == 1 && thePlayer.item_Check == 3)
        {
            theDM.ShowDialogue(dialogue_5);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);

            yield return new WaitUntil(() => !theDM.talking);
        }


        theOrder.Move();

        flag = true;
    }
}
