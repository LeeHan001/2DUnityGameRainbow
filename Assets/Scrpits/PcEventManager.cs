using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PcEventManager : MonoBehaviour
{
    public GameObject Pc_On;
    public GameObject Pc_Off;
    public GameObject Advertise1;
    public GameObject Advertise2;
    public int Button_Count;
    public TextMeshProUGUI text;
    private AudioManager theAudio;
    private PlayerManager thePlayer;

    public void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theAudio = FindObjectOfType<AudioManager>();
        text.text = "0";
        Button_Count = 0;
    }

    public void PC_On()
    {
        thePlayer.Event = true;
        Pc_Off.SetActive(true);
        StartCoroutine(Wait());
        theAudio.Play("EnterSound");
        Pc_Off.SetActive(false);
        Pc_On.SetActive(true);
    }

    public void PC_Off()
    {
        thePlayer.Event = false;
        Pc_Off.SetActive(true);
        Pc_On.SetActive(false);
        StartCoroutine(Wait());
        theAudio.Play("EnterSound");
        Pc_Off.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Count()
    {
        theAudio.Play("EnterSound2");
        Button_Count++;
        text.text = Button_Count.ToString();
    }
}
