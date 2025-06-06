using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToFightButton : MonoBehaviour
{
    public AudioSource click;
    private GameObject shopGenerator;
    private GameObject dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        shopGenerator = GameObject.FindWithTag("ShopGenerator");
        dataHolder = GameObject.FindWithTag("DataHolder");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void goToScene()
    {
        if(dataHolder.GetComponent<DataHolder>().gold >= shopGenerator.GetComponent<ShopGenerator>().waveCost)
        {
            dataHolder.GetComponent<DataHolder>().gold -= shopGenerator.GetComponent<ShopGenerator>().waveCost;
            SceneManager.LoadScene(2);
        }
        
    }
    public void OnHover()
    {
        click.Play();   
    }
}
