using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    GameManager gameManager => GameManager.Instance;
    public int speed;

    [Header("Fire")]
    [SerializeField] GameObject[] bullets;
    public int bulletLevel;
    public float bulletDelay;
    public int BulletLevel
    {
        get => bulletLevel;
        set
        {
            bulletLevel = value;
            if (bulletLevel >= 4)
                bulletLevel = 4;
            else if (bulletLevel <= 0)
                bulletLevel = 0;
        }
    }

    [Header("God")]     //무적 상태
    public Coroutine godCoroutine;
    public bool isGod;

    [Header("SpeedUP")]
    public Coroutine speedUpCoroutine;

    [Header("Flower")]
    [SerializeField] GameObject[] followers;



    Animator anim;
    BoxCollider2D col;
    AudioSource audio;
    public SpriteRenderer sprite;


    public override void Awake()
    {
        base.Awake();

        
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(StartShowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.fixedDeltaTime);
        anim.SetInteger("move", (int)h);

    }

    float fireTimer = 0f;
    void FireCheck()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireTimer >= bulletDelay)
        {
            Fire();
        }
    }

    private void Fire()
    {
        audio.Play();
        Instantiate(bullets[bulletLevel], transform.position, transform.rotation);
        fireTimer = 0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGod)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet.mybullet == Bullet.BulletType.Enemy)
                {
                    gameManager.HP -= bullet.power;

                    if (godCoroutine != null)
                        StopCoroutine(godCoroutine);
                    godCoroutine = StartCoroutine(God(1.5f,1.5f));
                }
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                gameManager.HP -= (int)(collision.gameObject.GetComponent<Enemy>().power * 0.5f);

                if (godCoroutine != null)
                    StopCoroutine(godCoroutine);
                godCoroutine = StartCoroutine(God(1.5f, 1.5f));

            }
        }

        
    }

    /// <summary>
    /// 플레이어 처음등장
    /// </summary>
    /// <returns></returns>
    IEnumerator StartShowPlayer()
    {
        col.enabled = false;
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (timer >= 1f)
                break;
            yield return new WaitForEndOfFrame();
        }

        col.enabled = true;
    }

    
    /// <summary>
    /// 무적 상태
    /// </summary>
    /// <param name="showTime">보이는 시간</param>
    /// <param name="realTime">실질적 무적 시간</param>
    /// <returns></returns>
    public IEnumerator God(float showTime, float realTime)
    {
        if (CheatManager.Instance.isCheatGod)
            yield break;
        isGod = true;
        sprite.color = new Color(1,1,1,0.7f);
        yield return new WaitForSeconds(showTime);
        sprite.color = Color.white;
        yield return new WaitForSeconds(realTime - showTime);
        isGod = false;
    }

    public float temp;
    public IEnumerator SpeedUP()
    {
        temp = speed;
        speed *= 2;
        yield return new WaitForSeconds(2f);
        speed = (int)temp;
    }

    public void ShowFollower()
    {
        for (int i = 0;  i< followers.Length; i++)
        {
            if (followers[i].activeSelf == false)
                followers[i].SetActive(true);
            else
                continue;
            break;
        }
    }


}
