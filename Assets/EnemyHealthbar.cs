using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;  
    public Transform enemyTransform; 
    public Vector3 offset = new Vector3(0, .5f, 0);

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
        //healthbar above obj
        transform.position = enemyTransform.position + offset;
        transform.rotation = Quaternion.identity;
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
            //kill enemy
            Destroy(gameObject.transform.parent.gameObject);
            //update director
            GameObject Director = GameObject.FindWithTag("EnemyDirector");
            Director.GetComponent<EnemyDirector>().enemySlain();

        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
