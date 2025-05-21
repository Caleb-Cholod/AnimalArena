using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;         
    public float rotationSpeed = 180f; 

    private Transform player;

    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        Debug.Log("Hit");
        GetComponentInChildren<EnemyHealthBar>().TakeDamage(20f);
    }

    void Update()
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
}
