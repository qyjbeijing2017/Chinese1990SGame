using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private int a;
    [Space(10)] public int b;
    [Header("这个是个测试")] public int c;
    [Header("1")]
    [SerializeField, Range(0, 10),Tooltip("d is a test")] private float d;

    public int A
    {
        get
        {
            return a;
        }
        set
        {
            Update();
            a = value;
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
