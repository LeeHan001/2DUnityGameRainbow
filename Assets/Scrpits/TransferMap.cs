using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    [Tooltip("문이 열린다 : true , 문이 없으면 false")]
    public bool door;
    private bool flag = true;

    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;
    private DialogueManager theDM;
    public Choice choice;
    private ChoiceManager theChoice;
    private Inventory theInven;
    private AudioManager theAudio;

    public string DoorOpenSound;

    void Start()
    {
        theInven = FindObjectOfType<Inventory>();
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!door)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (door)
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
    }
    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();

        if(thePlayer.Quest_Check >= 9)
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
                if (door)
                {
                    theAudio.Play(DoorOpenSound);
                    anim_1.SetBool("Open", true);
                    if (door_count == 2)
                        anim_2.SetBool("Open", true);
                }
                yield return new WaitForSeconds(1f);
                theFade.FadeOut();
                theOrder.SetTransparent("player");
                if (door)
                {
                    anim_1.SetBool("Open", false);
                    if (door_count == 2)
                        anim_2.SetBool("Open", false);
                }
                yield return new WaitForSeconds(0.5f);
                theOrder.SetUnTransparent("player");

                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = target.transform.position;
                theFade.FadeIn();
                yield return new WaitForSeconds(0.5f);

                theDM.ShowDialogue(dialogue_3);

                yield return new WaitUntil(() => !theDM.talking);
            }
        }
      
        theOrder.Move();

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
