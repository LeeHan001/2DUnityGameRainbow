using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violet_Npc_Event : MonoBehaviour
{
    public Dialogue dialogue_0;
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;

    public int itemID;
    public int _count;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private Inventory theInven;
    private BGMManager BGM;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    public GameObject gameObject_True;
    public GameObject gameObject_False;

    public GameObject BGM_True;
    public GameObject BGM_False;
    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        BGM = FindObjectOfType<BGMManager>();
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
                            StartCoroutine(TransferCoroutine());
                        }
                        break;
                    case "DOWN":
                        if (vector.y == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(TransferCoroutine());
                        }
                        break;
                    case "RIGHT":
                        if (vector.x == 1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(TransferCoroutine());
                        }
                        break;
                    case "LEFT":
                        if (vector.x == -1f && flag == true)
                        {
                            flag = false;
                            StartCoroutine(TransferCoroutine());
                        }
                        break;
                    default:
                        //StartCoroutine(EventCoroutine());
                        break;
                }
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();

        if (thePlayer.Quest_Check == 7)
        {
            theDM.ShowDialogue(dialogue_0);

            yield return new WaitUntil(() => !theDM.talking);

            thePlayer.Quest_Check++;
        }
        else if (thePlayer.Quest_Check == 8 && thePlayer.item_Check == 7)
        {
            theDM.ShowDialogue(dialogue_1);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else if (thePlayer.Quest_Check == 8 && thePlayer.item_Check == 8)
        {
            BGM_False.SetActive(false);
            BGM.Stop();

            theDM.ShowDialogue(dialogue_2);

            yield return new WaitUntil(() => !theDM.talking);
            Inventory.instance.GetAnItem(itemID, _count);
            thePlayer.Quest_Check++;
            gameObject_True.SetActive(true);
            gameObject_False.SetActive(false);

            BGM_True.SetActive(true);
        }
        else
        {
            theDM.ShowDialogue(dialogue_3);

            yield return new WaitUntil(() => !theDM.talking);
        }
        theOrder.Move();

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
