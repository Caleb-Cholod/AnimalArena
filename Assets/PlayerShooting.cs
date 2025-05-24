using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject BigbulletPrefab;
    public Transform firePoint;

    PlayerItems playerItems;

    private bool isBeingKnockedBack = false;
    private float knockbackDelta;
    private float knockbackDuration = 0.3f;
    //Stats -------------
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    private float FRdelta;

    public float fireRateAlt = 2f;
    private float FRdeltaAlt;
    public Vector2 ForceDirection;
    private float KnockbackAmount = 25;


    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerItems = player.GetComponent<PlayerItems>();

        if (playerItems.HasItem("Boots of Mercury"))
        {
            KnockbackAmount += 5f;
        }
        if (playerItems.HasItem("Artemis Quiver"))
        {
            fireRate -= 0.1f;
        }
    }


    void Update()
    {
        FRdelta += Time.deltaTime;
        FRdeltaAlt += Time.deltaTime;
        if (Input.GetMouseButton(0) && FRdelta > fireRate) //Left click
        {
            FRdelta = 0;
            Shoot(0);
        }
        if (Input.GetMouseButton(1) && FRdeltaAlt > fireRateAlt) //Right click
        {
            FRdeltaAlt = 0;
            Shoot(1);
        }


        
        if (isBeingKnockedBack)
        {
            knockbackDelta += Time.deltaTime;
            //apply knockback
            gameObject.GetComponent<Rigidbody2D>().AddForce((ForceDirection * KnockbackAmount), ForceMode2D.Force);
            Debug.Log("knockback");
            if (knockbackDelta > knockbackDuration)
            {
                isBeingKnockedBack = false;
                knockbackDelta = 0;
            }
        }
    }

    void Shoot(int bulletType)
    {
        if (bulletType == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * bulletSpeed;
        }
        else if (bulletType == 1)
        {
            GameObject bullet = Instantiate(BigbulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * (bulletSpeed*0.75f);

            //knockback
            isBeingKnockedBack = true;
            //grab force direction
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPosition - transform.position);

            ForceDirection = -direction.normalized;//(transform.position - (Vector3)direction).normalized;

        }
    }

}
