using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : Singleton<BossManager>
{
    public GameObject[] bosses;
    public bool isBossTime;     //보스가 생성 중인지 아닌지
    public bool isMini;     //미니보스를 생성하는 타이밍인지 아닌지

    [Header("Boss")]
    public GameObject boss; //Boss, miniBoss

    [Header("Mini")]
    public GameObject mini1 = null;
    public GameObject mini2 = null;
    public bool lastmini;        //마지막 미니보스인지

    int stage => StageFlow.Instance.stage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 보스 생성하기
    /// </summary>
    public void SpawnBoss()
    {
        UIController.Instance.ShowBossAnim();

    }

    public void RealBossSpawn()
    {
        isBossTime = true;
        isMini = false;
        boss = Instantiate(bosses[stage - 1]);
        
    }

    public void SpawnMiniBoss()
    {
        isMini = true;
        lastmini = false;
        isBossTime = true;

        mini1 = Instantiate(bosses[stage + 1]);
        mini2 = Instantiate(bosses[stage + 1]);

        MiniBoss mini1Logic = mini1.GetComponent<MiniBoss>();
        MiniBoss mini2Logic = mini2.GetComponent<MiniBoss>();

        mini1Logic.moveVec = Vector3.right;
        mini2Logic.moveVec = Vector3.left;

        UIController.Instance.miniBossMaxHP = mini1Logic.MaxHP + mini2Logic.MaxHP;

        UIController.Instance.boss_HPObj.SetActive(true);

    }


}
