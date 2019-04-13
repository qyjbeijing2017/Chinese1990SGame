using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSimple : PlayerFunctionBase
{
    public override string Name { get { return "Attack"; } }
    public DamageBase AttackDamage = new DamageBase();

    public bool IsAttack;

    [SerializeField] protected CircleCollider2D collider;



    public override void PlayerInit()
    {
        if (!collider)
            collider = GetComponent<CircleCollider2D>();

        AttackDamage.Attacker = this;

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collider.IsTouching(collision))
        {
            OnAttack(collision);
        }
    }



    virtual protected void OnAttack(Collider2D collision)
    {


        BeHitBase beHit = collision.GetComponent<BeHitBase>();

        if (beHit)
        {          
            AttackDamage.AttackPosition = transform.position;
            beHit.OnBeHitBefore(AttackDamage.Copy());
            if (Player.OnAttack != null)
            {
                Player.OnAttack.Invoke(beHit.Player);
            }
        }

    }

}
