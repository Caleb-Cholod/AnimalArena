using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 20f;
    private float bulletLifetime = 5f;
    private float bulletlifetimeDelta;
    void Start()
    {
        
    }

    // Update is called once per frame
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

            Destroy(gameObject); // Bullet disappears on hit
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Optional: destroy on walls or other things
        }
    }


}
