using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class FlyingGameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject[] LifeImage;
    public GameObject gameOverSet;
    public FlyingGameObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private PlayerManager thePlayer;//"DirY" == 1
    private OrderManager theOrder;
    private FadeManager theFade;
    private CameraManager theCamera;
    private AudioManager theAudio;

    public string click_sound;

    void Awake()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theFade = FindObjectOfType<FadeManager>();
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };
        StageStart();
    }



    public void StageStart()
    {
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<TextMeshProUGUI>().text = "STAGE\nSTART";
        clearAnim.GetComponent<TextMeshProUGUI>().text = "STAGE\nCLEAR";
        ReadSpawnFile();
        theFade.FadeIn();
    }

    public void StageEnd()
    {
        clearAnim.SetTrigger("On");
        stage++;
        theFade.FadeOut();
        if (thePlayer.Quest_Check < 4)
            thePlayer.Quest_Check++;
        Invoke("BackHome", 5);
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        FlyGame_Player playerLogic = player.GetComponent<FlyGame_Player>();
        scoreText.text = string.Format("{0:n0}",playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;

        }
        int enemyPoint = spawnList[spawnIndex].point;

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
            
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0 , enemyLogic.speed * (-1));
        }

        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    public void UpdateLifeIcon(int life)
    {
        for(int index = 0; index < 3; index++)
        {
            LifeImage[index].SetActive(false);
        }

        for (int index = 0; index < life; index++)
        {
            LifeImage[index].SetActive(true);
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3f;
        player.SetActive(true);

        FlyGame_Player playerLogic = player.GetComponent<FlyGame_Player>();
        playerLogic.isHit = false;
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        theAudio.Play(click_sound);
        SceneManager.LoadScene("FlyingGame");
    }

    public void BackHome()
    {
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        gameOverSet.SetActive(false);
        theFade.FadeOut();
        theAudio.Play(click_sound);
        yield return new WaitForSeconds(2f);

        thePlayer.currentMapName = "Start";
        thePlayer.transform.position = new Vector3(90.3f, 23, 0);
        theCamera.transform.position = new Vector3(90.3f, 23, -10);
        SceneManager.LoadScene("Start");
        theFade.FadeIn();
        theOrder.Move();
    }
}
