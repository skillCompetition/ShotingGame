using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : Enemy
{
    public override IEnumerator Attack()
    {

        for (int i = 0; i < 3; i++)
        {
            //À¯µµÅº
            Vector3 vec = //new Vector3(0,0,0);   //ÀÓ½Ã
                player.transform.position - transform.position;
            float angle;

            var Bullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet.GetComponent<Bullet>().power = power;
            angle = Mathf.Atan2(vec.y,vec.x) * Mathf.Rad2Deg;
            Bullet.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            yield return new WaitForSeconds(0.5f);
        }

    }
}
