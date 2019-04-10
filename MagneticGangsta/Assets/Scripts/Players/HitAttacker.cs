using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAttacker : PlayerFunctionBase
{
    public override string Name { get { return "HitAttacker"; } }


    [SerializeField]PlayerBase m_attacker = null;

    public PlayerBase Attacker { get { return m_attacker; } }

    public CDBase AttackerLoginTime = new CDBase(3.0f);

    public override void PlayerInit()
    {
        Player.OnBeHit += OnBeHit;
        Player.OnAttack += OnAttack;
        AttackerLoginTime.OnTimeOut += OnSignOut;

    }

    void OnBeHit(DamageBase damage)
    {
        m_attacker = damage.Attacker.Player;
        AttackerLoginTime.Start();
    }

    void OnAttack(PlayerBase player)
    {
        m_attacker = player;
        AttackerLoginTime.Start();
    }


    void OnSignOut()
    {
        m_attacker = null;
    }

    public override void OnPlayerDie()
    {

    }
}
