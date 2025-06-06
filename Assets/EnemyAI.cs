using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 180f;

    public float shootInterval = 2f;
    public GameObject bulletPrefab;
    public GameObject meleePrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    private float shootTimer;

    private float meleeTimer;
    public float meleeInterval = 1f;

    public GameObject miniSummon;
    private int numSummons;
    private float summonTimer = 3f;
    private float summonDelta;
    public bool isASummon;

    private Transform player;

    public int AItype;
    public int[] goldDropped = new int[5];

    public Color[] AIcolors = new Color[5];

    void Start()
    {

        goldDropped[0] = 5;
        goldDropped[1] = 7;
        goldDropped[2] = 8;
        goldDropped[3] = 10;
        goldDropped[4] = 12;



        //assign  color
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = AIcolors[AItype];

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

    }

    void Update()
    {
        //AI - Chaser
        if (AItype == 0)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            //rotate to player
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float rotationStep = rotationSpeed * Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -rotationStep, rotationStep);
            transform.Rotate(Vector3.forward, clampedAngle);

            //move
            transform.position += transform.up * moveSpeed * Time.deltaTime;

            //attack if possible
            meleeTimer += Time.deltaTime;
            if (meleeTimer >= meleeInterval)
            {
                meleeTimer = 0f;
                MeleeAtPlayer();
            }
        }
        //AI - Turret
        if (AItype == 1)
        {

            // Rotate toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -step, step));

            // Shoot timer
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                ShootAtPlayer();
            }
        }
        //AI - Gunner
        if (AItype == 2)
        {

            // Rotate toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -step, step));

            // Shoot timer
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                ShootAtPlayer();



            }

            //move close or further
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);

            // Rotate toward the player
            float angl = Vector3.SignedAngle(transform.up, directionToPlayer, Vector3.forward);
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -rotationStep, rotationStep));

            // Move to maintain ideal distance
            Vector2 moveDirection = Vector2.zero;

            if (distanceToPlayer > 5f + 0.1f)
            {
                moveDirection = transform.up;
            }
            else if (distanceToPlayer < 5f - 0.1f)
            {
                moveDirection = -transform.up;
            }
            transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        }

        //AI - Flanker
        if (AItype == 3)
        {

            // Rotate toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -step, step));

            // Shoot timer
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                ShootAtPlayer();

            }

            //move close or further
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);

            // Rotate toward the player
            float angl = Vector3.SignedAngle(transform.up, directionToPlayer, Vector3.forward);
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -rotationStep, rotationStep));

            // Move to maintain ideal distance
            Vector2 moveDirection = Vector2.zero;

            if (distanceToPlayer > 3f + 0.1f)
            {
                moveDirection = transform.up;
            }
            else if (distanceToPlayer < 3f - 0.1f)
            {
                moveDirection = -transform.up;
            }
            else
            {
                transform.position += (Vector3)(transform.right * (1f) * moveSpeed * Time.deltaTime);
            }
            transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        }



        //AI - Summoner
        if (AItype == 4)
        {

            // Rotate toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -step, step));

            // Summon timer
            summonDelta += Time.deltaTime;
            if (numSummons <= 2)
            {
                if (summonDelta >= summonTimer)
                {

                    summonDelta = 0f;
                    numSummons += 1;
                    SummonFriendly();
                }
            }
            else
            {
                //chase down player
                Vector2 direction1 = (player.position - transform.position).normalized;

                //rotate to player
                float angle1 = Vector3.SignedAngle(transform.up, direction1, Vector3.forward);
                float rotationStep = rotationSpeed * Time.deltaTime;
                float clampedAngle = Mathf.Clamp(angle1, -rotationStep, rotationStep);
                transform.Rotate(Vector3.forward, clampedAngle);

                //move
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            }
        }


        //AI - Boss
        if (AItype == 5)
        {

            // Rotate toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.up, direction, Vector3.forward);
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, Mathf.Clamp(angle, -step, step));

            // Summon timer
            summonDelta += Time.deltaTime;
            if (numSummons <= 2)
            {
                if (summonDelta >= summonTimer)
                {

                    summonDelta = 0f;
                    numSummons += 1;
                    SummonFriendly();
                }
            }
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                ShootAtPlayer();
                transform.Rotate(10, 0, 0);
                ShootAtPlayer();
                transform.Rotate(-20, 0, 0);
                ShootAtPlayer();
                transform.Rotate(10, 0, 0);

                shootTimer += shootInterval / 2;
            }

        }

    }

        void ShootAtPlayer()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * bulletSpeed;
        }

        void MeleeAtPlayer()
        {
            GameObject bullet = Instantiate(meleePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position += 0.5f*transform.up;
            bullet.GetComponent<EnemyBullet>().lifetime = 0.5f;
            
        }

        void SummonFriendly()
        {
        //create enemy
            GameObject miniEnemy = Instantiate(miniSummon, firePoint.position, firePoint.rotation);
            miniEnemy.GetComponent<EnemyAI>().isASummon = true;
            miniEnemy.transform.GetChild(1).gameObject.GetComponent<EnemyHealthBar>().maxHealth = 50;
    }

    public void RestatEnemy(int seed, int waveNum)
    {
        if(waveNum != 5)
        {
            //normal enemies
        //Set to a random type
        AItype = seed % 5;

        //give random stats
        moveSpeed = 2.5f + (seed % 10)/10;
        rotationSpeed = 150f + (seed % 60);
        shootInterval = 1.5f + (seed%20)/15;
        bulletSpeed = 4f + (seed%5)/2.5f;
        }
        else
        {
            //boss
            AItype = 5;
            gameObject.GetComponent<EnemyHealthBar>().maxHealth = 1500;
            moveSpeed = 2.5f;
            shootInterval = 1f;
            bulletSpeed = 8f;

        }

    }
}


