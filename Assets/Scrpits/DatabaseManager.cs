using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;
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

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();
    void Start()
    {
        itemList.Add(new Item(10001, "찢어진 종이", "무언가 적혀있는 종이다.\n플레이어 이동:\n방향키, WASD\n빠른 이동: shift\n일시정지: esc키\n인벤토리: I키\n상호작용: E키,Z키\n대화 진행 : E키,Z키\n취소: X키\n\n인생은 단 한번.\n세이브도 단 한번.\n\n이라는 알 수 없는\n글들이 적혀있다. \n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10002, "우유", "따듯한 온기가\n 남아있는 우유다. \n 이게 왜 금고 안에\n 있는지는 의문이다. \n\n우유의 밑 부분에는\n\"사랑하는 아기에게\"\n라고 적혀져있다.\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10003, "사랑", "따듯하다.\n 누군가를 안아줄\n 수 있을 것 같다. \n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10004, "게임기", "사용감이 남아있는\n 게임기다.\n소중히 다룬\n흔적이 보인다.\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10005, "문제집", "여러 문제를\n풀어볼 수 있는\n문제집이다.\n소중하게 보관됐는지\n깨끗하다.\n\n이름을 쓰는 곳은\n왜인지 텅 비어있다.\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10006, "밝게 빛나는 빛", "보고 있으면\n힘이 솟는 빛이다.\n왜인지 익숙한\n느낌이 든다.\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10007, "돈", "세상에서\n가장 중요한 것\n이라고 남자가 말했다.\n이게 세상에서 정말\n 가장 중요한 걸까?\n\n 나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10008, "밧줄", "두꺼운 밧줄이다.\n밧줄은 어딘가에\n걸어서 사용해야 \n할 거 같다.\n어디에 쓰려는 걸까?\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10009, "돈다발", "두꺼운 돈다발이다.\n\n나가기 : X키,I키", Item.ItemType.Use));
        itemList.Add(new Item(10010, "밧줄", "이상한 모양으로\n꼬여있는 밧줄이다.\n\n나가기 : X키,I키", Item.ItemType.Use));

        itemList.Add(new Item(20001, "보라색 돌", "보라색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20002, "남색 돌", "남색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20003, "파란색 돌", "파란색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20004, "초록색 돌", "초록색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20005, "노란색 돌", "노란색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20006, "주황색 돌", "주황색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
        itemList.Add(new Item(20007, "빨간색 돌", "빨간색 돌이다.\n\n나가기 : X키,I키", Item.ItemType.Equip));
    }
}
