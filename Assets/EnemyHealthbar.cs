using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;           // The green part (health fill)
    public Transform enemyTransform;  // Reference to the enemy position
    public Vector3 offset = new Vector3(0, .5f, 0); // Offset above enemy

    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth = 100f;

    void Start()
    {
        currentHealth = maxHealth;

        if (enemyTransform == null)
            enemyTransform = transform.parent;
    }

    void Update()
    {
        // Keep the health bar positioned above the enemy
        transform.position = enemyTransform.position + offset;
        transform.rotation = Quaternion.identity; // Keep it upright
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        fillImage.fillAmount = fillAmount;

        if(currentHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
