﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	public GameObject m_now;
	public GameObject m_before;

	private void Start () {
		m_targetMask = LayerMask.GetMask ("Target");
		m_now.SetActive (true);
		m_before.SetActive (false);
	}
	private void Update () {
		Pick ();
	}

	#region pick实现

	void onpickcolliderEnter (Collision other) {
		m_target.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		m_target.transform.SetParent (null);
		PickObj pickObj = m_target.GetComponent<PickObj> ();
		pickObj.IOnCollisionEnter -= onpickcolliderEnter;
		Destroy (pickObj);
		m_target = null;
		m_isPicked = false;
	}

	int m_targetMask;
	private GameObject m_target;
	[SerializeField] private Camera _playerCamera;
	[SerializeField] private float m_distance = 50f;
	private bool m_isPicked = false;

	void Pick () {
		if (!m_isPicked) {
			Ray m_ray = _playerCamera.ScreenPointToRay (new Vector2 (Screen.width / 2, Screen.height / 2));
			RaycastHit m_hit;
			if (Physics.Raycast (m_ray, out m_hit, m_distance, m_targetMask)) {
				if (Input.GetKeyDown (KeyCode.E)) {
					m_target = m_hit.transform.gameObject;
					PickObj pickObj = m_target.AddComponent<PickObj> ();
					pickObj.IOnCollisionEnter += onpickcolliderEnter;
					m_target.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
					m_target.transform.SetParent (_playerCamera.transform);
					m_isPicked = true;
					m_now.SetActive (false);
					m_before.SetActive (true);
				}

			}
		} else {
			if (Input.GetKeyDown (KeyCode.E)) {
				m_target.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				m_target.transform.SetParent (null);
				PickObj pickObj = m_target.GetComponent<PickObj> ();
				pickObj.IOnCollisionEnter -= onpickcolliderEnter;
				Destroy (pickObj);
				m_target = null;
				m_now.SetActive (true);
				m_before.SetActive (false);
				m_isPicked = false;
			}
		}
	}
	#endregion
}