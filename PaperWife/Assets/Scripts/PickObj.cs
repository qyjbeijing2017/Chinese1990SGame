using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickObj : MonoBehaviour {

	public delegate void OnCollisionEnterHandler(Collision other);
	public event OnCollisionEnterHandler IOnCollisionEnter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		IOnCollisionEnter.Invoke(other);
	}


	
}
