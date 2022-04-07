using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFlow : Singleton<StageFlow>
{
    GameManager gameManager => GameManager.Instance;
    public int stage = 1;

    [Header("UI")]
    [SerializeField] Image stageFlowObj;
    [SerializeField] Text stageText;

    Animator anim;
    AudioSource source;

    public override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();
        anim = stageFlowObj.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartStage(stage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStage(int stage)
    {
        GameManager.Instance.Init(stage);
        InitStage();
        source.Play();
        UIController.Instance.ChangeBackground(stage);
        anim.SetTrigger("StartStage");
        stageText.text = "stage"+stage;
        SpawnManager.Instance.SpawnDataParce(stage);
    }

    public void EndStage()
    {
        if (stage >= 2)
        {
           
            anim.SetTrigger("StartStage");
            stageText.text = "Game Clear!";
            gameManager.GameOver();
            return;
        }
        else
        {

            CheckStageScore();
            stage++;
            StartStage(stage);

        }
    }

    void CheckStageScore()
    {
        gameManager.stagescore +=
            (int)(gameManager.HP + (GameManager.MaxHP - gameManager.Pain));
    }


    public void MoveStage(int stage)
    {
        this.stage = stage;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().Dead();
        }
        StartStage(stage);
    }

    void InitStage()
    {
        GameObject[] NPCes = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < NPCes.Length; i++)
        {
            NPCes[i].GetComponent<NPC>().Dead();
        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<Item>().Dead();
        }
        GameObject[] bulletes = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < bulletes.Length; i++)
        {
            Destroy(bulletes[i]);
        }
    }
}
