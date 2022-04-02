using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss
{
    public Vector3 moveVec;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        transform.Translate(moveVec * speed * Time.deltaTime);
    }

    public override void Dead()
    {
        col.enabled = false;
        source.Play();
        anim.SetTrigger("isDead");
        GameManager.Instance.enemyScore += score;
        if (!BossManager.Instance.lastmini)
        {
            BossManager.Instance.lastmini = true;
        }
        else
        {
            Debug.Log("마지막 미니 보스 죽음");
            BossManager.Instance.isBossTime = false;
            StageFlow.Instance.EndStage();
            UIController.Instance.boss_HPObj.SetActive(false);

        }


    }
}
