using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcMove
{
    [Tooltip("NPCMove를 체크시 NPC 움직임")]
    public bool NPCMove;

    public string[] direction;
    [Range(1,5)]
    public int frequency;
}
public class NpcManager : MovingObject
{
    [SerializeField]
    public NpcMove npc;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {

    }

    
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i < npc.direction.Length; i++)
            {

                yield return new WaitUntil(() => queue.Count < 2);
                base.Move(npc.direction[i], npc.frequency);

                if (i == npc.direction.Length - 1)
                    i = -1;
            }
        }
    }


}
