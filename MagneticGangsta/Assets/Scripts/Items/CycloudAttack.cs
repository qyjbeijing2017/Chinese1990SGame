using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloudAttack : AttackSimple
{

    [SerializeField, Range(0, 1)] protected float centerForce;

    protected override void OnAttack(Collider2D collision)
    {

        BeHitBase beHit = collision.GetComponent<BeHitBase>();

        if (beHit)
        {
            AttackDamage.AttackPolarity = Player.PlayerPolarity;
            AttackDamage.AttackPosition = transform.position;
            DamageBase damage = AttackDamage.Copy();

            float u = (collision.transform.position - transform.position).magnitude / collider.radius;

            damage.AttackForce = AttackDamage.AttackForce * Mathf.Lerp(1, centerForce, u);
            beHit.OnBeHitBefore(damage);
            if (Player.OnAttack != null)
            {
                Player.OnAttack.Invoke(beHit.Player);
            }
        }
        base.OnAttack(collision);
    }
}
