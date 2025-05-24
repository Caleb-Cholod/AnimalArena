using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 10f;
    private float bulletLifetime = 5f;
    private float bulletlifetimeDelta;
    private float damage;


    void Start()
    {
        //get damage from player items
        GameObject player = GameObject.FindWithTag("Player");
        PlayerItems playerItems = player.GetComponent<PlayerItems>();

        playerItems.AddItem("Bow");
        playerItems.AddItem("Knife");

        if (playerItems.HasItem("Axe"))
        {
            //Axe
            damage += 5;
        }
        if (playerItems.HasItem("Knife"))
        {
            //Knife
            damage += 7;
            bulletLifetime += 5f;
        }
        if (playerItems.HasItem("Bow"))
        {
            //Bow
            damage += 5;
            bulletLifetime += 3f;
        }
        if (playerItems.HasItem("Spear"))
        {
            //Spear
            damage += 5;
            bulletLifetime += 3f;
            bulletLifetime += 2f;
        }

        damage += baseDamage;
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
