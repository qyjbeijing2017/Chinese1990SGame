using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Instance.Open("LoadingNormalPanel", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        UIManager.Instance.clear();
    }
}
