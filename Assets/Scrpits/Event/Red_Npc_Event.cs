using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Npc_Event : MonoBehaviour
{
    public Transform target;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;

    public Choice choice;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private CameraManager theCamera;
    private FadeManager theFade;
    private PlayerManager thePlayer;//"DirY" == 1
    private ChoiceManager theChoice;

    public int Check;
    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
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

        if (thePlayer.item_Check < Check)
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
        }

        else
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
                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);

                theFade.FadeOut();
                yield return new WaitForSeconds(0.5f);

                theOrder.SetTransparent("player");

                yield return new WaitForSeconds(0.5f);
                theOrder.SetUnTransparent("player");

                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = target.transform.position;
                theFade.FadeIn();
                yield return new WaitForSeconds(0.5f);

                theDM.ShowDialogue(dialogue_5);

                yield return new WaitUntil(() => !theDM.talking);
            }

        }

        theOrder.Move();

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
