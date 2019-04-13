using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitWithNonePolarity : BeHitWithCoefficient
{


    protected override void OnBeHit(DamageBase damage)
    {
        base.OnBeHit(damage);

        if (damage.AttackPolarity == Polarity.None)
        {
            Vector2 myPoistion2Target = (damage.AttackPosition - new Vector2(Player.transform.position.x, Player.transform.position.y)).normalized;
            PlayerRigidbody2D.AddForce(-myPoistion2Target * damage.AttackForce * AttackForceCoefficient);
            Player.OnBeHit?.Invoke(damage);
        }
    }
}
