using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isGameOver;

    public const float MaxHP = 100;
    float hp;
    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            if (hp >= MaxHP)
                hp = MaxHP;
            else if (hp <= 0 && !isGameOver)
                GameOver();


        }
    }
    public const float MaxPain = 100;
    float pain;
    public float Pain
    {
        get => pain;
        set
        {
            pain = value;
            if (pain <= 0)
                pain = 0;
            else if (pain >= MaxPain && !isGameOver)
                GameOver();
        }
    }

    [Header("Score")]
    public int totalScore;
    public int enemyScore;
    public int itemScore;
    public int stagescore;

    [Header("Ranking")]
    public List<Rank> rankingList = new List<Rank>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void GameOver()
    {


        totalScore += enemyScore + stagescore + itemScore;
        isGameOver = true;

        //종료 씬으로 이동
        SceneManager.LoadScene("GameOverScene");
    }

    public void Init(int stage)
    {
        HP = MaxHP;

        switch (stage)
        {
            case 1:
                Pain =  MaxPain * 0.2f;
                break;
            case 2:
                Player.Instance.transform.position = new Vector2(0, -1);
                Pain = MaxPain * 0.3f;
                break;
            default:
                break;
        }
    }
}
