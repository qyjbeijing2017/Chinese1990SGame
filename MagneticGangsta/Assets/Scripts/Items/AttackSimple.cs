using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSimple : PlayerFunctionBase
{
    public DamageBase AttackDamage = new DamageBase();

    public bool IsAttack;

    public Collider2D collider;



    public override void PlayerInit()
    {
        if (!collider)
            collider = GetComponent<CircleCollider2D>() as Collider2D;

        AttackDamage.Attacker = this;

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }


}
