using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Quiz_Event : MonoBehaviour
{
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
    public Dialogue dialogue_10;
    public Dialogue dialogue_11;

    private Green_Face_Change Face_Change;
    private Inventory theInven;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private FadeManager theFade;

    public int answer_check;
    public Choice choice_0;
    public Choice choice_1;
    public Choice choice_2;
    public Choice choice_3;
    public Choice choice_4;
    public Choice choice_5;
    private ChoiceManager theChoice;

    public Animator anim;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    public BGMManager BGM;
    public AudioManager theAudio;

    public string Good;
    public string Soso;
    public string Bad;


    void Start()
    {
        Face_Change = FindObjectOfType<Green_Face_Change>();
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theFade = FindObjectOfType<FadeManager>();
        BGM = FindObjectOfType<BGMManager>();
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
        anim.SetBool("Out_Bool", false);

        answer_check = 0;

        yield return new WaitForSeconds(0.01f);

        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        if (thePlayer.Quiz_Check == 0)
        {
            theDM.ShowDialogue(dialogue_0);
            yield return new WaitUntil(() => !theDM.talking);
        }
        else if ((thePlayer.Quiz_Check == 1 || thePlayer.Quiz_Check == 2) && thePlayer.Quest_Check == 4)
        {
            Face_Change.Blank_Face();
            anim.SetTrigger("Come");
            anim.SetBool("NotMove", true);
            yield return new WaitForSeconds(1f);
            if (thePlayer.Quiz_Check == 1)
            {
                thePlayer.Quiz_Check++;
                theDM.ShowDialogue(dialogue_1);
                yield return new WaitUntil(() => !theDM.talking);
            }
            else
            {
                theDM.ShowDialogue(dialogue_2);
                yield return new WaitUntil(() => !theDM.talking);
            }
            theChoice.ShowChoice(choice_0);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                anim.SetBool("NotMove", false);
            }
            else
            {
                BGM.Play(8);
                answer_check++;
                theDM.ShowDialogue(dialogue_4);
                yield return new WaitUntil(() => !theDM.talking);

                anim.SetBool("Idel", true);
                theChoice.ShowChoice(choice_1);
                yield return new WaitUntil(() => !theChoice.choiceing);
                if (theChoice.GetResult() == 0)
                {
                    answer_check++;
                    theDM.ShowDialogue(dialogue_5);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                else
                {
                    theDM.ShowDialogue(dialogue_6);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                theChoice.ShowChoice(choice_2);
                yield return new WaitUntil(() => !theChoice.choiceing);
                if (theChoice.GetResult() == 2)
                {
                    answer_check++;
                    theDM.ShowDialogue(dialogue_5);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                else
                {
                    theDM.ShowDialogue(dialogue_6);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                theChoice.ShowChoice(choice_3);
                yield return new WaitUntil(() => !theChoice.choiceing);
                if (theChoice.GetResult() == 3)
                {
                    answer_check++;
                    theDM.ShowDialogue(dialogue_5);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                else
                {
                    theDM.ShowDialogue(dialogue_6);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                theChoice.ShowChoice(choice_4);
                yield return new WaitUntil(() => !theChoice.choiceing);
                if (theChoice.GetResult() == 4)
                {
                    answer_check++;
                    theDM.ShowDialogue(dialogue_5);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                else
                {
                    theDM.ShowDialogue(dialogue_6);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                theChoice.ShowChoice(choice_5);
                yield return new WaitUntil(() => !theChoice.choiceing);
                if (theChoice.GetResult() == 3)
                {
                    answer_check++;
                    theDM.ShowDialogue(dialogue_5);
                    yield return new WaitUntil(() => !theDM.talking);
                }
                else
                {
                    theDM.ShowDialogue(dialogue_6);
                    yield return new WaitUntil(() => !theDM.talking);
                }
            }

            if (answer_check == 6)
            {
                BGM.Stop();
                theAudio.Play(Good);
                thePlayer.Quest_Check++;
                anim.SetBool("Idel", false);
                theFade.FalshOut();
                yield return new WaitForSeconds(0.5f);
                theFade.FalshIn();
                Face_Change.Laugh_Face();
                theDM.ShowDialogue(dialogue_7);
                yield return new WaitUntil(() => !theDM.talking);
                BGM.Play(7);
            }
            else if (answer_check == 5 || answer_check == 4)
            {
                BGM.Stop();
                theAudio.Play(Soso);
                anim.SetBool("Idel", false);
                theFade.FalshOut();
                yield return new WaitForSeconds(0.5f);
                theFade.FalshIn();
                Face_Change.Smile_Face();
                theDM.ShowDialogue(dialogue_8);
                yield return new WaitUntil(() => !theDM.talking);
                BGM.Play(7);
            }
            else if (answer_check == 3 || answer_check == 2)
            {
                BGM.Stop();
                theAudio.Play(Soso);
                anim.SetBool("Idel", false);
                theFade.FalshOut();
                yield return new WaitForSeconds(0.5f);
                theFade.FalshIn();
                theDM.ShowDialogue(dialogue_9);
                yield return new WaitUntil(() => !theDM.talking);
                BGM.Play(7);
            }
            else if (answer_check == 1)
            {
                BGM.Stop();
                theAudio.Play(Bad);
                anim.SetBool("Angry", true);
                theFade.FalshOut();
                yield return new WaitForSeconds(0.5f);
                theFade.FalshIn();
                Face_Change.Angry_Face();
                theDM.ShowDialogue(dialogue_10);
                yield return new WaitUntil(() => !theDM.talking);
                BGM.Play(7);
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_11);
            yield return new WaitUntil(() => !theDM.talking);
        }
        theOrder.Move();

        flag = true;
        anim.SetBool("Out_Bool", true);
        anim.SetTrigger("Out");
        anim.SetBool("Idel", false);
        anim.SetBool("Angry", false);
    }
}
