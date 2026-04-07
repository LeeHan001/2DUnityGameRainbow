using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private Inventory theInven;

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
                    case "":
                        if (flag == true)
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

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        yield return new WaitForSeconds(0.25f);

        theOrder.Move();

        yield return new WaitForSeconds(0.1f);

        flag = true;
    }
}
