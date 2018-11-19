using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class Level1Control : MonoSingleton<Level1Control> {

    [SerializeField] Level1AI m_level1AI;
    List<Level1AI> level1AIs;

    public List<Transform> Seats;
    [HideInInspector]public List<bool> IsSeatEmpty;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Seats.Count; i++)
        {
            IsSeatEmpty.Add(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator Level1TechnologicalProcess()
    {
        yield return new WaitForSeconds(3);

    }


}
