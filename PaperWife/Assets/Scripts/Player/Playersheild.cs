using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersheild : MonoBehaviour
{
    private Animator animator;
    private bool m_defence;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorcontroll();
    }

    void animatorcontroll()
    {
        animator.SetBool("defence", m_defence);
        if (Input.GetButton("Defence1"))
            m_defence = true;
        else
            m_defence = false;
    }

}
