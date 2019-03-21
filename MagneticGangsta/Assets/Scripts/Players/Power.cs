using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : PlayerFunctionBase
{
    public float MaxPower = 100.0f;
    [SerializeField]private float m_powerNow = 100.0f;
    public float PowerNow { get { return m_powerNow; } }

    public float RecoverySpeed = 20.0f;
    public CDBase RecoveryCD = new CDBase(3.0f);

    private bool m_isEmptyRecovery = false;

    public bool IsEmptyRecovery { get { return m_isEmptyRecovery; } }

    public override void PlayerInit()
    {
        m_powerNow = MaxPower;
    }

    public override void PlayerLoop()
    {
        if (RecoveryCD.CD < 1 || m_powerNow >= MaxPower) return;
        m_powerNow += RecoverySpeed * Time.deltaTime;
        if (m_powerNow >= MaxPower) m_isEmptyRecovery = false;
        if (m_powerNow > MaxPower) m_powerNow = MaxPower;
    }

    public bool PowerCost(float cost)
    {
        if (m_isEmptyRecovery)
        {
            return false;
        }

        if (m_powerNow > 0)
        {
            m_powerNow -= cost;
            if (m_powerNow <= 0)
            {
                m_powerNow = 0;
                m_isEmptyRecovery = true;
            }
            RecoveryCD.Start();
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnPlayerDie()
    {
        m_powerNow = MaxPower;
        m_isEmptyRecovery = false;
        RecoveryCD.Stop();
    }
}
