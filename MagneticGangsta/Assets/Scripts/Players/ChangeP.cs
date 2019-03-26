using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeP : MonoBehaviour
{

    [SerializeField] PlayerBase player;
    [SerializeField] GameObject north;
    [SerializeField] GameObject sourth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (player.PlayerPolarity == Polarity.North)
        {
            north.SetActive(true);
            sourth.SetActive(false);
        }
        if (player.PlayerPolarity == Polarity.Sourth)
        {
            north.SetActive(false);
            sourth.SetActive(true);
        }

    }
}
