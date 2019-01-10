using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        UIManager.Instance.clear();
    }
}
