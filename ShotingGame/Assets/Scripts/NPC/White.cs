using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : NPC
{
    public GameObject[] items;

    protected override void Use()
    {
        //아이템 생성
        Instantiate(items[Random.Range(0, items.Length)],transform.position,transform.rotation);

        base.Use();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet.mybullet == Bullet.BulletType.Player)
            {
                Use();
            }
        }

        base.OnTriggerEnter2D(collision);
    }
}
