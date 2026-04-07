using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay_Event : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private Inventory theInven;
    private GameManager theGM;

    public Choice choice;
    private FadeManager theFade;
    private ChoiceManager theChoice;

    private bool flag = true;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theGM = FindObjectOfType<GameManager>();
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
                        if (flag == true)
                        {
                            flag = false;
                            StartCoroutine(EventCoroutine());
                        }
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

        if(thePlayer.Quest_Check == 4)
        {
            theDM.ShowDialogue(dialogue_4);
            yield return new WaitUntil(() => !theDM.talking);
            theChoice.ShowChoice(choice);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                theOrder.Move();
            }
            else
            {
                theOrder.NotMove();
                theFade.FadeOut();
                yield return new WaitForSeconds(2f);

                thePlayer.currentMapName = "FlyingGame";
                thePlayer.transform.position = new Vector3(0, 0, 0);
                theGM.LoadStart();
                SceneManager.LoadScene("FlyingGame");
            }
        }
        else if (thePlayer.GamePlay_Check == 0)
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            theOrder.Move();
        }
        else if (thePlayer.GamePlay_Check == 1)
        {
            theDM.ShowDialogue(dialogue_2);
            yield return new WaitUntil(() => !theDM.talking);
            theChoice.ShowChoice(choice);
            yield return new WaitUntil(() => !theChoice.choiceing);
            if (theChoice.GetResult() == 0)
            {
                theDM.ShowDialogue(dialogue_3);
                yield return new WaitUntil(() => !theDM.talking);
                theOrder.Move();
            }
            else
            {
                theOrder.NotMove();
                theFade.FadeOut();
                //theAudio.Play(click_sound);
                yield return new WaitForSeconds(2f);

                thePlayer.currentMapName = "FlyingGame";
                thePlayer.transform.position = new Vector3(0, 0, 0);
                theGM.LoadStart();
                SceneManager.LoadScene("FlyingGame");
            }
        }
        else
        {
            theDM.ShowDialogue(dialogue_1);
            yield return new WaitUntil(() => !theDM.talking);
            theOrder.Move();
        }

        yield return new WaitForSeconds(0.15f);

        flag = true;
    }
}
