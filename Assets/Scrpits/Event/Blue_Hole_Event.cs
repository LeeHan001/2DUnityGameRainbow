using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Hole_Event : MonoBehaviour
{
    public Transform target;
    public GameObject Light;
    public GameObject BackGround;
    public Dialogue dialogue_0;
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
    private Inventory theInven;

    private AudioManager theAudio;

    public string TransformSound;

    public int Check;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theAudio = FindObjectOfType<AudioManager>();
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

        if(thePlayer.BlueMap_Check == 0)
        {
            theDM.ShowDialogue(dialogue_0);

            yield return new WaitUntil(() => !theDM.talking);
        }
        else if (thePlayer.BlueMap_Check == 1 && thePlayer.item_Check == 5)
        {
            theDM.ShowDialogue(dialogue_1);

            yield return new WaitUntil(() => !theDM.talking);

            theChoice.ShowChoice(choice);
            yield return new WaitUntil(() => !theChoice.choiceing);

            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_4);

                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                BackGround.SetActive(true);

                theFade.FadeOut();
                yield return new WaitForSeconds(0.5f);

                theOrder.SetTransparent("player");

                yield return new WaitForSeconds(0.5f);
                theOrder.SetUnTransparent("player");

                theAudio.Play(TransformSound);
                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = target.transform.position;
                theFade.FadeIn();

                yield return new WaitForSeconds(2f);

                theDM.ShowDialogue(dialogue_2);

                yield return new WaitUntil(() => !theDM.talking);

                theAudio.Play("EnterSound2");
                Light.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                
                BackGround.SetActive(false);
                theDM.ShowDialogue(dialogue_3);

                yield return new WaitUntil(() => !theDM.talking);
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_5);

            yield return new WaitUntil(() => !theDM.talking);
        }
        theOrder.Move();

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
