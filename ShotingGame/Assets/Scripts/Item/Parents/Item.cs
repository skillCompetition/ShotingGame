using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int speed;

    protected Player player => Player.Instance;
    protected GameManager gameManager => GameManager.Instance;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    protected virtual void Use()
    {
        GameManager.Instance.itemScore += 10;
        Dead();
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Use();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Dead();
        }
    }
}
