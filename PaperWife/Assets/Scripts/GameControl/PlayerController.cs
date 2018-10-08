using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerCharacter m_Character;
    private void Start()
    {
        m_Character = GetComponent<PlayerCharacter>();

    }
    void FixedUpdata()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        m_Character.Move(new Vector3(h, 0, v));
        
    }
}
