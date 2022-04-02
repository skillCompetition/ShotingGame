using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject inputPanel;
    [SerializeField] InputField input;

    [Header("rankingShow")]
    [SerializeField] Text[] nameText;
    [SerializeField] Text[] scoreText;

    List<Rank> rankingList => GameManager.Instance.rankingList;

    // Start is called before the first frame update
    void Start()
    {
        //만약 게임 매니져의 점수가 5등보다 크거나 , 랭킹의 수가 4개 이하라면
        ShowInputID();
        //아니면 비활성화하고 바로 ShowRanking();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /// <summary>
    /// 아이디를 입력 받는 함수
    /// </summary>
    void ShowInputID()
    {
        inputPanel.SetActive(true);
    }

    public void InputnameBtnClick()
    {
        Rank rank = new Rank();
        rank.name = input.text;
        rank.score = GameManager.Instance.totalScore; 

        inputPanel.SetActive(false);

        setRanking(rank);

    }

    /// <summary>
    /// 랭킹을 세팅하는 함수
    /// </summary>
    void setRanking(Rank rank)
    {
        rankingList.Add(rank);

        if(rankingList.Count > 1)
        {
            rankingList.Sort((rank1, rank2) => rank1.score.CompareTo(rank2.score));
            rankingList.Reverse();
        }

        if (rankingList.Count > 5)
            rankingList.RemoveAt(4);

        ShowRanking();
    }

    void ShowRanking()
    {
        GameOverUIController.Instance.GameClearAudio();

        for (int i = 0; i < rankingList.Count; i++)
        {
            Rank rank = rankingList[i];
            nameText[i].text = rank.name;
            scoreText[i].text = rank.score.ToString();
        }
    }



}
