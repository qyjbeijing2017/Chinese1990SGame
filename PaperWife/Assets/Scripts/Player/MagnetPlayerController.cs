using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagnetPlayerController : MonoBehaviour {
	[SerializeField]private BoxCollider2D m_up;
	[SerializeField]private BoxCollider2D m_down;
	[SerializeField]private BoxCollider2D m_left;
	[SerializeField]private BoxCollider2D m_right;
	[SerializeField]private BoxCollider2D m_upperLeft;
	[SerializeField]private BoxCollider2D m_upperRight;
	[SerializeField]private BoxCollider2D m_lowerLeft;
	[SerializeField]private BoxCollider2D m_lowerRight;
	
	private Rigidbody2D m_rigidbody;
	[SerializeField]private int m_force = 3; 
	[SerializeField]private List <bool> m_boxCollider2D = new List<bool>();
	void Start () {
		m_rigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move();
		ColliderOn();
	}
	void Update() {
		ColliderOff();
	}
	void move(){
		float h = Input.GetAxis("Horizontal");
		m_rigidbody.AddForce(new Vector2(h,0) * m_force);
	}
	//移动脚本
	void ColliderOn(){
		if(Input.GetKeyDown(KeyCode.I)){
			if(Input.GetKeyDown(KeyCode.J)){
				m_upperLeft.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.L)){
				m_upperRight.enabled = true;
				return;
			}
			m_up.enabled = true;
			return;
		}	
		if (Input.GetKeyDown(KeyCode.K)){
			if(Input.GetKeyDown(KeyCode.J)){
				m_lowerLeft.enabled = true;
				return;
			}
			if(Input.GetKeyDown(KeyCode.L)){
				m_lowerRight.enabled = true;
				return;
			}
			m_down.enabled = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.J)){
			m_left.enabled = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.L)){
			m_right.enabled = true;
			return;
		}
							
	}
	void ColliderOff(){
		if (m_up.enabled == true )
		{
			m_up.enabled = false;
		}
		if (m_down.enabled == true )
		{
			m_down.enabled = false;
		}
		if (m_left.enabled == true )
		{
			m_left.enabled = false;
		}
		if (m_right.enabled == true )
		{
			m_right.enabled = false;
		}
		if (m_upperLeft.enabled == true )
		{
			m_upperLeft.enabled = false;
		}
		if (m_upperRight.enabled == true )
		{
			m_upperRight.enabled = false;
		}
		if (m_lowerLeft.enabled == true )
		{
			m_lowerLeft.enabled = false;
		}
		if (m_lowerRight.enabled == true )
		{
			m_lowerRight.enabled = false;
		}
		//碰撞盒关闭
	
	}




}
