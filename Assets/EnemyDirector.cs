using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDirector : MonoBehaviour
{

    //enemy spawn points

    //list of enemies

    //num enemies alive
    int enemiesAlive;

    //waves and enemy types


    void Start()
    {
        enemiesAlive = 3;
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
            SceneManager.LoadScene(1);
        }
    }
}
