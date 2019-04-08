using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitEffect : PlayerFunctionBase
{

    public float DelayTime = 0.3f;

    public override void PlayerInit()
    {
        Player.OnBeHit += OnLockOption;

    }

    void OnLockOption(DamageBase player)
    {

        Delay delay = new Delay(DelayTime);
        Player.BuffManager.AddBuff(delay);

    }


}
