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
    public Transform firePoint;
    public float bulletSpeed = 5f;
    private float shootTimer;

    private Transform player;

    public int AItype;

    void Start()
    {

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


    }

        void ShootAtPlayer()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * bulletSpeed;
        }
    }


