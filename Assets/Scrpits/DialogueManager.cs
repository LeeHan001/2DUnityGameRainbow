using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    #region Singleton
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton
    //public TextMesh text;
    public TextMeshProUGUI text;
    //public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialougeWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialougeWindow;

    private int count; // 대화 진행

    public Animator animSprite;
    public Animator animDialougeWindow;

    public string typeSound;
    public string enterSound;

    private AudioManager theAudio;

    public bool talking = false;
    private bool KeyActivated = false;
    private bool onlyText = false;

    void Start()
    {
        //text = GetComponent<TextMeshProUGUI>();
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialougeWindow = new List<Sprite>();
        //theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void Showtext(string[] _sentences)
    {
        onlyText = true;
        talking = true;

        //theOrder.NotMove();

        for (int i = 0; i < _sentences.Length; i++)
        {
            listSentences.Add(_sentences[i]);
        }
        StartCoroutine(StartTextcoroutine());
    }
    public void ShowDialogue(Dialogue dialogue)
    {
        onlyText = false;
        talking = true;

        //theOrder.NotMove();

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialougeWindow.Add(dialogue.dialogueWindows[i]);
        }

        animSprite.SetBool("Appear", true);
        animDialougeWindow.SetBool("Appear", true);
        StartCoroutine(StartDialoguecoroutine());
    }

    public void ExitDialogue()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprites.Clear();
        listDialougeWindow.Clear();
        animSprite.SetBool("Appear", false);
        animDialougeWindow.SetBool("Appear", false);
        talking = false;
        //theOrder.Move();
    }

    IEnumerator StartDialoguecoroutine()
    {
        if(count > 0)
        {
            if (listDialougeWindow[count] != listDialougeWindow[count - 1])
            {
                animSprite.SetBool("Change", true);
                animDialougeWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialougeWindow.GetComponent<SpriteRenderer>().sprite = listDialougeWindow[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialougeWindow.SetBool("Appear", true);
                animSprite.SetBool("Change", false);
            }
            else
            {
                if (listSprites[count] != listSprites[count - 1])
                {
                    animSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    animSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }

            }
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            rendererDialougeWindow.GetComponent<SpriteRenderer>().sprite = listDialougeWindow[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }
        KeyActivated = true;

        for(int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];
            if (i % 7 == 1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator StartTextcoroutine()
    {
        KeyActivated = true;

        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];
            yield return new WaitForSeconds(0.01f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(talking && KeyActivated)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))
            {
                KeyActivated = false;
                count++;
                text.text = "";
                theAudio.Play(enterSound);
                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    if(onlyText)
                        StartCoroutine(StartTextcoroutine());
                    else
                        StartCoroutine(StartDialoguecoroutine());
                }
            }
        }
    }
}
