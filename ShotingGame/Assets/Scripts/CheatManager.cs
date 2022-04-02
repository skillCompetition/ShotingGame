using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatManager : Singleton<CheatManager>
{
    Player player => Player.Instance;
    GameManager gameManager => GameManager.Instance;
    SpawnManager spawnManager =>  SpawnManager.Instance;


    [Header("UI")]
    [SerializeField] GameObject cheatPanel;
    [SerializeField] InputField stageInput;
    [SerializeField] InputField hpInput;
    [SerializeField] InputField painInput;
    [SerializeField] InputField powerInput;


    [Header("God")]
    public bool isCheatGod;


    void Start()
    {
        cheatPanel.SetActive(false);
    }

    void Update()
    {
        isGodOn();
        isGodOff();
        SpawnRed();
        SpawnWhite();
        ShowCheatPanel();
        CheakBoom();
    }

    /// <summary>
    /// ���� ���� 
    /// </summary>
    void isGodOn()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (player.godCoroutine != null)
                player.StopCoroutine(player.godCoroutine);
            player.isGod = true;
            player.sprite.color = new Color(1, 1, 1, 0.7f);
            isCheatGod = true;
        }

    }

    /// <summary>
    /// �������� ����
    /// </summary>
    void isGodOff()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (player.godCoroutine != null)
                player.StopCoroutine(player.godCoroutine);
            player.sprite.color = Color.white;
            player.isGod = false;
            isCheatGod = false;
        }

    }

    /// <summary>
    /// ����������
    /// </summary>
    void SpawnRed()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int num = Random.Range(0, spawnManager.redPos.Length);
            Transform trans = spawnManager.redPos[num];
            Instantiate(spawnManager.NPC[0],trans.position,trans.rotation);
        }
    }
    /// <summary>
    /// ������ ����
    /// </summary>
    void SpawnWhite()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Transform trans = spawnManager.whitePos[Random.Range(0, spawnManager.whitePos.Length)];
            Instantiate(spawnManager.NPC[1], trans.position, trans.rotation);
        }
    }

    /// <summary>
    /// ġƮ �ǳ� �����ִ°�
    /// </summary>
    void ShowCheatPanel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stageInput.text = "";
            hpInput.text = "";
            painInput.text = "";
            powerInput.text = "";

            cheatPanel.SetActive(true);

        }
    }

    /// <summary>
    /// ġƮ ���氪 �ޱ�
    /// </summary>
    public void InputCheatBtnClick()
    {
        if (stageInput.text != "")
            MoveStage(int.Parse(stageInput.text));
        if (hpInput.text != "")
            HPChange(int.Parse(hpInput.text));
        if (painInput.text != "")
            PainChange(int.Parse(painInput.text));
        if (powerInput.text != "")
            PowerChange(int.Parse(powerInput.text));


        cheatPanel.SetActive(false);

    }

    void MoveStage(int stage)
    {
        if(stage == 1 ||stage == 2)
        {
            spawnManager.StopCoroutine(spawnManager.spawnEnemyCoroutine);
            StageFlow.Instance.MoveStage(stage);
        }

    }

    void HPChange(int hp)
    {
        if (!(hp > GameManager.MaxHP || hp < 0))   
            gameManager.HP = hp;

    }

    void PainChange(int pain)
    {
        if (!(pain > GameManager.MaxPain || pain < 0))
            gameManager.Pain = pain;
    }

    void PowerChange(int power)
    {
        if (!(power > 5 || power < 1))
            player.BulletLevel = power - 1;
    }

    void CheakBoom()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AllEnemyDead(true);
        }
    }

    /// <summary>
    /// ��� ���� �� ����
    /// </summary>
    /// <param name="isCheat">ġƮ�� ���� ���� ���� ���</param>
    public void AllEnemyDead(bool isCheat)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!isCheat)
            {
                if (enemies[i].TryGetComponent<Boss>(out var boss))
                    continue;
            }

            enemies[i].GetComponent<Enemy>().Dead();
        }



    }
}
