using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [HideInInspector] public MPlayerController Player;
    [SerializeField] private Slider m_sliderHP;
    [SerializeField] private Slider m_power;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Image m_powerImage;

    // Update is called once per frame
    void Update()
    {
        m_sliderHP.value = (float)Player.PlayerHP / (float)Player.m_playerHPMax;
        m_power.value = Player.m_playerPower.PowerNow / Player.m_playerPower.MaxPower;
        if (Player.m_playerPower.IsWaitRepowerEnd)
        {
            m_animator.speed = 1;
        }
        else
        {
            m_animator.speed = 0;
            m_powerImage.color = new Color(1, 1, 1, 1);
        }
    }
}
