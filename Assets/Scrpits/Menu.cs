using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public static Menu instance;
    private PlayerManager thePlayer;
    private FadeManager theFade;
    private CameraManager theCamera;
    private ChoiceManager theChoice;
    private DialogueManager theDM;
    public Inventory theInventory;
    public GameObject[] gos;
    public AudioManager theAudio;
    private BGMManager BGM;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        theInventory = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        BGM = FindObjectOfType<BGMManager>();
    }

    public GameObject go;
   

    public string call_sound;
    public string cancel_sound;

    public OrderManager theOrder;

    public bool activated;

    public void Exit()
    {
        {
            theAudio.Play(cancel_sound);
            Application.Quit();
        }
    }

    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theOrder.Move();
        theAudio.Play(cancel_sound);
    }

    public void GoToTitle()
    {
        theAudio.Play(cancel_sound);
        StartCoroutine(FadeCorutine());
        if (PlayerPrefs.GetInt("End") == 1)
        {
            thePlayer.BGM_Check = 13;
            BGM.Play(13);
        }
        else
        {
            thePlayer.BGM_Check = 1;
            BGM.Play(1);
        }
    }
    IEnumerator FadeCorutine()
    {
        theFade.FadeOut();
        for (int i = 0; i < gos.Length; i++)
        {
            Destroy(gos[i]);
        }
        go.SetActive(false);
        activated = false;
        yield return new WaitForSeconds(2f);
        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        thePlayer.GetComponent<SpriteRenderer>().color = color;
        thePlayer.transform.position = new Vector3(0, 0, 0);
        theCamera.transform.position = new Vector3(0, 0, -10);
        theFade.FadeIn();
        SceneManager.LoadScene("Title");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && theInventory.activated == false && SceneManager.GetActiveScene().name == "Start" 
            && theDM.talking == false && theChoice.choiceing == false && thePlayer.Event == false)
        {
            activated = !activated;

            if(activated)
            {
                theOrder.NotMove();
                go.SetActive(true);
                theAudio.Play(call_sound);
            }

            else
            {
                go.SetActive(false);
                theAudio.Play(cancel_sound);
                theOrder.Move();
            }
        }
    }
}
