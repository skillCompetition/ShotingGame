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
        //���� ���� �Ŵ����� ������ 5��� ũ�ų� , ��ŷ�� ���� 4�� ���϶��
        ShowInputID();
        //�ƴϸ� ��Ȱ��ȭ�ϰ� �ٷ� ShowRanking();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    /// <summary>
    /// ���̵� �Է� �޴� �Լ�
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
    /// ��ŷ�� �����ϴ� �Լ�
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
