using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

	[SerializeField] private BoxCollider2D m_knifeTrigger;
	private MPlayerController m_playerController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		KnifeFly();
	}
	
	//判断刀子此时是否有速度，如果有，则可以伤害玩家
	void KnifeFly(){
		if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.01  ){
			m_knifeTrigger.enabled = true;
		}
		else {
			m_knifeTrigger.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.transform.CompareTag("Player")){
			other.GetComponentInParent<MPlayerController>().m_playerHP --;
			Debug.Log("Minus");
			
		}
		
	}
}
