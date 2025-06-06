using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private SpriteRenderer sr;
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
    private float KnockbackAmount = 20;

    public GameObject PlayerAfterimage;

    //Audio
    public AudioSource SlideFX;
    public AudioSource ShootFX;

    //items
    public GameObject AxeBul;
    private bool Axe;

    public GameObject TridentBul;
    private bool Trident;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerItems = player.GetComponent<PlayerItems>();

        //sr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (playerItems.HasItem("BootsOfMercury"))
        {
            KnockbackAmount += 5f;
        }
        if (playerItems.HasItem("ArtemisQuiver"))
        {
            fireRate -= 0.2f;
        }
        if (playerItems.HasItem("TridentOfNeptune"))
        {
            
        }
        if (playerItems.HasItem("Axe"))
        {
            Axe = true;
        }
        if (playerItems.HasItem("TridentOfNeptune"))
        {
            Trident = true;
        }
    }


    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - transform.position);
        transform.up = direction;

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
            transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce((ForceDirection * KnockbackAmount), ForceMode2D.Force);
            Debug.Log("knockback");
            GameObject afterimg = Instantiate(PlayerAfterimage, transform.position, transform.rotation);
            afterimg.SetActive(true);

            if (knockbackDelta > knockbackDuration)
            {
                isBeingKnockedBack = false;
                knockbackDelta = 0;
            }
        }
    }

    void Shoot(int bulletType)
    {
        ShootFX.Play();
        if (bulletType == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * bulletSpeed;

            //items
            if (Axe)
            {
                if(Random.Range(0, 5) == 1)
                {
                GameObject axe = Instantiate(AxeBul, firePoint.position, firePoint.rotation);
                Rigidbody2D axerb = bullet.GetComponent<Rigidbody2D>();
                axerb.velocity = firePoint.up * bulletSpeed;
                }
                
            }

            if (Trident)
            {
                if (Random.Range(0, 10) == 1)
                {
                    GameObject trid = Instantiate(TridentBul, firePoint.position, firePoint.rotation);
                    Rigidbody2D tridentrb = bullet.GetComponent<Rigidbody2D>();
                    tridentrb.velocity = firePoint.up * bulletSpeed;
                }
            }
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

            //AfterImage
            GameObject afterimg = Instantiate(PlayerAfterimage, transform.position, transform.rotation);
            afterimg.SetActive(true);

            //play sfx
            SlideFX.Play();
        }
    }

}
