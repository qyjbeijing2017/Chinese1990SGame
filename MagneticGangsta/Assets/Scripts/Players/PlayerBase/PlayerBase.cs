using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour
{

    public int ID;


    #region Attribute

    public bool IsMoving { get; set; }
    public bool IsOnGround { get; set; }
    public bool IsLockOption { get; set; }

    #endregion

    #region Events

    public UnityAction OnHit;
    public UnityAction<int> OnJump;
    public UnityAction OnChangePolarity;

    public UnityAction OnGroundBefroe;
    public UnityAction OnGround;
    public UnityAction OnExitGround;

    #endregion Events

    List<PlayerFunctionBase> m_playerFunctionBases = new List<PlayerFunctionBase>();

    void PlayerFuncsLoop()
    {

        var funcEnumerator = m_playerFunctionBases.GetEnumerator();
        while (funcEnumerator.MoveNext())
        {
            funcEnumerator.Current.PlayerLoop();
        }

    }

    void InitPlayerFuncs()
    {
        PlayerFunctionBase[] playerFunctionBases = GetComponents<PlayerFunctionBase>();
        for (int i = 0; i < playerFunctionBases.Length; i++)
        {
            playerFunctionBases[i].Player = this;
            playerFunctionBases[i].PlayerInit();
            m_playerFunctionBases.Add(playerFunctionBases[i]);

        }
    }

    private void Awake()
    {
        InitPlayerFuncs();
    }
    private void Update()
    {
        if (!IsLockOption)
        {
            PlayerFuncsLoop();
        }
    }

    private void InitPlayer()
    {
        IsMoving = false;
        IsOnGround = false;
        IsLockOption = false;

    }


}

