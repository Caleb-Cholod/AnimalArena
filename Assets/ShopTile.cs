using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopTile : MonoBehaviour
{
    public int ItemID;
    public GameObject PlayerItems;
    public TMP_Text itemDesc;
    public AudioSource clickSFX;
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
    public void OnHover()
    {
        itemDesc.text = PlayerItems.GetComponent<PlayerItems>().itemDescriptions[ItemID];
        clickSFX.Play();
    }
}
