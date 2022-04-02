using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool isBoss;

    public float hp;
    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                Dead();

            }
        }
    }
    public int power;
    public int speed;
    public int score;

    [Header("Attack")]
    public GameObject bullet;

    public Player player => Player.Instance;
    protected Animator anim;
    protected Collider2D col;
    protected AudioSource source;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    /// <summary>
    ///  공격 코루틴
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator Attack()
    {
        yield return null;
    }

    /// <summary>
    /// 피격함수
    /// </summary>
    /// <param name="power">얻은 데미지</param>
    public void Hit(int power)
    {
        anim.SetTrigger("isHit");
        HP -= power;
    }

    public virtual void Dead()
    {
        col.enabled = false;
        source.Play();
        anim.SetTrigger("isDead");
        GameManager.Instance.enemyScore += score;
    }

    public void Destroy()
    {
        Destroy(gameObject);

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (isBoss)
                return;

            //플레이어의 고통게이지 증가
            GameManager.Instance.Pain += (int)(power * 0.5f);

            Destroy(gameObject);

        }
    }
}
