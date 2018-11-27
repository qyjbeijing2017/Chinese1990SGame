using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddle : MonoBehaviour {
    Transform m_ai;

    public void StartShow(Level1AI ai)
    {
        m_ai = ai.transform;
        transform.position = Camera.main.WorldToScreenPoint(m_ai.position);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (m_ai)
        {
            transform.position = Camera.main.WorldToScreenPoint(m_ai.position);
        }
    }

}
