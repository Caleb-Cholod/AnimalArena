using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerator : MonoBehaviour
{
    public GameObject[] tiles = new GameObject[5];
    public Sprite[] sprites = new Sprite[5];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            int RandItem = Random.Range(0, 5);
            tiles[i].GetComponent<ShopTile>().ItemID = RandItem;
            //set sprite
            tiles[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[RandItem];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
