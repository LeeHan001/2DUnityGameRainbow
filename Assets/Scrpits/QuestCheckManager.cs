using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCheckManager : MonoBehaviour
{
    private PlayerManager thePlayer;
    private ItemPickup ItemPickUp;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        ItemPickUp = FindObjectOfType<ItemPickup>();

        if (thePlayer.item_Check >= ItemPickUp.ItemCheck)
        {
            Destroy(ItemPickUp.gameObject);
        }
    }

    void Update()
    {
    }
}
