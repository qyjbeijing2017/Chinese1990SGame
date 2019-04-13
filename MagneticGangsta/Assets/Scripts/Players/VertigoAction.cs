using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VertigoAction : PlayerFunctionBase
{
    public override string Name { get { return "VertigoAction"; } }

    public bool IsEffectEnable
    {
        get
        {
            if (VertigoEffectEnableTime.CD < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public CDBase VertigoEffectEnableTime = new CDBase(1.0f);
    public float VertigoTime = 3.0f;
    public float MaxAttackPower = 5.0f;

    public float AttackForceCoefficientAdd = 1.5f;

    public event UnityAction<bool> VertigoAct;

    [SerializeField] protected Collider2D collider;


    private Vector2 m_lastVelocity;
    public Vector2 LastVelocity { get { return m_lastVelocity; } }

    public override void PlayerInit()
    {
        //Player.OnAttack += OnAttack;
        Player.OnBeHit += OnAttack;
    }

    void OnAttack(DamageBase damage)
    {
        VertigoEffectEnableTime.Start();
    }

    private void Update()
    {
        m_lastVelocity = Player.PlayerRigidbody2D.velocity;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collider.IsTouching(collision))
        {
            return;
        }

        if (IsEffectEnable && collision.gameObject.layer == 9 && !Player.IsDefence)
        {
            PlayerBase other = collision.gameObject.GetComponent<PlayerBase>();
            float AttackPower = 0.0f;
            if (!other)
            {
                Vector2 origin2target = (collision.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(LastVelocity, origin2target);
                AttackPower = LastVelocity.magnitude * Mathf.Cos(Mathf.Deg2Rad * angle);
            }
            else
            {
                if (other.FunctionBases.ContainsKey("VertigoAction"))
                {
                    VertigoAction vertigoActionOther = other.FunctionBases["VertigoAction"] as VertigoAction;
                    Vector2 otherVelocity = vertigoActionOther.LastVelocity;
                    Vector2 myVelocity = LastVelocity;
                    Vector2 origin2target = (collision.transform.position - transform.position).normalized;
                    Vector2 attackVelocity = myVelocity - otherVelocity;
                    float angle = Vector2.Angle(origin2target, attackVelocity);
                    AttackPower = attackVelocity.magnitude * Mathf.Cos(Mathf.Deg2Rad * angle);
                }
            }

            if (AttackPower > MaxAttackPower)
            {
                Vertigo vertigo = new Vertigo(VertigoTime, AttackForceCoefficientAdd);
                Player.BuffManager.AddBuff(vertigo);
                vertigo.MaxTime.OnTimeOut += OnVertigoEnd;
                VertigoAct?.Invoke(true);
            }
        }

    }



    protected void OnVertigoEnd()
    {
        VertigoAct?.Invoke(false);
    }

}
