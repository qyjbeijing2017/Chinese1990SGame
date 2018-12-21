using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayerController : MonoBehaviour {
	[SerializeField] private BoxCollider2D m_up;
	[SerializeField] private BoxCollider2D m_down;
	[SerializeField] private BoxCollider2D m_left;
	[SerializeField] private BoxCollider2D m_right;
	[SerializeField] private BoxCollider2D m_upperLeft;
	[SerializeField] private BoxCollider2D m_upperRight;
	[SerializeField] private BoxCollider2D m_lowerLeft;
	[SerializeField] private BoxCollider2D m_lowerRight;

	private Rigidbody2D m_rigidbody;
	[SerializeField] private int m_playerID = 1;
	[SerializeField] private int m_playerForce = 10;
	[SerializeField] private int m_magnetForce = 50;
	[SerializeField] private bool isPositive = true;

	[SerializeField] private int m_playerHPMax = 5;
	 [HideInInspector] public int m_playerHP;
	[SerializeField] private bool isdead = false;
	[SerializeField] private Transform m_reborn1;
	[SerializeField] private Transform m_reborn2;
	[SerializeField] private Transform m_reborn3;
	[SerializeField] private Transform m_reborn4;
	[SerializeField] private Transform m_deathXMin;
	[SerializeField] private Transform m_deathXMax;
	[SerializeField] private Transform m_deathYMin;
	[SerializeField] private Transform m_deathYMax;

	private string m_a;
	private string m_b;
	private string m_x;
	private string m_y;
	private string m_rB;
	private string m_horizontal;

	void Start () {
		m_playerHP = m_playerHPMax;
		m_rigidbody = this.GetComponent<Rigidbody2D> ();
		if (m_playerID == 1) {
			m_horizontal = "Horizontal1";
			m_a = "1A";
			m_b = "1B";
			m_x = "1X";
			m_y = "1Y";
			m_rB = "1RB";
		} else if (m_playerID == 2) {
			m_horizontal = "Horizontal2";
			m_a = "2A";
			m_b = "2B";
			m_x = "2X";
			m_y = "2Y";
			m_rB = "2RB";
		} else if (m_playerID == 3) {
			m_horizontal = "Horizontal3";
			m_a = "3A";
			m_b = "3B";
			m_x = "3X";
			m_y = "3Y";
			m_rB = "3RB";

		} else if (m_playerID == 4) {
			m_horizontal = "Horizontal4";
			m_a = "4A";
			m_b = "4B";
			m_x = "4X";
			m_y = "4Y";
			m_rB = "4RB";
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!isdead) {
			move ();
			TriggerOn ();
			PoleChange ();
			Death ();
		} else {
			Reborn ();
		}

	}

	void Update () {
		TriggerOff ();
	}

	//移动脚本
	void move () {
		m_rigidbody.AddForce (new Vector2 (Input.GetAxis (m_horizontal), 0) * m_playerForce);

	}

	//更改极性
	void PoleChange () {
		if (Input.GetButtonDown (m_rB)) {
			isPositive = !isPositive;
		}
	}

	//攻击（打开磁场）
	void TriggerOn () {

		if (Input.GetButtonDown (m_y)) {
			if (Input.GetButtonDown (m_x)) {
				m_upperLeft.enabled = true;
				return;
			}
			if ( Input.GetButton (m_b)) {
				m_upperRight.enabled = true;
				return;
			}
			m_up.enabled = true;
			return;
		}
		if ( Input.GetButtonDown (m_a)) {
			if ( Input.GetButtonDown (m_x)) {
				m_lowerLeft.enabled = true;
				return;
			}
			if ( Input.GetButtonDown (m_b)) {
				m_lowerRight.enabled = true;
				return;
			}
			m_down.enabled = true;
			return;
		}
		if ( Input.GetButtonDown (m_x)) {
			m_left.enabled = true;
			return;
		}
		if (Input.GetButtonDown (m_b)) {
			m_right.enabled = true;
			return;
		}

	}

	//磁场碰撞，同性相斥，异性相吸
	private void OnTriggerEnter2D (Collider2D Other) {
		if (Other.transform.CompareTag ("Player"))
			if (Other.GetComponent<MPlayerController> ().isPositive != isPositive)
				gameObject.GetComponent<Rigidbody2D> ().AddForce ((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce);
			else
				gameObject.GetComponent<Rigidbody2D> ().AddForce ((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce * -1f);

			//新加入可互动tag IronObject	
		else if (Other.transform.CompareTag ("ComeObject"))
			Other.GetComponent<Rigidbody2D> ().AddForce ((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce * -1f);
		else if (Other.transform.CompareTag("GoObject"))
			gameObject.GetComponent<Rigidbody2D> ().AddForce ((Other.transform.position - gameObject.transform.position).normalized * m_magnetForce);	
	}

	//人物自身碰撞


	//关闭磁场
	void TriggerOff () {
		 if (m_up.enabled == true) {
			m_up.enabled = false;
		}
		if (m_down.enabled == true) {
			m_down.enabled = false;
		}
		if (m_left.enabled == true) {
			m_left.enabled = false;
		}
		if (m_right.enabled == true) {
			m_right.enabled = false;
		}
		if (m_upperLeft.enabled == true) {
			m_upperLeft.enabled = false;
		}
		if (m_upperRight.enabled == true) {
			m_upperRight.enabled = false;
		}
		if (m_lowerLeft.enabled == true) {
			m_lowerLeft.enabled = false;
		}
		if (m_lowerRight.enabled == true) {
			m_lowerRight.enabled = false;
		}

	}

	//重生判定
	void Reborn () {
		int i = Random.Range (1, 5);
		if (i == 1) {
			gameObject.transform.position = m_reborn1.position;
			
		} 
		else if (i == 2) {
			gameObject.transform.position = m_reborn2.position;
		
		} 
		else if (i == 3) {
			gameObject.transform.position = m_reborn3.position;
			
		} 
		else if (i == 4) {
			gameObject.transform.position = m_reborn4.position;
			
		}
		isdead = false;
		m_playerHP = m_playerHPMax;
		gameObject.GetComponent<Rigidbody2D>().drag /= 10 * m_magnetForce;
	}

	//死亡判定
	void Death () {

		if (gameObject.transform.position.x < m_deathXMin.position.x || gameObject.transform.position.x > m_deathXMax.position.x ||
			gameObject.transform.position.y < m_deathYMin.position.y || gameObject.transform.position.y > m_deathYMax.position.y) {
			m_playerHP = 0;
		}
		Debug.Log(m_playerHP);
		if (m_playerHP <= 0) {
			isdead = true;
			m_rigidbody.drag *= 10 * m_magnetForce ;
		}
	}
	
}