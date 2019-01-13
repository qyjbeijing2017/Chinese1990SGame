using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [HideInInspector]public MPlayerController Player;
    [SerializeField]private Slider m_sliderHP;
    [SerializeField] private Slider m_defenceCD;

    // Update is called once per frame
    void Update()
    {
        m_sliderHP.value = (float)Player.PlayerHP / (float)Player.m_playerHPMax;
        m_defenceCD.value = Player.PolaityDefenceCD;
    }
}
