using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seat : MonoBehaviour
{

    public UnityAction OnEachOther;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButton("Each Other"))
            {
                if (OnEachOther != null)
                    OnEachOther();
            }
        }
    }
}
