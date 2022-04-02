using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();

    }

    float fireTime = 0;
    void FireCheck()
    {
        fireTime += Time.deltaTime;
        if (fireTime >= 0.3f)
            Fire();
    }

    void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        fireTime = 0;
    }

    void OnEnable()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
       
    }
}
