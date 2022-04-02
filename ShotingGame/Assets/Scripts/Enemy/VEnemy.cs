using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEnemy : Enemy
{
    public float dashSpeed;

    protected override void Start()
    {
        StartCoroutine(Dash());
        base.Start();
    }

    IEnumerator Dash()
    {
        int temp = speed;
        yield return new WaitForSeconds(0.2f);
        speed = (int)dashSpeed;
        yield return new WaitForSeconds(0.5f);
        speed = temp;
    }

    public override IEnumerator Attack()
    {
        for (int i = 0; i < 360; i+= 30)
        {
            var Bullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet.GetComponent<Bullet>().power = power;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        return base.Attack();
    }
}
