using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    //Stats -------------
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    private float FRdelta;



    void Update()
    {
        FRdelta += Time.deltaTime;
        if (Input.GetMouseButton(0) && FRdelta > fireRate) //Left click
        {
            FRdelta = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
