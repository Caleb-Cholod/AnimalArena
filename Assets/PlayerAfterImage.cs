using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= Time.deltaTime;
        Color color = new Color(0f, 0f, 0f, alpha);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
