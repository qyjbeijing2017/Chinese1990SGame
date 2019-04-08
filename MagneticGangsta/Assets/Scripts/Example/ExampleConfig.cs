using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
public class ExampleConfig : MonoBehaviour
{
    private void Awake()
    {
        Daemon.Instance.Init();
        UIManager.Instance.Open("ExamplePanel");
    }


    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
