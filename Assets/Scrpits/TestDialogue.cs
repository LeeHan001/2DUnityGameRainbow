using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Choice choice;
    public Dialogue dialogue;
    private DialogueManager theDM;
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private NumberSystem theNumber;
    public int correctNumber;

    public string[] texts;

    private bool flag;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
        theNumber = FindObjectOfType<NumberSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }
    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue);
        yield return new WaitUntil(() => !theDM.talking);
        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choiceing);
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        theDM.Showtext(texts);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();

        Debug.Log(theChoice.GetResult());

    }
}
