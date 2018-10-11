using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float m_Speed = 5f;
	[SerializeField]private Vector3 m_Movement;
	[SerializeField]private Rigidbody m_PlayerRigidbody;
	[SerializeField]private bool isJump;
	[SerializeField]private bool isOnGround;
	[SerializeField]private float m_JumpForce = 5f;

	private void Awake () {
		m_PlayerRigidbody = GetComponent<Rigidbody> ();

	}

	private void FixedUpdate () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		Move(h, v);
		


		isJump = Input.GetKeyDown(KeyCode.Space);
		if(isJump){
			Jump(); 
		} 
	}
	void Move(float h, float v){
		m_Movement.Set(h, 0f, v);
		m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
		m_PlayerRigidbody.MovePosition(transform.position + m_Movement);
	}
	void Jump(){
		if (isOnGround){
			m_PlayerRigidbody.velocity = new Vector3(m_PlayerRigidbody.velocity.x, 0, m_PlayerRigidbody.velocity.z);
			m_PlayerRigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
			isOnGround = false;
		}
		
	}
	private void OnCollisionEnter(Collision m_Ground) {
		isOnGround = true;
	}
}