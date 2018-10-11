using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[AddComponentMenu("Control Script/FPS Input")]
public class CharacterController : MonoBehaviour {
	 [SerializeField]private float m_Speed = 6.0f;
	[SerializeField]private float m_Gravity = 0;
	private CharacterController m_CharacterController;
	private void Start() {
		m_CharacterController = GetComponent<CharacterController>();
	}
	private void FixedUpdate() {
		float h = Input.GetAxis("Horizontal") * m_Speed;
		float v = Input.GetAxis("Vertical") * m_Speed;
		Vector3 m_Movement = new Vector3(h , 0 , v);
		m_Movement = Vector3.ClampMagnitude(m_Movement, m_Speed);
		m_Movement.y = m_Gravity;
		m_Movement *= Time.deltaTime;
		m_Movement = transform.TransformDirection(m_Movement);
		//CharacterController.Move(m_Movement);
	}
	
	
}
