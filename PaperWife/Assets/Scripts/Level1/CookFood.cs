using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DaemonTools;

public class CookFood : MonoSingleton<CookFood>
{
    event UnityAction OnEachOther;
    // Use this for initialization

    public void SetEachOther(bool enable)
    {
        GetComponent<Collider>().enabled = enable;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(Input.GetButtonDown("Each Other"))
            {
                UIManager.Instance.Open("CookFoodPanel");
                if (OnEachOther != null)
                {
                    OnEachOther.Invoke();
                }
            }

        }
    }
}
