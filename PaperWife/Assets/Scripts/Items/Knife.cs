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
		if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude != 0 ){
			m_knifeTrigger.enabled = true;
		}
		else {
			m_knifeTrigger.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other) {
		
		if(other.transform.CompareTag("Player")){
			other.GetComponent<MPlayerController>().m_playerHP --;
		}
		
	}

	

	//刀子有两个box collider，一个写ontriggerenter，一个写oncollisionenter，刀子碰玩家是trigger，刀子碰地面是collider（设置物理层），刀子速度为0时无伤害，刀子有速度时有伤害
}
