using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimationController : PlayerFunctionBase
{
    [SerializeField] Animator m_animator;

    [SerializeField] Transform MagneticFieldGUI;

    public override void PlayerInit()
    {
        if (!m_animator) m_animator = GetComponent<Animator>();
        Player.OnJump += OnJump;
        Player.OnGround += OnGround;

        if (Player.FunctionBases.ContainsKey("AttackWithCost"))
        {
            AttackBase attack = Player.FunctionBases["AttackWithCost"] as AttackBase;
            attack.AttackTime.OnStart += OnAttackStart;
            attack.AttackTime.OnTimeOut += OnAttackStop;
            float r = (attack.collider as CircleCollider2D).radius;
            MagneticFieldGUI.localScale = new Vector3(r, r, r);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float inputDir = Input.GetAxis("Move" + Player.ID);
        if (inputDir < 0) Player.transform.localScale = new Vector3(-1, 1, 1);
        if (inputDir > 0) Player.transform.localScale = new Vector3(1, 1, 1);

        m_animator.SetFloat("MoveSpeed", Mathf.Abs(inputDir));
        m_animator.SetBool("Defence",Player.IsDefence);
    }

    void OnJump(int jumpTime)
    {
        m_animator.SetTrigger("Jump" + jumpTime);
    }

    void OnGround()
    {
        m_animator.SetTrigger("JumpEnd");
    }

    void OnAttackStart() { m_animator.SetBool("Attack", true); }
    void OnAttackStop() { m_animator.SetBool("Attack", false); }



}
