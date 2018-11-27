using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatTrigger : MonoBehaviour {
    [SerializeField]Seat seat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        seat.OnTriggerStay(other);
    }
}
