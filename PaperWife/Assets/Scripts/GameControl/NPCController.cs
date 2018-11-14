using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
	[SerializeField] private Transform m_target;
	[SerializeField] private UnityEngine.AI.NavMeshAgent m_navMeshAgent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_navMeshAgent.SetDestination(m_target.position);
	}
}
