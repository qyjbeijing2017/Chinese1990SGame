using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGlitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material.renderQueue = 2980;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
