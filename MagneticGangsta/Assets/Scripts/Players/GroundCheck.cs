using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : PlayerFunctionBase
{
    public Vector2 FootPosition = new Vector2(0, -0.7f);
    public float FootLength = 0.1f;
    public float CheckGroundBeforeLength = 0.5f;

    private bool m_CheckOnGroundBefore = true;
    private Rigidbody2D m_rigidbody2d = null;

    public LayerMask GroundMask = 1 << 9 | 1 << 10;


    public override string Name { get { return "GroundCheck"; } }

    public override void PlayerInit()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
        Player.OnJump += InitCheckGorundBefore;
        Player.OnExitGround += InitCheckGorundBefore;
    }

    private void Update()
    {
        CheckOnGround();
        CheckOnGroundBefore();
    }


    public override void OnPlayerDie()
    {
        m_CheckOnGroundBefore = true;
    }

    private void CheckOnGround()
    {
        Vector3 originPosition = transform.position + new Vector3(FootPosition.x, FootPosition.y, 0);
        Vector3 endPosition = originPosition + Vector3.down * FootLength;

        bool isOnGround = false;

        Ray2D ray = new Ray2D(originPosition, Vector2.down);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, FootLength, GroundMask);
        if (!info.collider)
        {
            isOnGround = false;
        }
        else
        {
            isOnGround = true;
        }

        if (Player.IsOnGround != isOnGround)
        {
            if (Player.IsOnGround && Player.OnExitGround != null)
            {
                Player.OnExitGround.Invoke();
            }

            if (!Player.IsOnGround && Player.OnGround != null)
            {
                Player.OnGround.Invoke();
            }
        }
        Player.IsOnGround = isOnGround;
        Debug.DrawLine(originPosition + new Vector3(-0.05f, 0, 0), endPosition + new Vector3(-0.05f, 0, 0), new Color(1, 1, 1));
    }


    private void CheckOnGroundBefore()
    {
        Vector3 originPosition = transform.position + new Vector3(FootPosition.x, FootPosition.y, 0);
        Vector3 endPosition = originPosition + Vector3.down * CheckGroundBeforeLength;

        if (!Player.IsOnGround && m_CheckOnGroundBefore && m_rigidbody2d.velocity.y <= 0)
        {
            Ray2D ray = new Ray2D(originPosition, Vector2.down);
            RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, CheckGroundBeforeLength, GroundMask);

            if (info.collider)
            {
                if (Player.OnGroundBefroe != null)
                {
                    Player.OnGroundBefroe.Invoke();
                }
                m_CheckOnGroundBefore = false;
            }
        }

        Debug.DrawLine(originPosition + new Vector3(0.05f, 0, 0), endPosition + new Vector3(0.05f, 0, 0), new Color(1, 0, 0));
    }


    private void InitCheckGorundBefore()
    {
        m_CheckOnGroundBefore = true;
    }

    private void InitCheckGorundBefore(int jumpTimes)
    {
        InitCheckGorundBefore();
    }
}
