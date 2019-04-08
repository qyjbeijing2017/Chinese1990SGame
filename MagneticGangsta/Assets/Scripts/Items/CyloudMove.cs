using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CyloudMove : PlayerFunctionBase
{

    public CycloudTargetPosition TargetPosition;
    public float Speed = 3.0f;
    public Vector2 MoveAdd = new Vector3(0, 0, 0);

    private Vector2 m_nowPosition;

    public UnityAction OnDestory;


    void Start()
    {
        transform.position = TargetPosition.StartPositon;
        m_nowPosition = TargetPosition.StartPositon;
    }

    // Update is called once per frame
    void Update()
    {
            
        if (TargetPosition.Start2End.magnitude <= (m_nowPosition- TargetPosition.StartPositon).magnitude )
        {
            OnDestory?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            m_nowPosition += TargetPosition.Start2End.normalized * Speed * Time.deltaTime;
            transform.position = m_nowPosition + MoveAdd;
        }
    }
}
