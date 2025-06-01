using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

    //Player Stats
    //--------------------
    public float moveSpeed = 5f;
    public float playerHealth = 100f;

    //Audio
    public AudioSource FootstepsFX;
    public AudioSource FootstepsFX1;
    private float footstepDelta;
    private bool step1 = true;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindWithTag("Player");
        PlayerItems playerItems = player.GetComponent<PlayerItems>();

        //set player position
        transform.position = new Vector3(-4f, 0f, 0f);

        //get item stats
        if (playerItems.HasItem("Boots of Mercury"))
        {
            moveSpeed += 1f;
        }
        if (playerItems.HasItem("Armor"))
        {
            playerHealth += 12;
        }

    }

    void Update()
    {
        //get movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
        //play footstpes fx
        footstepDelta += Time.deltaTime;
        if(footstepDelta > Random.Range(0.7f, 30f))
        {
            footstepDelta = 0f;
            if (step1)
            {
                FootstepsFX.Play();
                step1 = false;
            }
            else
            {
                step1 = true;
                FootstepsFX1.Play();
            }
        }

        //rotate
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - transform.position);
        transform.up = direction; // rotate to face the mouse (up is forward in 2D)
    }

    void FixedUpdate()
    {
        //move
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        Debug.Log("Player health: " + playerHealth);

        if (playerHealth <= 0)
        {
            //player dies - make this into a reset screen/animation/whatever
            Destroy(this.gameObject);
        }
    }

}
