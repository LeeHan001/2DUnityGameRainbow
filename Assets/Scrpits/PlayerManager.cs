using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    public bool Event = false;

    public int BGM_Check;
    public int Once_Check;
    public int Quest_Check;
    public int item_Check;
    public int Save_Check;
    public int RedMap_Quest_Check;
    public int GamePlay_Check;
    public int Quiz_Check;
    public int BlueMap_Check;
    public int IndigoMap_Check;
    public bool Pc_Check = false;
    public int VioletMap_Check;

    public int Car_item;
    public int Candy_item;
    public int Dool_item;

    public int _Check;

    static public PlayerManager instance;

    private SaveNLoad theSaveNLoad;

    public string currentSceneName;
    public string currentMapName;

    public float runSpeed;
    private float applyRunSpeed;

    private bool canMove = true;

    public bool notMove = true;
    void Start()
    {
        queue = new Queue<string>();
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
    }

    IEnumerator MoveCoroutine()//코루틴
    {
        while ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !notMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                animator.SetBool("Walking", false);
                animator.SetBool("Runing", true);
            }
            else
            {
                applyRunSpeed = 0;
                animator.SetBool("Walking", true);
                animator.SetBool("Runing", false);
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);//이동 벡터값 선언

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollsionFlag = base.CheckCollsion();
            if (checkCollsionFlag)
                break;

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) * Time.deltaTime, 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) * Time.deltaTime, 0);
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.001f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Runing", false);
        animator.SetBool("Walking", false);
        canMove = true;
    }

    void Update()
    {
        if (canMove && !notMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }

    void FixedUpdate()
    {
        if (applyRunSpeed == 0 && _Check >= 24)
        {
            theAudio.Play(walkSound);
            _Check = 0;
        }
        else if (applyRunSpeed != 0 && _Check >= 12)
        {
            theAudio.Play(walkSound);
            _Check = 0;
        }
        if (currentWalkCount != 0)
        {
            _Check++;
        }
    }
}
