using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBase))]
public abstract class PlayerFunctionBase : MonoBehaviour
{
    [HideInInspector]public PlayerBase Player;

    /// <summary>
    /// 用于初始化玩家功能，会在PlayerBase的Awake最后执行
    /// </summary>
    public virtual void PlayerInit() { }

    /// <summary>
    /// 适用于玩家的Update但是受控于操作锁（IsLockOption）;
    /// </summary>
    public virtual void PlayerLoop() { }
}
