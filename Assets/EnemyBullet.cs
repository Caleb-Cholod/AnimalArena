using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
                Destroy(gameObject);
                Debug.Log("Hurt");
            }

    }
}
