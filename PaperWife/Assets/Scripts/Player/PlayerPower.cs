using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerPower : MonoBehaviour
{

    [SerializeField, Header("精力最大值")] private float m_maxPower = 100.0f;
    [SerializeField, Header("当前精力值")] private float m_powerNow = 100.0f;
    [SerializeField, Header("攻击精力消耗")] private float m_attackCost = 30.0f;
    [SerializeField, Header("防御每秒精力消耗")] private float m_defenceCostPerS = 5.0f;
    [SerializeField, Header("精力每秒回复")] private float m_repower = 5.0f;
    [SerializeField, Header("精力回复等待时间")] private float m_repowerWaitTime = 3.0f;

    [SerializeField] private bool m_IsLockRepower = false;
    [SerializeField] private bool m_IsWaitRepowerEnd = false;
    public float PowerNow { get { return m_powerNow; } }
    public bool IsWaitRepowerEnd { get { return m_IsWaitRepowerEnd; } }
    public float MaxPower
    {
        get { return m_maxPower; }
        set { m_maxPower = value; }
    }

    public enum CostType
    {
        None = 0,
        Attack = 1,
        Defence = 2
    }

    public bool Attack(CostType costType)
    {
        if (m_powerNow > 0 && !m_IsWaitRepowerEnd)
        {
            switch (costType)
            {
                case CostType.None:
                    return false;
                case CostType.Attack:
                    m_powerNow -= m_attackCost;
                    StopCoroutine("WaitRepower");
                    StartCoroutine("WaitRepower");
                    if (m_powerNow < 0)
                    {
                        m_powerNow = 0;
                        m_IsWaitRepowerEnd = true;
                    }
                    break;
                case CostType.Defence:
                    m_powerNow -= m_defenceCostPerS * Time.deltaTime;
                    StopCoroutine("WaitRepower");
                    StartCoroutine("WaitRepower");
                    if (m_powerNow < 0)
                    {
                        m_powerNow = 0;
                        m_IsWaitRepowerEnd = true;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        return false;
    }

    private void Start()
    {
        InitPower();
    }
    public void InitPower()
    {
        m_powerNow = m_maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsLockRepower && m_powerNow < m_maxPower)
        {
            m_powerNow += m_repower * Time.deltaTime;
            if (m_powerNow >= m_maxPower)
            {
                m_powerNow = m_maxPower;
                m_IsWaitRepowerEnd = false;
            }
        }
    }

    IEnumerator WaitRepower()
    {
        m_IsLockRepower = true;
        yield return new WaitForSeconds(m_repowerWaitTime);
        m_IsLockRepower = false;
    }
}
