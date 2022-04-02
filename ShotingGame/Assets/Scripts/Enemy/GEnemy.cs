using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemy : Enemy
{


    public override IEnumerator Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            var Bullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet bulletLogic = Bullet.GetComponent<Bullet>();
            bulletLogic.power = power;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
