using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float m_speed = 5f;
	[SerializeField] private Vector3 m_movement;
	[SerializeField] private Rigidbody m_playerRigidbody;
	[SerializeField] private bool m_isJump;
	[SerializeField] private bool m_isOnGround;
	[SerializeField] private float m_jumpForce = 5f;
	
	private void Awake () {
		m_playerRigidbody = GetComponent<Rigidbody> ();

	}

	private void FixedUpdate () {
		//PlayerFollowCamera();
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);

		m_isJump = Input.GetKeyDown (KeyCode.Space);
		if (m_isJump) {
			Jump ();
		}
	    
	}
	/// <summary>
	/// 移动脚本
	/// </summary>
	/// <param name="h">水平轴</param>
	/// <param name="v">垂直轴</param>
	void Move (float h, float v) {
		m_movement = transform.forward * v + transform.right * h;
		m_movement = m_movement.normalized * m_speed * Time.deltaTime;
		m_playerRigidbody.MovePosition (transform.position + m_movement);
	}
	void Jump () {
		if (m_isOnGround) {
			m_playerRigidbody.velocity = new Vector3 (m_playerRigidbody.velocity.x, 0, m_playerRigidbody.velocity.z);
			m_playerRigidbody.AddForce (Vector3.up * m_jumpForce, ForceMode.Impulse);
			m_isOnGround = false;
		}

	}
	private void OnCollisionEnter (Collision m_Ground) {
		m_isOnGround = true;
	}
}