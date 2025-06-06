using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDirector : MonoBehaviour
{
    private GameObject DataHolder;

    //enemy spawn points
    Vector3 sp1 = new Vector3(4f, 0f, 0f);
    Vector3 sp2 = new Vector3(0f, 4f, 0f);
    Vector3 sp3 = new Vector3(0f, -4f, 0f);
    //list of enemies
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyPrefab;
    //num enemies alive
    int enemiesAlive;

    //waves and enemy types
    public int waveNumber;
    public TMP_Text waveText;

    void Start()
    {
        DataHolder = GameObject.FindGameObjectWithTag("DataHolder");
        enemiesAlive = 3;
        waveNumber = DataHolder.GetComponent<DataHolder>().waveNumber;

        waveText.text = "-Wave " + waveNumber + "-";


        int randomSeed = Random.Range(0, 10000);

        //create and add enemies based on wave
        //Enemy1

        if(waveNumber != 5)
        {
        //normal waves
        GameObject enemy1 = Instantiate(enemyPrefab);
        enemy1.transform.position = sp1;
        enemy1.GetComponent<EnemyAI>().RestatEnemy(randomSeed, waveNumber);


        //Enemy2
        randomSeed = Random.Range(0, 10000);
        GameObject enemy2 = Instantiate(enemyPrefab);
        enemy2.transform.position = sp2;
        enemy2.GetComponent<EnemyAI>().RestatEnemy(randomSeed, waveNumber);

        //Enemy3
        randomSeed = Random.Range(0, 10000);
        GameObject enemy3 = Instantiate(enemyPrefab);
        enemy3.transform.position = sp3;
        enemy3.GetComponent<EnemyAI>().RestatEnemy(randomSeed, waveNumber);


        //add enemies

        enemies.Add(enemy1);
        enemies.Add(enemy2);
        enemies.Add(enemy3); 
        }
        else
        {
            GameObject enemy1 = Instantiate(enemyPrefab);
            enemy1.transform.position = sp1;
            enemy1.GetComponent<EnemyAI>().RestatEnemy(randomSeed, waveNumber);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemySlain()
    {
        //could be a better way to do this using objects better but whatever
        enemiesAlive--;
        if(enemiesAlive == 0)
        {
            //wave completed
            DataHolder.GetComponent<DataHolder>().waveNumber += 1;
            SceneManager.LoadScene(1);

        }
    }
}
