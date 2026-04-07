using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    private DatabaseManager theDatabase;

    public GameObject StartMenu;
    public GameObject EndMenu;

    public GameObject Sub;
    public GameObject TitleButton;
    private FadeManager theFade;
    private AudioManager theAudio;
    private BGMManager BGM;

    public string click_sound;
    public string start_sound;

    private CameraManager theCamera;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private GameManager theGM;
    private SaveNLoad theSaveNLoad;
    private Inventory theInven; 
    void Start()
    {
        //PlayerPrefs.SetInt("End", 0);
        //PlayerPrefs.SetInt("Save", 0);

        if (TitleButton.activeSelf == false)
        {
            TitleButton.SetActive(true);
        }
        theCamera = FindObjectOfType<CameraManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theAudio = FindObjectOfType<AudioManager>();
        BGM = FindObjectOfType<BGMManager>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theGM = FindObjectOfType<GameManager>();
        theInven = FindObjectOfType<Inventory>();

        if (PlayerPrefs.GetInt("End") == 1)
        {
            StartMenu.SetActive(false);
            EndMenu.SetActive(true);
            BGM.Play(13);
            BGM.FadeInMusic();
        }
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCorutine());
    }

    IEnumerator GameStartCorutine()
    {
        theInven.Clear();
        BGM.Stop();
        thePlayer.Quest_Check = 0;
        thePlayer.item_Check = 0;
        thePlayer.Save_Check = 0;

        thePlayer.Event = false;

        thePlayer.BGM_Check = 0;
        thePlayer.Once_Check = 0;
        thePlayer.RedMap_Quest_Check = 0;
        thePlayer.GamePlay_Check = 0;
        thePlayer.Quiz_Check = 0;
        thePlayer.BlueMap_Check = 0;
        thePlayer.IndigoMap_Check = 0;
        thePlayer.VioletMap_Check = 0;

        thePlayer.Car_item = 0;
        thePlayer.Candy_item = 0;
        thePlayer.Dool_item = 0;
        thePlayer._Check = 0;

        theOrder.NotMove();
        TitleButton.SetActive(false);
        theFade.FadeOut();
        theAudio.Play(click_sound);
        yield return new WaitForSeconds(0.5f);
        theAudio.Play(start_sound);
        yield return new WaitForSeconds(2f);

        Sub.SetActive(true);
        theFade.FadeIn();
        yield return new WaitForSeconds(8f);
        theFade.FadeOut();
        yield return new WaitForSeconds(2f);


        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        thePlayer.GetComponent<SpriteRenderer>().color = color;
        thePlayer.currentMapName = "Start";
        thePlayer.transform.position = new Vector3(0.5f, -0.5f, 0);
        theCamera.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, theCamera.transform.position.z);
        theGM.LoadStart();
        SceneManager.LoadScene("Start");

        Sub.SetActive(false);
    }

    public void ExitGame()
    {
        theAudio.Play(click_sound);
        Application.Quit();
    }

    public void LoadGame()
    {
       if(PlayerPrefs.GetInt("Save") != 1)
       {
            theAudio.Play(click_sound);
            return;
       }
       else
       {
            StartCoroutine(LoadCorutine());
       }
        
    }

    IEnumerator LoadCorutine()
    {
        theAudio.Play(click_sound);
        BGM.Stop();
        theOrder.NotMove();
        TitleButton.SetActive(false);
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        theSaveNLoad.CallLoad();
        theCamera.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, theCamera.transform.position.z);
        yield return new WaitForSeconds(2f);
        theOrder.Move();
    }
}
