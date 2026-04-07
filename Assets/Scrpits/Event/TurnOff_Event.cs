using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff_Event : MonoBehaviour
{
    public PlayerManager thePlayer;

    public GameObject gameObject_True;
    public GameObject gameObject_False;

    public GameObject BGM_True;
    public GameObject BGM_False;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(thePlayer.Quest_Check >= 9)
        {
            if (collision.gameObject.name == "Player")
            {
                gameObject_True.SetActive(true);
                gameObject_False.SetActive(false);
                BGM_True.SetActive(true);
                BGM_False.SetActive(false);
            }
        }
    }
}
