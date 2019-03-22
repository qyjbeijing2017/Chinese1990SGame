using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitWithCoefficient : BeHitBase
{

    public float AttackForceCoefficient = 1.0f;

    protected override void OnBeHit(DamageBase damage)
    {
        if (Player.IsDefence)
        {
            return;
        }

        if (damage.AttackPolarity != Polarity.None)
        {

            Vector2 myPoistion2Target = (damage.AttackPosition - new Vector2(Player.transform.position.x, Player.transform.position.y)).normalized;

            if (damage.AttackPolarity != Player.PlayerPolarity)
            {
                PlayerRigidbody2D.AddForce(myPoistion2Target * damage.AttackForce * AttackForceCoefficient);
            }
            else
            {
                PlayerRigidbody2D.AddForce(-myPoistion2Target * damage.AttackForce * AttackForceCoefficient);
            }
            if (Player.OnBeHit != null)
            {
                Player.OnBeHit(damage.Attacker.Player);
            }
        }
    }
}
