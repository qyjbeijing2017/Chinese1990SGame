using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.SceneManagement;

public class Player2World : MonoBehaviour{
    enum WorldState
    {
        Now = 0,
        Before = 1,
    }
    [SerializeField] GameObject nowWorld;
    [SerializeField] GameObject BeforeWorld;

    [SerializeField]WorldState worldState;

    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            worldChange();
        }
		
	}

    void worldChange()
    {
        switch (worldState)
        {
            case WorldState.Now:
                nowWorld.SetActive(false);
                BeforeWorld.SetActive(true);
                worldState = WorldState.Before;
                break;
            case WorldState.Before:
                nowWorld.SetActive(true);
                BeforeWorld.SetActive(false);
                worldState = WorldState.Now;
                break;
            default:
                break;
        }
    }
}
