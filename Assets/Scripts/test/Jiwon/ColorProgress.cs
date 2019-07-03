using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProgress : MonoBehaviour
{
    SpriteRenderer colorA;
    // Start is called before the first frame update
    void SetBackgroundColor(int progress)
    {
        colorA.color = new Color(colorA.color.r, colorA.color.g, colorA.color.b, progress);
    }

    void Start()
    {
        colorA = gameObject.GetComponent<SpriteRenderer>();
        SetBackgroundColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
