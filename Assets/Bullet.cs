using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    private float bulletLifetime = 5f;
    private float bulletlifetimeDelta;
    void Start()
    {
        
    }

    void Update()
    {
        bulletlifetimeDelta += Time.deltaTime;

        if(bulletlifetimeDelta > bulletLifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealthBar healthBar = collision.GetComponentInChildren<EnemyHealthBar>();
            if (healthBar != null)
            {
                
                healthBar.TakeDamage(damage);
            }
            Debug.Log("Hit");

            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


}
