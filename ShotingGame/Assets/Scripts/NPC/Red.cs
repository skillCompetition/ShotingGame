using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : NPC
{
    protected override void Use()
    {
        GameManager.Instance.Pain += 5;

        base.Use();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Use();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Use();
        }
    }
}
