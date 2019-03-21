using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAttacker : PlayerFunctionBase
{

    PlayerBase m_attacker = null;

    public PlayerBase Attacker { get { return m_attacker; } }

    public CDBase AttackerLoginTime = new CDBase(3.0f);

    public override void PlayerInit()
    {
        Player.OnBeHit += OnBeHit;
        Player.OnAttack += OnBeHit;
        AttackerLoginTime.OnTimeOut += OnSignOut;

    }

    void OnBeHit(PlayerBase attacker)
    {
        m_attacker = attacker;
        AttackerLoginTime.Start();
    }

    void OnSignOut()
    {
        m_attacker = null;
    }

    public override void OnPlayerDie()
    {
        OnSignOut();
        AttackerLoginTime.Stop();
    }
}
