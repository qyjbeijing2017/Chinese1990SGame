using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class AttackBase : PlayerFunctionBase
{
    public string OptionButton = "Attack";
    public CDBase AttackTime = new CDBase(0.1f);
    public CDBase AttackCD = new CDBase(1.0f);
    [Range(0, 1)] public float ReactionCoefficient = 0.5f;
    [SerializeField] Collider2D collider;
    public DamageBase AttackDamage = new DamageBase();


    public override void PlayerInit()
    {
        if (collider)
            collider.enabled = false;

        AttackTime.OnTimeOut += AttackEnd;

        AttackDamage.Attacker = this;
    }

    public override void PlayerLoop()
    {

        if (!collider) return;
        if (AttackTime.CD < 1 || AttackCD.CD < 1) return;
        if (Input.GetButtonDown(OptionButton + Player.ID))
        {
            AttackNow();
        }
    }

    virtual protected void AttackNow()
    {
        collider.enabled = true;
        AttackTime.Start();
    }

    void AttackEnd()
    {
        collider.enabled = false;
        AttackCD.Start();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collider.IsTouching(collision))
        {

            BeHitBase beHit = collision.GetComponent<BeHitBase>();

            if (beHit)
            {
                AttackDamage.AttackPolarity = Player.PlayerPolarity;
                AttackDamage.AttackPosition = transform.position;
                beHit.OnBeHitBefore(AttackDamage.Copy());
                ReactionForce(collision.GetComponent<PlayerBase>());
            }

        }
    }


    protected virtual void ReactionForce(PlayerBase enemy)
    {
        if (AttackDamage.AttackPolarity == Polarity.None)
        {
            return;
        }

        Vector2 myPoistion2Target = (new Vector2(enemy.transform.position.x, enemy.transform.position.y) - AttackDamage.AttackPosition).normalized;

        if (AttackDamage.AttackPolarity != enemy.PlayerPolarity)
        {
            PlayerRigidbody2D.AddForce(myPoistion2Target * AttackDamage.AttackForce * ReactionCoefficient);
        }
        else
        {
            PlayerRigidbody2D.AddForce(-myPoistion2Target * AttackDamage.AttackForce * ReactionCoefficient);
        }


    }
}
