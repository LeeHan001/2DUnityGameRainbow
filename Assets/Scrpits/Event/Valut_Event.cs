using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valut_Event : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private FadeManager theFade;

    private NumberSystem theNumber;
    public int correctNumber;

    public string direction;
    private Vector2 vector;

    public GameObject Valut;
    public GameObject Milk;

    private bool flag = true;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theNumber = FindObjectOfType<NumberSystem>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
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
                            StartCoroutine(ValutCoroutine());
                        }
                        break;
                    case "DOWN":
                        if (vector.y == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(ValutCoroutine());
                        }
                        break;
                    case "RIGHT":
                        if (vector.x == 1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(ValutCoroutine());
                        }
                        break;
                    case "LEFT":
                        if (vector.x == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(ValutCoroutine());
                        }
                        break;
                    default:
                        //StartCoroutine(EventCoroutine());
                        break;
                }
            }
        }
    }
    IEnumerator ValutCoroutine()
    {
        thePlayer.Event = true;
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue1);
        yield return new WaitUntil(() => !theDM.talking);

        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);

        if(theNumber.GetResult())
        {
            thePlayer.Quest_Check = 2;
            theFade.FalshOut();
            Valut.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Milk.SetActive(true);
            theFade.FalshIn();
            theDM.ShowDialogue(dialogue2);
            yield return new WaitUntil(() => !theDM.talking);
            this.gameObject.SetActive(false);
        }

        else
        {
            if(thePlayer.RedMap_Quest_Check >= 7)
            {
                theDM.ShowDialogue(dialogue4);
            }
            else
            {
                theDM.ShowDialogue(dialogue3);
                thePlayer.RedMap_Quest_Check++;
            }
            yield return new WaitUntil(() => !theDM.talking);
        }

        theOrder.Move();

        flag = true;
        thePlayer.Event = false;
    }
}
