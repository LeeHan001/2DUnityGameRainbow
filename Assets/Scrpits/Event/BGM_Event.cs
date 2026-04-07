using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Event : MonoBehaviour
{
    private PlayerManager thePlayer;
    public int BGM_NUM;
    public float BGM_Volume;
    private BGMManager BGM;

    //public bool flag = false;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        BGM = FindObjectOfType<BGMManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            if(thePlayer.BGM_Check != BGM_NUM)
            {
                thePlayer.BGM_Check = BGM_NUM;
                BGM.SetVolume(BGM_Volume);
                BGM.FadeOutMusic();
                StartCoroutine(Wait());
                BGM.Play(BGM_NUM);
                BGM.FadeInMusic();
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
