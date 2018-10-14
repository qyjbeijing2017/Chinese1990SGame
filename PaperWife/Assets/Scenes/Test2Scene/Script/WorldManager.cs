using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    static bool loadPlayer = false;
    Player2World player;

    // Use this for initialization
    void Start () {
        if (!loadPlayer)
        {
            Instantiate(player.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
