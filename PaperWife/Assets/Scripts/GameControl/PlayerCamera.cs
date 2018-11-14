using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	[Range(0,5)] public float NoFollowCircledR;
	[Range(0,100)] public float EdgeUp;
	[Range(0,-100)] public float EdgeBottom;
	[Range(0,100)] public float EdgeRight;
	[Range(0,-100)] public float EdgeLeft;
	[SerializeField,Range(0,4)] private float m_followSpeed;

	public Transform Player;
	public Vector3 FollowPosition;
	public Vector3 Follow2Camera;

	public bool IsFollow = true;

	// Use this for initialization
	void Start () {
		FollowPosition = Player.position;
		Follow2Camera = transform.position - FollowPosition;
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		CameraFollow();
	}
	void CameraFollow(){
		if ((Player.position - FollowPosition).magnitude > NoFollowCircledR)
		{
			float dic = (Player.position - FollowPosition).magnitude - NoFollowCircledR;
			float followSpeed = m_followSpeed * dic * Time.fixedDeltaTime;
			Vector3 followdic = (Player.position - FollowPosition).normalized;
			FollowPosition += followdic * followSpeed;
			
			if(FollowPosition.z > EdgeUp){
				FollowPosition = new Vector3(FollowPosition.x, FollowPosition.y, EdgeUp);
			}else if(FollowPosition.z < EdgeBottom)
			{
				FollowPosition = new Vector3(FollowPosition.x, FollowPosition.y, EdgeBottom);
			}
			if(FollowPosition.x > EdgeRight){
				FollowPosition = new Vector3(EdgeRight, FollowPosition.y, FollowPosition.z);
			}else if (FollowPosition.x < EdgeLeft){
				FollowPosition = new Vector3(EdgeLeft, FollowPosition.y, FollowPosition.z);
			}
		}
		transform.position = FollowPosition + Follow2Camera;
	}
}
