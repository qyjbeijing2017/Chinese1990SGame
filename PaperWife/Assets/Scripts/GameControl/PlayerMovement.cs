using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float m_speed = 5f;
	[SerializeField] private Vector3 m_movement;
	[SerializeField] private Rigidbody m_playerRigidbody;
	
	private void Awake () {
		m_playerRigidbody = GetComponent<Rigidbody> ();

	}

	private void FixedUpdate () {
		//PlayerFollowCamera();
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);
	    
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

}