using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CDBase : System.Object
{
    float m_cd = 1.0f;
    public float CD { get { return m_cd; } }

    public float CDTime = 0.0f;

    public event UnityAction OnTimeOut;


    float m_timer = 0.0f;
    float NowTimer
    {
        get
        {
            return m_timer;
        }
    }

    CDBase()
    {
        CDTime = 0.0f;
    }
    CDBase(float time)
    {
        CDTime = time;
    }

    public void Start()
    {
        m_timer = 0.0f;
        m_cd = 0.0f;
        Daemon.Instance.StopCoroutine("CDNow");
        Daemon.Instance.StartCoroutine("CDNow");
    }

    IEnumerator CDNow()
    {
        while (m_timer < CDTime)
        {
            m_cd = m_timer / CDTime;
            m_timer += UnityEngine.Time.deltaTime;
            yield return null;
        }
        if (OnTimeOut != null)
        {
            OnTimeOut.Invoke();
        }
    }

}
