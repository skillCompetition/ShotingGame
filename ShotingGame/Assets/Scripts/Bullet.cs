using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int power;
    public int speed;

    [Header("BulletMove")]
    public Vector3 moveVec;
    public Vector3 changeVec = Vector3.zero;

    public enum BulletType
    {
        Player,
        Enemy,
    }
    public BulletType mybullet;

    void Start()
    {
        if (mybullet == BulletType.Player)
        {
            moveVec = Vector3.up;
        }
        else if(mybullet == BulletType.Enemy)
        {
            moveVec = Vector3.down;
        }

    }

    void Update()
    {
        if (changeVec == Vector3.zero)
        {
            transform.Translate(moveVec * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(changeVec * speed * Time.deltaTime);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (mybullet == BulletType.Enemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //플레이어의 HP 감소
                Destroy(gameObject);
            }
  
        }
        else if(mybullet == BulletType.Player)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Hit(power);

                Destroy(gameObject);

            }
        }
    }
}
