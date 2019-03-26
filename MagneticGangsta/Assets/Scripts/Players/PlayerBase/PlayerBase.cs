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

    [HideInInspector] public bool IsMoving = false;
    [HideInInspector] public bool IsOnGround = false;
    [HideInInspector] public bool IsLockOption = false;
    [HideInInspector] public bool IsDefence = false;

    public Rigidbody2D PlayerRigidbody2D;

    public Polarity PlayerPolarity = Polarity.North;
    public CapsuleCollider2D MainCollider;
    #endregion

    #region Events

    public UnityAction<PlayerBase> OnBeHit;
    public UnityAction<PlayerBase> OnAttack;
    public UnityAction<int> OnJump;
    public UnityAction OnChangePolarity;

    public UnityAction OnGroundBefroe;
    public UnityAction OnGround;
    public UnityAction OnExitGround;

    public UnityAction OnDie;
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
            OnDie += playerFunctionBases[i].OnPlayerDie;
            Type t = playerFunctionBases[i].GetType();
            if (!m_functionBases.ContainsKey(t.Name)) m_functionBases.Add(t.Name, playerFunctionBases[i]);
        }

        var enumerator = FunctionBases.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.PlayerInit();

        }

    }

    void InitBuffManager()
    {
        PlayerBuffManager playerBuffManager = GetComponent<PlayerBuffManager>();
        if (playerBuffManager)
        {
            playerBuffManager.Init(this);
            m_buffManager = playerBuffManager;
        }
    }

    private void Awake()
    {

        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        MainCollider = !MainCollider ? GetComponent<CapsuleCollider2D>() : MainCollider;
        OnDie += OnPlayerDie;
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


    private void OnPlayerDie()
    {
        IsMoving = false;
        IsOnGround = false;
        IsLockOption = true;
        IsDefence = false;
        gameObject.SetActive(false);
    }


    public void ReBorn(Vector2 position)
    {
        transform.position = position;
        IsLockOption = false;
        gameObject.SetActive(true);
    }
}

