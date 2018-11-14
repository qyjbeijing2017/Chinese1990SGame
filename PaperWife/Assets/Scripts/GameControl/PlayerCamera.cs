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
	[HideInInspector]public Vector3 FollowPosition;
	[HideInInspector]public Vector3 Follow2Camera;



	public bool IsFollow = true;

	[SerializeField] private bool m_isDebug = true;
	[SerializeField] Color m_debugColor;
	// Use this for initialization
	void Start () {
		FollowPosition = Player.position;
		Follow2Camera = transform.position - FollowPosition;
	}
	
	// Update is called once per frame
	void Update() {
		DebugLine();
	}
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
	void DebugLine(){
		Debug.DrawLine(new Vector3(EdgeLeft, 0.3f, EdgeUp), new Vector3(EdgeRight, 0.3f, EdgeUp), m_debugColor);
		Debug.DrawLine(new Vector3(EdgeRight, 0.3f,EdgeUp), new Vector3(EdgeRight, 0.3f, EdgeBottom), m_debugColor); 
		Debug.DrawLine(new Vector3(EdgeRight, 0.3f, EdgeUp), new Vector3(EdgeLeft, 0.3f, EdgeUp), m_debugColor);
		Debug.DrawLine(new Vector3(EdgeLeft, 0.3f, EdgeBottom), new Vector3(EdgeRight, 0.3f, EdgeBottom), m_debugColor);
	}
	void DebugCircle(Vector3 center, Vector3 normal, float radius){
		int i = 0;
		float a = 20.0f;
		Vector3 startVec = new Vector3(1, 0, 0);
		List<Vector3> dots = new List<Vector3>();
		float angle = 360.0f / a;
		if (normal.normalized != new Vector3(0, 1, 0) && normal.normalized != new Vector3(0, -1, 0)){
			startVec = Vector3.Cross(normal, new Vector3(0, 1, 0)).normalized;
		}
		while(i<a){
			Vector3 dot = Quaternion.AngleAxis(i * angle, normal ) * startVec * radius + center;
			dots.Add(dot);
			i++;
		}
		for (int j = 1; j < dots.Count; j++){
			Debug.DrawLine(dots[j - 1], dots[j], m_debugColor);
		}
		Debug.DrawLine(dots[dots.Count - 1], dots[0], m_debugColor);
	}
}
