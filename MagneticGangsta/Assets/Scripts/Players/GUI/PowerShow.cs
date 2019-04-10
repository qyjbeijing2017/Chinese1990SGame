using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerShow : MonoBehaviour
{
    [SerializeField] Power m_power;
    [SerializeField] GameObject m_powerImage;
    [SerializeField] Slider m_powerSlider;

    [SerializeField] Image m_bg;
    [SerializeField] Image m_filled;
    [SerializeField] Color m_normal;
    [SerializeField] Color m_effect;
    [SerializeField] float m_effectSpeed;
    [SerializeField] float m_disappearSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //m_powerImage.SetActive(false);
        m_powerSlider.value = 1.0f;
        m_power.OnEmptyRecoveryStart += () => { StartCoroutine("PowerRecovery"); };
        m_power.OnEmptyRecoveryEnd += () =>
        {
            StopCoroutine("PowerRecovery");
            m_bg.color = m_normal;
            m_filled.color = m_normal;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (m_power.PowerNow < m_power.MaxPower)
        {
            //StopCoroutine("PowerEndShow");
            //m_powerImage.SetActive(true);
            m_bg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            m_filled.color = m_bg.color;

        }
        else
        {
            //StartCoroutine("PowerEndShow");
        }

        m_powerSlider.value = m_power.PowerNow / m_power.MaxPower;

        if (m_power.transform.localScale.x == -1)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }
    }

    IEnumerator PowerRecovery()
    {
        float lerpC = 1.0f;
        while (true)
        {
            while (lerpC > 0)
            {
                m_bg.color = Color.Lerp(m_effect, m_normal, lerpC);
                m_filled.color = m_bg.color;
                lerpC -= m_effectSpeed * Time.deltaTime;
                yield return null;
            }
            while (lerpC < 1)
            {
                m_bg.color = Color.Lerp(m_effect, m_normal, lerpC);
                m_filled.color = m_bg.color;
                lerpC += m_effectSpeed * Time.deltaTime;
                yield return null;
            }


        }
    }


    IEnumerator PowerEndShow()
    {
        float a = 1.0f;
        while (a > 0.0f)
        {
            m_bg.color = new Color(1.0f, 1.0f, 1.0f, a);
            m_filled.color = m_bg.color;
            a -= m_disappearSpeed * Time.deltaTime;
            yield return null;
        }
        //m_powerImage.SetActive(false);
    }
}
