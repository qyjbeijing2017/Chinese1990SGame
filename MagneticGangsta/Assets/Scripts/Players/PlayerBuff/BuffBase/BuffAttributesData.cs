using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttributesData
{
    #region 初始化
    PlayerBase m_player = null;


    public BuffAttributesData(PlayerBase player)
    {
        m_player = player;
        InitData();
    }
    #endregion

    #region 登记属性

    public BuffAttributeBase<bool> IsLockOption;

    public BuffAttributeBase<float> AttackForceCoefficient;

    #endregion

    #region 登记属性修改位置

    /// <summary>
    /// 初始化属性位置
    /// </summary>
    void InitData()
    {
        IsLockOption = new BuffAttributeBase<bool>(m_player.IsLockOption);

        if (m_player.FunctionBases.ContainsKey("BeHit"))
            AttackForceCoefficient = new BuffAttributeBase<float>((m_player.FunctionBases["BeHit"] as BeHitWithCoefficient).AttackForceCoefficient);
    }

    /// <summary>
    /// 输出属性位置
    /// </summary>
    public void OutData()
    {
        m_player.IsLockOption = IsLockOption.Attribute;

        if (m_player.FunctionBases.ContainsKey("BeHit"))
            (m_player.FunctionBases["BeHit"] as BeHitWithCoefficient).AttackForceCoefficient = AttackForceCoefficient.Attribute;

    }
    #endregion
}
