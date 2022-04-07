using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Enemy")]
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject[] enemies;      //B,G,V,C

    [Header("NPC")]
    public GameObject[] NPC;          //R, W


    [Header("White")]
    float whiteTimer;
    public float whiteDelay;
    [SerializeField] float whiteRan;
    public Transform[] whitePos;

    [Header("Red")]
    float redTimer;
    [SerializeField] float redDelay;
    [SerializeField] float redRan;
    public Transform[] redPos;



    BossManager bossManager => BossManager.Instance;
    List<Spawn> spawnList = new List<Spawn>();
    public Coroutine spawnEnemyCoroutine;


    void Update()
    {
        if (!BossManager.Instance.isBossTime)
        {
            Checkwhite();
            CheckRed();
        }

    }


    public void SpawnDataParce(int stage)
    {
        string filename = "stage" + stage.ToString();


        spawnList.Clear();

        TextAsset textAsset = Resources.Load(filename) as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
            {
                break;
            }

            Spawn spawn = new Spawn();

            spawn.name = line.Split(',')[0];
            spawn.pos = int.Parse(line.Split(',')[1]);
            spawn.delay = float.Parse(line.Split(',')[2]);

            spawnList.Add(spawn);
        }

        stringReader.Close();

        spawnEnemyCoroutine = StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < spawnList.Count; i++)
        {
            GameObject enemy = ReturnEnemy(spawnList[i].name);

            Instantiate(enemy, spawnPos[spawnList[i].pos].position, spawnPos[spawnList[i].pos].rotation);

            yield return new WaitForSeconds(spawnList[i].delay);
        }

        yield return new WaitForSeconds(2f);
        bossManager.SpawnBoss();
    }
    GameObject ReturnEnemy(string name)
    {
        GameObject enemy = null;

        switch (name)
        {
            case "B":
                enemy = enemies[0];
                break;
            case "G":
                enemy = enemies[1];
                break;
            case "V":
                enemy = enemies[2];
                break;
            case "C":
                enemy = enemies[3];
                break;

            default:
                break;
        }

        return enemy;
    }

    void Checkwhite()
    {
        whiteTimer += Time.deltaTime;
        if(whiteTimer >= whiteDelay)
        {

            if((int)Random.Range(0,whiteRan) == 0)
            {
                SpawnWhite();
            }
            whiteTimer = 0;

        }

    }

    private void SpawnWhite()
    {
        Transform trans = whitePos[Random.Range(0, whitePos.Length)];
        Instantiate(NPC[1], trans.position, trans.rotation);
    }


    void CheckRed()
    {
        redTimer += Time.deltaTime;
        if (redTimer >= redDelay)
        {
            if ((int)Random.Range(0, redRan) == 0)
            {
                SpawnRed();
            }
            redTimer = 0;

        }
    }

    private void SpawnRed()
    {
        Transform trans = redPos[Random.Range(0, redPos.Length)];
        Instantiate(NPC[0], trans.position, trans.rotation);
    }

}
