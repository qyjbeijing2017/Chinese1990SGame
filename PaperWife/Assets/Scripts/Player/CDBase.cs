using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CDBase
{
    private float m_time = 0.0f;
    private float m_timePercent = 1.0f;
    private float m_timer = 0.0f;

    public float CDTime
    {
        get { return m_time; }
        set { m_time = value; }
    }
    public float CDPercent
    {
        get { return m_timePercent; }
    }

    public event UnityAction OnCD;
    public event UnityAction OnOK;

    public void Start()
    {
        m_timer = 0;
        m_timePercent = 0;
        if (null != OnCD)
        {
            OnCD.Invoke();
        }
    }

    public float Update()
    {
        if (m_timePercent < 1)
        {
            m_timer += UnityEngine.Time.deltaTime;
            if (m_timer >= m_time)
            {
                m_timePercent = 1;
                if (null != OnOK)
                {
                    OnOK.Invoke();
                }
            }
            else
            {
                m_timePercent = m_timer / m_time;
            }
        }
        return m_timePercent;
    }

    public float FixUpdate()
    {
        if (m_timePercent < 1)
        {
            m_timer += UnityEngine.Time.fixedDeltaTime;
            if (m_timer >= m_time)
            {
                m_timePercent = 1;
                if (null != OnOK)
                {
                    OnOK.Invoke();
                }
            }
            else
            {
                m_timePercent = m_timer / m_time;
            }
        }
        return m_timePercent;
    }

    CDBase(float time)
    {
        m_time = time;
    }
    CDBase() { }

}
