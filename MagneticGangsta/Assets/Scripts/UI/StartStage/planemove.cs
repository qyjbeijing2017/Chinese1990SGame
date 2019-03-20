using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planemove : MonoBehaviour
{
    public float time;
    Vector3 positionstart;
    // Start is called before the first frame update
    void Start()
    {
        positionstart = transform.position;

    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += new Vector3(-1*Time.deltaTime, 0, 0);
        if (timer>=time)
        {
            transform.position = positionstart;
            timer = 0;
        }
    }
}
