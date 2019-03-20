using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : PlayerFunctionBase
{
    [Range(0, 30)] public float PlayerForce = 15f;
    [Range(0, 20)] public float MaxMoveSpeed = 10f;
    private Rigidbody2D m_rigidbody2d = null;

    public override void PlayerInit()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public override void PlayerLoop()
    {
        Moving();
        CheckMoving();
    }

    public void Moving()
    {
        float force = 0;
        force = Input.GetAxis("Move" + Player.ID) * PlayerForce;
        if (m_rigidbody2d.velocity.x > MaxMoveSpeed)
        {
            if (force > 0)
            {
                force = 0;
            }
        }

        if (m_rigidbody2d.velocity.x < -MaxMoveSpeed)
        {
            if (force < 0)
            {
                force = 0;
            }
        }
        m_rigidbody2d.AddForce(new Vector2(force, 0));
    }

    public void CheckMoving()
    {
        if (Mathf.Abs(m_rigidbody2d.velocity.x) >= 0.1)
        {
            Player.IsMoving = true;
        }
        else
        {
            Player.IsMoving = false;
        }
    }
}
