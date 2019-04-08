using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloudAttack : AttackBase
{
    public override void PlayerInit()
    {
        if (!collider)
            collider = GetComponent<CircleCollider2D>() as Collider2D;


        AttackDamage.Attacker = this;
    }

    public override void PlayerLoop()
    {
    }

    protected override void ReactionForce(PlayerBase enemy)
    {
    }

}
