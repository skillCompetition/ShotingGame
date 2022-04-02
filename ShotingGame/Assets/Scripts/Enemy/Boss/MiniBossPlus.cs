using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossPlus : MiniBoss
{
    /// <summary>
    /// 공격을 선택
    /// </summary>
    protected override void CheckAttack()
    {
        if (attackIndex >= 4)
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
            case 3:
                StartCoroutine(GotoCircle());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 탄막형태의 원형 공격
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator CircleAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 360; j += 13)
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
    protected override IEnumerator SnakeAttack()
    {
        int bulletNum = 21;
        for (int i = 0; i < bulletNum; i++)
        {
            Vector2 vec1 = new Vector2(Mathf.Sin(Mathf.PI * 10 * i / bulletNum), -1);
            Vector2 vec2 = new Vector2(Mathf.Cos(Mathf.PI * 10 * i / bulletNum), -1);


            var Bullet = Instantiate(bullet, transform.position, transform.rotation);
            var Bullet2 = Instantiate(bullet, transform.position, transform.rotation);
            Bullet bulletLogic = Bullet.GetComponent<Bullet>();
            Bullet bulletLogic2 = Bullet.GetComponent<Bullet>();
            bulletLogic.power = power;
            bulletLogic2.power = power;
            bulletLogic.changeVec = vec1;
            bulletLogic.changeVec = vec2;
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
    protected override IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 3; i++)
        {

            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos[0].position, spawnPos[0].rotation);
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos[1].position, spawnPos[1].rotation);

            yield return new WaitForSeconds(1f);
        }


        yield return new WaitForSeconds(1f);
        attackIndex++;
        CheckAttack();
    }

    IEnumerator GotoCircle()
    {
        List<GameObject> b1 = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 360; j += 13)
            {
                var Bullet = Instantiate(bullet, transform.position, transform.rotation);
                Bullet.GetComponent<Bullet>().power = power;
                b1.Add(Bullet);
                Bullet.transform.rotation = Quaternion.Euler(0, 0, j);

                StartCoroutine(Go(b1));
            }

            yield return new WaitForSeconds(1f);
            attackIndex++;
            CheckAttack();
        }
    }

    IEnumerator Go(List<GameObject> b1)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < b1.Count; i++)
        {

            if (b1[i] == null)
                continue;

            Vector3 vec = player.transform.position - b1[i].transform.position;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            b1[i].transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
}
