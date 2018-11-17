using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{

    [SerializeField,Range(1,20)] private float m_speed = 6.0f;
    [SerializeField,Range(0,30)] private float m_jumpSpeed = 0.8f;
    [SerializeField,Range(0,30)] private float m_gravity = 20.0f;

    private CharacterController m_characterController;
    private Vector3 m_cameraWorldForward;
    private Vector3 m_cameraWorldRight;

    public bool IsLock = false;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();

    }
    private void Start()
    {
        m_cameraWorldForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        m_cameraWorldRight = Camera.main.transform.right;
    }

    private void FixedUpdate()
    {
        Move();

    }
    private Vector3 moveDirection = Vector3.zero;
    void Move()
    {
        if (m_characterController.isGrounded && !IsLock)
        {
            moveDirection = m_cameraWorldForward * Input.GetAxis("Vertical") + m_cameraWorldRight * Input.GetAxis("Horizontal");
            moveDirection *= m_speed;

            if (moveDirection != Vector3.zero)
                transform.forward = moveDirection;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = m_jumpSpeed;
            }
        }
        moveDirection.y -= m_gravity * Time.fixedDeltaTime;
        m_characterController.Move(moveDirection * Time.fixedDeltaTime);


    }

}