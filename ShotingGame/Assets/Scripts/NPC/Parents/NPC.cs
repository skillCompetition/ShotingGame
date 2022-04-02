using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public int speed;

    void Start()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        
    }

    IEnumerator Move()
    {
        float timer = 0f;

        while (true)
        {
            float dir = Random.Range(-1, 2);
            timer = 0;

            while (true)
            {
                timer += Time.deltaTime;
                transform.Translate(new Vector3(dir, -1, 0 )* speed * Time.deltaTime);
                if (timer >= 1f)
                    break;
                yield return new WaitForEndOfFrame();
            }

        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    protected virtual void Use()
    {
        Dead();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Dead();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Use();
        }
    }
}
