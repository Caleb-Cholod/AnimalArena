using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 10f;
    private float bulletLifetime = 5f;
    private float bulletlifetimeDelta;
    private float damage;

    public int bulletType;


    void Start()
    {
        //get damage from player items
        GameObject player = GameObject.FindWithTag("Player");
        GameObject playerItems = GameObject.FindWithTag("PlayerItems");


        //bullet type
        if(bulletType == 1)
        {
            //axe
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }


        if (playerItems.GetComponent<PlayerItems>().HasItem("Axe"))
        {
            //Axe
            damage += 5;
        }
        if (playerItems.GetComponent<PlayerItems>().HasItem("Knife"))
        {
            //Knife
            damage += 7;
            bulletLifetime += 5f;
        }
        if (playerItems.GetComponent<PlayerItems>().HasItem("Bow"))
        {
            //Bow
            damage += 5;
            bulletLifetime += 3f;
        }
        if (playerItems.GetComponent<PlayerItems>().HasItem("Spear"))
        {
            //Spear
            damage += 5;
            bulletLifetime += 3f;
            bulletLifetime += 2f;
        }
        if (playerItems.GetComponent<PlayerItems>().HasItem("JupitersLightning"))
        {
            //Spear
            damage += 5;
        }
        if (playerItems.GetComponent<PlayerItems>().HasItem("VulcansHammer"))
        {
            //Spear
            damage += (damage/10f);
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
        //else if (!collision.CompareTag("Player"))
        //{
        //    Destroy(gameObject);
        //}
    }


}
