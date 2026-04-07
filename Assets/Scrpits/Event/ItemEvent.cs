using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent : MonoBehaviour
{
    public GameObject Car_Item;
    public GameObject Candy_Item;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    public Choice choice;
    private ChoiceManager theChoice;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    private bool flag = true;
    public int check;

    [Tooltip("UP, DOWN, LEFT, RIGHT")]
    public string direction;
    private Vector2 vector;

    void Awake()
    {
        theChoice = FindObjectOfType<ChoiceManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    void Start()
    {
        if(thePlayer.Car_item != 0)
        {
            Car_Item.SetActive(false);
        }
        if(thePlayer.Candy_item != 0)
        {
            Candy_Item.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E))
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

        if (thePlayer.Dool_item != 0 && check == 2)
        {
            theDM.ShowDialogue(dialogue_4);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);

            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice);
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

                if (check == 0)
                {
                    thePlayer.Car_item = 2;
                    Car_Item.SetActive(false);
                }
                else if (check == 1)
                {
                    thePlayer.Candy_item = 2;
                    Candy_Item.SetActive(false);
                }
                else if (check == 2)
                {
                    thePlayer.Dool_item = 2;
                }
            }
        }

        theOrder.Move();

        flag = true;
    }
}
