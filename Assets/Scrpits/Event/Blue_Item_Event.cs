using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Item_Event : MonoBehaviour
{
    public Transform target;

    public Dialogue dialogue_0;
    public Dialogue dialogue_1;
    public GameObject Light;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;//"DirY" == 1
    private CameraManager theCamera;
    private FadeManager theFade;
    private Inventory theInven;

    public int itemID;
    public int _count;

    public bool flag = false;

    [Tooltip("UP, DOWN, LEFT, RIHT")]
    public string direction;
    private Vector2 vector;

    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theInven = FindObjectOfType<Inventory>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && flag == false)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.E) && theInven.activated == false)
            {
                flag = true;
                //AudioManager.instance.Play(pickUpSound);
                StartCoroutine(TransferCoroutine());
                thePlayer.item_Check++;
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_0);
        yield return new WaitUntil(() => !theDM.talking);
        Inventory.instance.GetAnItem(itemID, _count);
        theFade.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Light.SetActive(false);
        theOrder.SetTransparent("player");

        yield return new WaitForSeconds(0.5f);
        theOrder.SetUnTransparent("player");
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        thePlayer.transform.position = target.transform.position;
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

    }
}
