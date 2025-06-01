using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    // List of item names or item IDs
    public List<string> items = new List<string>();
    private int cost = 10;
    public GameObject DataHolder;

    //Items List
    //=======================
    //Item 1 - Axe: Increased Damage by 5, stackable up to 2 times.Summons axe around the player

    //Item 2 - Shield: Increase Health by 10, random chance to block damage(10%?) from enemy gladimal

    //Item 3 - Armor: Increases Health by 12

    //Item 4 - Spear: Increase Damage by 5, increases range by 5

    //Item 5 - Artemis’ Quiver: Increase Fire rate by 20%

    //Item 6 - Trident of Neptune: Increases Range by 10, Damage by 10. Occasional water wave attack(10%?)

    //Item 7 - Boots of Mercury: Increases movement speed by 10% and dash by 10%

    //Item 8 - Baccus’ Vines: When dashing, random (10 %?) chance to spawn a vine that increases dash range/does damage to nearest enemy

    //Item 9 - Vulcan’s Hammer: Damage increase by 10%, Changes attacks to AOE Earthquake

    //Item 10 - Hera’s Love: increases attack range by 10, random chance to draw in enemy gladimal

    //Item 11 - Jupiter’s Lightning: increases attack by 5, increases range by 10, 5% chance to paralyze enemy


    public void Start()
    {
        DataHolder = GameObject.FindWithTag("DataHolder");
    }
    // Add an item
    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            Debug.Log("Picked up item: " + itemName);
        }
    }

    public void AddItemByInt(int itemID)
    {
        switch (itemID)
        {
            case 0:
                //
                if (!items.Contains("Axe") && DataHolder.GetComponent<DataHolder>().gold >= cost)
                {
                    items.Add("Axe");
                    DataHolder.GetComponent<DataHolder>().UpdateGold(-cost);
                }
                break;
            case 1:
                //
                if (!items.Contains("Shield") && DataHolder.GetComponent<DataHolder>().gold >= cost)
                {
                    items.Add("Shield");
                    DataHolder.GetComponent<DataHolder>().UpdateGold(-cost);
                }
                break;
            case 2:
                //
                if (!items.Contains("Armor") && DataHolder.GetComponent<DataHolder>().gold >= cost)
                {
                    items.Add("Armor");
                    DataHolder.GetComponent<DataHolder>().UpdateGold(-cost);
                }
                    
                break;

            case 3:
                //
                if (!items.Contains("Spear") && DataHolder.GetComponent<DataHolder>().gold >= cost)
                {
                    items.Add("Spear");
                    DataHolder.GetComponent<DataHolder>().UpdateGold(-cost);
                }
                    
                break;

            case 4:
                //
                if (!items.Contains("ArtemisQuiver") && DataHolder.GetComponent<DataHolder>().gold >= cost)
                {
                    items.Add("ArtemisQuiver");
                    DataHolder.GetComponent<DataHolder>().UpdateGold(-cost);
                }
                    
                break;



            default:
                break;
        }
        
            
    }

    // Check if player has an item
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // Remove an item (optional)
    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            Debug.Log("Removed item: " + itemName);
        }

    }
}
