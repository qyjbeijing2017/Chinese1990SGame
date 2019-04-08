using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithCost : AttackBase
{
    public float AttackCost = 30.0f;

    protected override void AttackNow()
    {
        if (Player.FunctionBases.ContainsKey("Power"))
        {
            Power power = Player.FunctionBases["Power"] as Power;
            if (!power.PowerCost(AttackCost)) return;
        }
        base.AttackNow();
    }
}
