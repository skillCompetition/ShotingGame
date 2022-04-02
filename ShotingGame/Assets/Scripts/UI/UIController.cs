using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : Singleton<UIController>
{
    GameManager gamemanager => GameManager.Instance;
    BossManager bossManager => BossManager.Instance;

    [Header("Player")]
    public Image hpImg;
    public Text hpText;
    public Image painImg;
    public Text painText;
    public Text scoreText;

    [Header("Boss")]
    public GameObject boss_HPObj;
    public Image boss_HpImg;
    public Text boss_HpText;

    [Header("MiniBoss")]
    public float miniBossMaxHP;

    [Header("Background")]
    public SpriteRenderer background;
    public Sprite[] sprites;

    [Header("Stop")]
    public GameObject StopPanel;

    // Start is called before the first frame update
    void Start()
    {
        StopPanel.SetActive(false);
        boss_HPObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowData();
    }

    void ShowData()
    {
        hpImg.fillAmount = gamemanager.HP / GameManager.MaxHP;
        painImg.fillAmount = gamemanager.Pain / GameManager.MaxPain;
        hpText.text = gamemanager.HP.ToString();
        painText.text = gamemanager.Pain.ToString();
        scoreText.text = "Score: " + gamemanager.enemyScore.ToString();

        if (boss_HPObj.activeSelf == true)
        {
            BossData();
        }
    }

    void BossData()
    {
        if (!bossManager.isMini)
        {
            Boss boss
            = bossManager.boss.GetComponent<Boss>();

            boss_HpImg.fillAmount = boss.HP / boss.MaxHP;
            boss_HpText.text = boss.HP.ToString() + "/" + boss.MaxHP.ToString();

        }
        else
        {

            MiniBossData();


        }

    }

    GameObject mini1 => bossManager.mini1;
    GameObject mini2 => bossManager.mini2;

    void MiniBossData()
    {

        if (mini1 != null && mini2 != null)     //�Ѵ� ������� ��
        {
            MiniBoss mini1Logic = mini1.GetComponent<MiniBoss>();
            MiniBoss mini2Logic = mini2.GetComponent<MiniBoss>();

            boss_HpImg.fillAmount = mini1Logic.HP + mini2Logic.HP / miniBossMaxHP;
            boss_HpText.text = (mini1Logic.HP + mini2Logic.HP).ToString() +
                "/" +
               miniBossMaxHP.ToString();
        }
        else if (mini1 == null && mini2 != null)    //mini1�� �׾��� ��
        {
            MiniBoss mini2Logic = mini2.GetComponent<MiniBoss>();
            boss_HpImg.fillAmount = mini2Logic.HP / miniBossMaxHP;
            boss_HpText.text = mini2Logic.HP.ToString() + "/" + miniBossMaxHP.ToString();

        }
        else if(mini1 != null && mini2 == null)     //mini2�� �׾��� ��
        {
            MiniBoss mini1Logic = mini1.GetComponent<MiniBoss>();
            boss_HpImg.fillAmount = mini1Logic.HP / miniBossMaxHP;
            boss_HpText.text = mini1Logic.HP.ToString() + "/" + miniBossMaxHP.ToString();


        }

    }

    public void ChangeBackground(int stage)
    {
        background.sprite = sprites[stage - 1];
    }

    public void StopGameBtnClick()
    {
        Time.timeScale = 0;
        StopPanel.SetActive(true);
    }

    public void ContinueGameBtnClick()
    {
        StopPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOverBtnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
