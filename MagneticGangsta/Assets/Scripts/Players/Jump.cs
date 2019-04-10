using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerFunctionBase
{
    private Rigidbody2D m_rigidbody2d = null;
    private int m_jumpTimes = 0;

    public List<float> JumpVelocity = new List<float>() { 12.0f, 10.0f };

    public int JumpTimes { get { return m_jumpTimes; } }

    public override string Name { get { return "Jump"; } }

    public override void PlayerInit()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
        Player.OnGround += OnGround;
    }

    public override void PlayerLoop()
    {
        if (Input.GetButtonDown("Jump" + Player.ID) && m_jumpTimes < JumpVelocity.Count)
        {
            m_rigidbody2d.velocity = new Vector2(m_rigidbody2d.velocity.x, JumpVelocity[m_jumpTimes]);
            m_jumpTimes++;
            if (Player.OnJump != null)
            {
                Player.OnJump.Invoke(m_jumpTimes);
            }
        }
    }

    private void OnGround()
    {
        m_jumpTimes = 0;
    }
}
