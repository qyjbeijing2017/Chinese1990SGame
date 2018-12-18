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
	[SerializeField]private int m_playerID = 1;
	[SerializeField]private int m_playerForce = 10; 
	[SerializeField]private int m_magnetForce = 30; 
	[SerializeField]private bool isPositive = true;
	 
	 private float m_joystickV;
	 private float m_joystickH;
	 private float m_joystickRT;


	/* [SerializeField]private List <Collider2D> m_boxCollider2D = new List<Collider2D>();

	enum MyCollider
	{
		none = 0,
		up = 1,
		down = 2,
		left = 3,
		right = 4,
		upperleft = 5,
		upperright = 6,
		lowerleft = 7,
		lowerright = 8,
	}
	 Dictionary<MyCollider, Collider2D> myCollider = new Dictionary<MyCollider, Collider2D>();*/
	void Start () {
		m_rigidbody = this.GetComponent<Rigidbody2D>();
		if (m_playerID == 1)
		{
			m_joystickH = Input.GetAxis("Joystick1H");
			m_joystickV = Input.GetAxis("Joystick1V");
			m_joystickRT = Input.GetAxis("Joystick1RT");
			return;
		}
		if (m_playerID == 2)
		{
			m_joystickH = Input.GetAxis("Joystick2H");
			m_joystickV = Input.GetAxis("Joystick2V");
			m_joystickRT = Input.GetAxis("Joystick2RT");
			return;
		}
		if (m_playerID == 3)
		{
			m_joystickH = Input.GetAxis("Joystick3H");
			m_joystickV = Input.GetAxis("Joystick3V");
			m_joystickRT = Input.GetAxis("Joystick3RT");
			return;
		}
		if (m_playerID == 4)
		{
			m_joystickH = Input.GetAxis("Joystick4H");
			m_joystickV = Input.GetAxis("Joystick4V");
			m_joystickRT = Input.GetAxis("Joystick4RT");
			return;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move();
	
		ColliderOn();
		
	}

	void Update() {
		ColliderOff();
	}


	//移动脚本
	void move(){
		float h = Input.GetAxis("Horizontal");
		m_rigidbody.AddForce(new Vector2(h,0) * m_playerForce);
		
	}

	//更改极性
	void Pole(bool isPositive){
		if (m_joystickRT > 0){
			isPositive = !isPositive;
		}
	}
	
	//攻击（打开磁场）
	void ColliderOn(){
		
		if(Input.GetKeyDown(KeyCode.I) || m_joystickV > 0){
			if(Input.GetKeyDown(KeyCode.J) || m_joystickH < 0){
				m_upperLeft.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.L) || m_joystickH > 0){
				m_upperRight.enabled = true;
				return;
			}
			m_up.enabled = true;
			return;
		}	
		if (Input.GetKeyDown(KeyCode.K) || m_joystickV < 0){
			if(Input.GetKeyDown(KeyCode.J) || m_joystickH < 0){
				m_lowerLeft.enabled = true;
				return;
			}
			if(Input.GetKeyDown(KeyCode.L) || m_joystickH > 0){
				m_lowerRight.enabled = true;
				return;
			}
			m_down.enabled = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.J) || m_joystickH < 0){
			m_left.enabled = true;
			return;
		}
		if(Input.GetKeyDown(KeyCode.L) || m_joystickH > 0){
			m_right.enabled = true;
			return;
		}
							
	}

	//同性相吸，异性相斥
	private void OnTriggerEnter2D(Collider2D Other) {
		if(Other.transform.CompareTag("Player"))
			if(Other.GetComponent<MagnetPlayerController>().isPositive != isPositive)
				gameObject.GetComponent<Rigidbody2D>().AddForce((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce);
			else
				gameObject.GetComponent<Rigidbody2D>().AddForce((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce * -1f);
				
		//新加入可互动tag IronObject	
		else if (Other.transform.CompareTag("IronObject"))
			Other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y) * m_magnetForce);
	
	}
	
	//关闭磁场
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
		/* var dicenumator = myCollider.GetEnumerator();
		while(dicenumator.MoveNext()){
			if (dicenumator.Current.Key == MyCollider.up){
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.down)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.down)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.left)
			{
				dicenumator.Current.Value.enabled = false;
			}	
			if (dicenumator.Current.Key == MyCollider.right)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.upperleft)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.upperright)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.lowerleft)
			{
				dicenumator.Current.Value.enabled = false;
			}
			if (dicenumator.Current.Key == MyCollider.lowerright)
			{
				dicenumator.Current.Value.enabled = false;
			}
		}*/
	
	}




}
