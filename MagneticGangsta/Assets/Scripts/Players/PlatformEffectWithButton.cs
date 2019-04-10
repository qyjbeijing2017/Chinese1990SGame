using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffectWithButton : HitPlatformEffect
{
    protected override void OnBeHit(DamageBase damage)
    {
        base.OnBeHit(damage);

        Vector2 orgin2Attacker = damage.Attacker.Player.transform.position - Player.transform.position;
        float attackAngle = Vector2.Angle(orgin2Attacker, Vector2.down) * 2.0f;
        if (attackAngle > HitAngle) return;

        BeHitEffectTime.Start();

        m_platformEffector.useOneWay = true;
        m_platformEffector.useColliderMask = true;
    }
}
