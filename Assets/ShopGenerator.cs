using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerator : MonoBehaviour
{
    public GameObject[] tiles = new GameObject[5];
    public TMP_Text[] texts = new TMP_Text[3];
    public Sprite[] sprites = new Sprite[5];
    private GameObject PlayerItems;
    private GameObject DataHolder;
    private GameObject Director;

    public TMP_Text waveCostTxt;
    public TMP_Text goldText;

    public int waveCost;
    public int[] waveCosts = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        PlayerItems = GameObject.FindWithTag("PlayerItems");
        DataHolder = GameObject.FindWithTag("DataHolder");
        //Director = GameObject.FindWithTag("Director");

        waveCost = waveCosts[DataHolder.GetComponent<DataHolder>().waveNumber - 1];
        waveCostTxt.text = "" + waveCost;

        for (int i = 0; i < tiles.Length; i++)
        {
            int RandItem = Random.Range(0, 11);
            tiles[i].GetComponent<ShopTile>().ItemID = RandItem;
            //set sprite
            tiles[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[RandItem];

            texts[i].text = ""+PlayerItems.GetComponent<PlayerItems>().costs[RandItem];
        }
        
    }

    // Update is called once per frame
    public void UpdateGold(int amount)
    {
        DataHolder.GetComponent<DataHolder>().gold += 10;
        GameObject goldParent = GameObject.FindWithTag("GoldText");
        TextMeshProUGUI goldText = goldParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        goldText.text = DataHolder.GetComponent<DataHolder>().gold.ToString();
    }
}
