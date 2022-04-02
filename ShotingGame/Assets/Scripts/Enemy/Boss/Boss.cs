using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss")]
    public float MaxHP;

    [Header("Attack")]
    protected int attackIndex;

    [Header("SpawnAttack")]
    public GameObject[] enemies;
    public Transform[] spawnPos;

    protected override void Start()
    {
        HP = MaxHP;
        StartCoroutine(Show());
    }

    /// <summary>
    /// 처음 등장
    /// </summary>
    /// <returns></returns>
    IEnumerator Show()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        CheckAttack();
    }

    /// <summary>
    /// 공격을 선택
    /// </summary>
    protected virtual void CheckAttack()
    {
        if (attackIndex >= 3)
            attackIndex = 0;

        switch (attackIndex)
        {
            case 0:
                StartCoroutine(CircleAttack());
                break;
            case 1:
                StartCoroutine(SnakeAttack());
                break;
            case 2:
                StartCoroutine(SpawnEnemy());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 탄막형태의 원형 공격
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator CircleAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 360; j+= 13)
            {
                var Bullet = Instantiate(bullet, transform.position, transform.rotation);
                Bullet.GetComponent<Bullet>().power = power;

                Bullet.transform.rotation = Quaternion.Euler(0, 0, j);

            }

            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForSeconds(1f);
        attackIndex++;
        CheckAttack();
    }

    /// <summary>
    /// 뱀꼬리 모양 공격
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator SnakeAttack()
    {
        int bulletNum = 21;
        for (int i = 0; i < bulletNum; i++)
        {
            Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * 5 * i / bulletNum), -1);

            var Bullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet bulletLogic = Bullet.GetComponent<Bullet>();
            bulletLogic.power = power;
            bulletLogic.changeVec = vec;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        attackIndex++;
        CheckAttack();
    }

    /// <summary>
    /// 적군을 소환
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator SpawnEnemy()
    {

        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos[0].position, spawnPos[0].rotation);
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos[1].position, spawnPos[1].rotation);

        yield return new WaitForSeconds(1f);
        attackIndex++;
        CheckAttack();
    }

    public override void Dead()
    {
        UIController.Instance.boss_HPObj.SetActive(false);
        BossManager.Instance.SpawnMiniBoss();
        base.Dead();
    }
}
