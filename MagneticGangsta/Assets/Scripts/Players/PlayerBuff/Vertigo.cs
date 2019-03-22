using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertigo : PlayerBuffBase
{
    float m_attackForceCoefficientAdd = 0.0f;

    public Vertigo(float maxtime, float attackForceCoefficientAdd)
    {
        Name = "Vertigo";
        MaxTime.CDTime = maxtime;
        m_attackForceCoefficientAdd = attackForceCoefficientAdd;
    }

    public override void BuffStart(BuffAttributesData buffAttributes)
    {
        buffAttributes.IsLockOption.AddBuffEffect(Name, (attribute, attributeBase) => { return true; });
        buffAttributes.AttackForceCoefficient.AddBuffEffect(Name, (attackForceCoefficient, attackForceCoefficientBase) => { return attackForceCoefficient * m_attackForceCoefficientAdd; });

    }

    public override void BuffEnd(BuffAttributesData buffAttributes)
    {
        buffAttributes.IsLockOption.RemoveBuffEffect(Name);
        buffAttributes.AttackForceCoefficient.RemoveBuffEffect(Name);
    }
}
