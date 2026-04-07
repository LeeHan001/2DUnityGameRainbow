using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    public float speed;
    public int walkCount;
    public int currentWalkCount;
    public int Check;

    protected Vector3 vector;

    public Queue<string> queue;

    public Animator animator;
    public BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public string walkSound;

    public AudioManager theAudio;

    private bool notCoroutine = false;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }
    public void Move(string _dir, int _frequency = 5)
    {
        queue.Enqueue(_dir);
        if(!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while(queue.Count != 0) 
        {
            /*
            Check++;
            if (Check == 2)
            {
                theAudio.Play(walkSound);
                Check = 0;
            }
            */
            switch (_frequency)
            {
                case 1:
                    yield return new WaitForSeconds(4f);
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);
                    break;
                case 3:
                    yield return new WaitForSeconds(2f);
                    break;
                case 4:
                    yield return new WaitForSeconds(1f);
                    break;
                case 5:
                    break;
            }

            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);
            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1;
                    break;
                case "LEFT":
                    vector.x = -1;
                    break;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            while (true)
            {
                bool checkCollsionFlag = CheckCollsion();
                if (checkCollsionFlag)
                {
                    animator.SetBool("Walking", false);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    break;
                }
            }

            animator.SetBool("Walking", true);

            //boxCollider.offset = new Vector2(vector.x * speed * 0.02f, vector.y * speed * 0.02f);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed * 0.02f, vector.y * speed * 0.02f, 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            if (_frequency != 5)
                animator.SetBool("Walking", false);

            /*
            int temp = Random.Range(1, 3);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
            }*/
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }

    protected bool CheckCollsion()
    {
        RaycastHit2D hit;
        //RaycastHit2D hit2;
        //RaycastHit2D hit3;

        Vector2 start = transform.position;
        //Vector2 start2 = transform.position + new Vector3(0.2f,0,0);
        //Vector2 start3 = transform.position + new Vector3(-0.2f, 0, 0);

        Vector2 end = start + new Vector2(vector.x * 0.22f, vector.y * 0.22f);
        ///Vector2 end2 = start2 + new Vector2(vector.x * 0.22f, vector.y * 0.22f);
        //Vector2 end3 = start3 + new Vector2(vector.x * 0.22f, vector.y * 0.22f);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        //hit2 = Physics2D.Linecast(start2, end2, layerMask);
        //hit3 = Physics2D.Linecast(start3, end3, layerMask);
        Debug.DrawLine(start, end, Color.white, 0.22f);
        boxCollider.enabled = true;


        if (hit.transform != null /*|| hit2.transform != null || hit3.transform != null*/)
            return true;
        return false;
    }

}
