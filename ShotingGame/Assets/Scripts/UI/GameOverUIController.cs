using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIController : Singleton<GameOverUIController>
{

    GameManager gameManager => GameManager.Instance;

    [Header("UI")]
    [SerializeField] Text enemyScoreText;
    [SerializeField] Text stageScoreText;
    [SerializeField] Text itemScoreText;
    [SerializeField] Text totalScoreText;

    AudioSource sourse;

    public override void Awake()
    {
        base.Awake();

        sourse = GetComponent<AudioSource>();
    }

    void Start()
    {
        enemyScoreText.text = gameManager.enemyScore.ToString();
        stageScoreText.text = gameManager.stagescore.ToString();
        itemScoreText.text = gameManager.itemScore.ToString();
        totalScoreText.text = gameManager.totalScore.ToString();
    }

    public void RestartBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOverBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GameClearAudio()
    {
        sourse.Play();
    }
}
