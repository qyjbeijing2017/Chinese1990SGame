using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Idle,
    Walk,
    Run
}
public class PlayerControl : MonoBehaviour
{

    

    [SerializeField] private PlayerState m_State = PlayerState.None;
    

    
    [SerializeField] private float m_SprintSpeed = 10.0f;
    [SerializeField] private float m_SprintJumpSpeed = 8.0f;
    [SerializeField] private float m_NormalSpeed = 6.0f;
    [SerializeField] private float m_NormalJumpSpeed = 7.0f;


    [SerializeField] private float m_Gravity = 20.0f;

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpSpeed;

    [SerializeField] private bool m_isOnGround = false;
    [SerializeField] private bool m_isWalking = false;
    [SerializeField] private bool m_isRunning = false;



    
    
    

}
