using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;

    //Player Stats
    //--------------------
    public float moveSpeed = 5f;
    public float playerHealth = 100f;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        PlayerItems playerItems = player.GetComponent<PlayerItems>();
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
