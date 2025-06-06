using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;  
    public Transform enemyTransform; 
    public Vector3 offset = new Vector3(0, .5f, 0);

    public float maxHealth = 100f;
    private float currentHealth = 100f;

    public AudioSource EnemyHitSFX;
    public AudioSource EnemyDieSFX;

    public GameObject AudioPlayer;


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
        //
        //Pitch Bending----------
        // Load AudioMixer and AudioMixerGroup separately
        /*var pitchMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("pitchBendGroup");
        var pitchBendGroup = pitchMixer.FindMatchingGroups("PitchMixer")[0]; // or use correct path

        EnemyHitSFX.outputAudioMixerGroup = pitchBendGroup;

        // Set pitch and update exposed param
        float tempo = Random.Range(0.8f, 1.2f);
        EnemyHitSFX.pitch = tempo;
        pitchMixer.SetFloat("MyExposedParam1", 1f / tempo);
        */

        EnemyHitSFX.Play();


        UpdateHealthBar();


    }

    private void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        fillImage.fillAmount = fillAmount;

        if(currentHealth <= 0)
        {
            GameObject Director = GameObject.FindWithTag("EnemyDirector");
            //if its not a summon
            EnemyDieSFX.Play();
            if (!gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().isASummon)
            {
                //update director
                Director.GetComponent<EnemyDirector>().enemySlain();
                //Get gold
                GameObject DataHolder = GameObject.FindWithTag("DataHolder");
                //this line sucks lol
                DataHolder.GetComponent<DataHolder>().UpdateGold(gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().goldDropped[gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().AItype]);

            }
            //kill enemy
            
            Destroy(gameObject.transform.parent.gameObject);
            


        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
