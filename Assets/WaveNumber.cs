using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour
{
    private float alph;
    private float sitTimer;
    private float sitDur;
    private bool asc;
    // Start is called before the first frame update
    void Start()
    {
        alph = 0f;
        asc = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (asc)
        {
            alph += .002f;
            GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alph);
        }
        else
        {
            alph -= .001f;
            GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alph);
        }
        
        if(alph >= 1)
        {
            sitTimer += Time.deltaTime;
            if(sitTimer > sitDur)
            {
                asc = false;
            }
        }
    }
}
