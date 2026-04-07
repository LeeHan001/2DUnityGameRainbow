using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Event : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1

    private bool flag = true;

    public int Quest_Check;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
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

        if (thePlayer.Quest_Check < Quest_Check)
        {
            theDM.ShowDialogue(dialogue_1);
            thePlayer.Quest_Check++;
        }
        else
            theDM.ShowDialogue(dialogue_2);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

        flag = true;
    }
}
