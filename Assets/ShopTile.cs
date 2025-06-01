using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTile : MonoBehaviour
{
    public int ItemID;
    public GameObject PlayerItems;
    // Start is called before the first frame update
    void Start()
    {
        PlayerItems = GameObject.FindWithTag("PlayerItems");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyItem()
    {
        PlayerItems.GetComponent<PlayerItems>().AddItemByInt(ItemID);
    }
}
