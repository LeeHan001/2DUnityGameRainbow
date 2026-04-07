using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enemyscore;
    //public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public FlyingGameObjectManager objectManager;

    //public GameObject bulletObjA;
    public GameObject itemCoin;
    public GameObject itemPower;
    public FlyingGameManager gameManager;
    public AudioManager theAudio;
    SpriteRenderer spriteRenderer;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    public string Enemy_Fire_Sound;
    public string Boss_Fire_Sound1;
    public string Boss_Fire_Sound2;
    public string Exp_Sound;

    void Awake()
    {
        theAudio = FindObjectOfType<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        switch(enemyName)
        {
            case "B":
                health = 1500;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 35;
                break;
            case "M":
                health = 15;
                break;
            case "S":
                health = 3;
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                if (health <= 0) return;
                FireFoward();
                break;
            case 1:
                if (health <= 0) return;
                FireShot();
                break;
            case 2:
                if (health <= 0) return;
                FireArc();
                break;
            case 3:
                if (health <= 0) return;
                FireAround();
                break;
        }
    }

    void FireFoward()
    {
        theAudio.Play(Boss_Fire_Sound1);
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;

        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 1f;

        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 1f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();


        rigidR.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 4, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
            Invoke("Think", 3);
    }

    void FireShot()
    {
        theAudio.Play(Enemy_Fire_Sound);
        for (int index = 0; index < 10; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);

    }

    void FireArc()
    {
        theAudio.Play(Boss_Fire_Sound2);
        GameObject bullet = objectManager.MakeObj("BulletBossB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 5 * curPatternCount/maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);


        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.25f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        theAudio.Play(Enemy_Fire_Sound);
        int roundNumA = 15;
        int roundNumB = 10;

        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }
    void Update()
    {
        if (enemyName == "B")
            return;
        Fire();
        Reload();
    }


    void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        /*
        spriteRenderer.sprite = sprites[1];
        */
        Color color = spriteRenderer.GetComponent<SpriteRenderer>().color;
        color.a = 0.95f;
        spriteRenderer.GetComponent<SpriteRenderer>().color = color;
        Invoke("ReturnSprite",0.1f);


        if (health <= 0)
        {
            FlyGame_Player playerLogic = player.GetComponent<FlyGame_Player>();
            playerLogic.score += enemyscore;
            theAudio.Play(Exp_Sound);

            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);
            if(ran < 6)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 7)
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(ran < 10)
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }

            CancelInvoke();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);

            if (enemyName == "B")
                gameManager.StageEnd();
        }    
    }

    void ReturnSprite()
    {
        Color color = spriteRenderer.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        spriteRenderer.GetComponent<SpriteRenderer>().color = color;
    }

    void Fire()
    { 
        if (curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S")
        {
            theAudio.Play(Enemy_Fire_Sound);
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        if (enemyName == "L")
        {
            theAudio.Play(Enemy_Fire_Sound);
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.2f;

            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.2f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.1f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.1f);
            
            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            collision.gameObject.SetActive(false);
        }
    }
}
