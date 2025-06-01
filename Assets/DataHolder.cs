using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    public TMP_Text goldText;
    // Start is called before the first frame update
    public int gold = 0;
    public int waveNumber = 1;

    public Sprite[] sprites;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }
}
