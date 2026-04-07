using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Event : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public GameObject Black;
    public GameObject Ending;

    public GameObject Eye0;
    public GameObject Eye1;
    public GameObject Eye2;
    public GameObject Stone1;
    public GameObject Stone2;
    public Animator anim;
    public Animator anim2;
    public Animator anim3;
    public GameObject Tree;
    public GameObject Rope_Tree;

    public Dialogue dialogue_0;
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
    public Dialogue dialogue_7;
    public Dialogue dialogue_8;
    public Dialogue dialogue_9;

    public Choice choice;
    private Menu theMenu;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private CameraManager theCamera;
    private FadeManager theFade;
    private PlayerManager thePlayer;//"DirY" == 1
    private ChoiceManager theChoice;
    private Inventory theInven;
    private AudioManager theAudio;
    private BGMManager BGM;

    public int Check;

    public string WoodSound;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
        theInven = FindObjectOfType<Inventory>();
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theAudio = FindObjectOfType<AudioManager>();
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

        if (thePlayer.Quest_Check == Check)
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

                theFade.FadeOut();
                yield return new WaitForSeconds(0.5f);

                theOrder.SetTransparent("player");

                theAudio.Play(WoodSound);
                Tree.SetActive(false);
                Rope_Tree.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                theOrder.SetUnTransparent("player");

                theCamera.transform.position = new Vector3(target1.transform.position.x, target1.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = target1.transform.position;
                theOrder.Move("Player", "UP");

                theFade.FadeIn();
                yield return new WaitForSeconds(0.5f);


                theDM.ShowDialogue(dialogue_3);

                yield return new WaitUntil(() => !theDM.talking);

                theFade.FadeOut();
                yield return new WaitForSeconds(0.5f);
                Black.SetActive(true);
                theFade.FadeIn();
                yield return new WaitForSeconds(0.5f);

                theOrder.SetTransparent("player");

                yield return new WaitForSeconds(1f);
                theOrder.SetUnTransparent("player");

                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);

                theCamera.transform.position = new Vector3(target3.transform.position.x, target3.transform.position.y, theCamera.transform.position.z);
                theCamera.CameraMove = false;
                thePlayer.transform.position = target2.transform.position;

                BGM.Play(13);
                BGM.FadeInMusic();
                yield return new WaitForSeconds(2f);


                theFade.FadeOut();
                yield return new WaitForSeconds(0.5f);
                Eye0.SetActive(true);
                Eye1.SetActive(true);
                theFade.FadeIn();
                yield return new WaitForSeconds(0.5f);
                anim2.SetTrigger("Open");
                yield return new WaitForSeconds(2f);
                anim.SetTrigger("Open");
                yield return new WaitForSeconds(5f);
                Eye2.SetActive(true);
                Black.SetActive(false);
                yield return new WaitForSeconds(10f);

                Eye0.SetActive(false);
                Eye1.SetActive(false);
                Eye2.SetActive(false);

                yield return new WaitForSeconds(5f);

                theDM.ShowDialogue(dialogue_5);
                yield return new WaitUntil(() => !theDM.talking);

                Stone1.SetActive(true);
                Stone2.SetActive(true);
                yield return new WaitForSeconds(2f);

                theDM.ShowDialogue(dialogue_6);
                yield return new WaitUntil(() => !theDM.talking);

                anim3.SetTrigger("Stone_On");
                yield return new WaitForSeconds(5f);

                theDM.ShowDialogue(dialogue_7);
                yield return new WaitUntil(() => !theDM.talking);
                
                Stone1.SetActive(false);
                Stone2.SetActive(false);

                theFade.FadeOut();
                yield return new WaitForSeconds(3f);
                Black.SetActive(true);
                theFade.FadeIn();

                theDM.ShowDialogue(dialogue_8);
                yield return new WaitUntil(() => !theDM.talking);

                theDM.ShowDialogue(dialogue_9);
                yield return new WaitUntil(() => !theDM.talking);


                theFade.FadeOut();
                yield return new WaitForSeconds(3f);
                Black.SetActive(false);
                Ending.SetActive(true);
                theFade.FadeIn();

                yield return new WaitForSeconds(13f);

                theMenu.GoToTitle();

                PlayerPrefs.SetInt("End", 1);
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_0);

            yield return new WaitUntil(() => !theDM.talking);

            theOrder.Move();
        }

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
