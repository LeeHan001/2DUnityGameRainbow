using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Dialogue dialogue_1;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private AudioManager theAudio;

    public string pickup_sound;

    public int itemID;
    public int _count;
    private PlayerManager thePlayer;
    public int ItemCheck;
    private Inventory theInven;

    public bool flag = false;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    void FixedUpdate()
    {
        if (thePlayer.item_Check > ItemCheck)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E)) && theInven.activated == false && !flag)
            {
                flag = true;
                Inventory.instance.GetAnItem(itemID, _count);
                if (thePlayer.item_Check == ItemCheck)
                {
                    StartCoroutine(FristItemCoroutine());
                }
            }
        }
    }

    IEnumerator FristItemCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theAudio.Play(pickup_sound);

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

        flag = false;

        thePlayer.item_Check++;
    }
}
