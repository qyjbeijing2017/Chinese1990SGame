using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBase : MonoBehaviour
{

    public int ID;


    #region Attribute

    public bool IsMoving;
    public bool IsOnGround;
    public bool IsLockOption;
    public bool IsDefence;

    public Rigidbody2D PlayerRigidbody2D;

    public Polarity PlayerPolarity = Polarity.None;
    #endregion

    #region Events

    public UnityAction OnHit;
    public UnityAction<int> OnJump;
    public UnityAction OnChangePolarity;

    public UnityAction OnGroundBefroe;
    public UnityAction OnGround;
    public UnityAction OnExitGround;

    #endregion Events

    Dictionary<string, PlayerFunctionBase> m_functionBases = new Dictionary<string, PlayerFunctionBase>();


    public Dictionary<string, PlayerFunctionBase> FunctionBases { get { return m_functionBases; } }

    PlayerBuffManager m_buffManager;
    public PlayerBuffManager BuffManager { get { return m_buffManager; } }
    void PlayerFuncsLoop()
    {

        var funcEnumerator = m_functionBases.GetEnumerator();
        while (funcEnumerator.MoveNext())
        {
            funcEnumerator.Current.Value.PlayerLoop();
        }

    }

    void InitPlayerFuncs()
    {
        PlayerFunctionBase[] playerFunctionBases = GetComponents<PlayerFunctionBase>();
        for (int i = 0; i < playerFunctionBases.Length; i++)
        {
            playerFunctionBases[i].Player = this;
            playerFunctionBases[i].PlayerInit();
            Type t = playerFunctionBases[i].GetType();
            if (!m_functionBases.ContainsKey(t.Name)) m_functionBases.Add(t.Name, playerFunctionBases[i]);
        }
    }

    void InitBuffManager()
    {
        PlayerBuffManager playerBuffManager = GetComponent<PlayerBuffManager>();
        if (playerBuffManager)
        {
            playerBuffManager.Player = this;
            m_buffManager = playerBuffManager;
        }
    }

    private void Awake()
    {

        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        InitPlayerFuncs();
        InitBuffManager();
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

