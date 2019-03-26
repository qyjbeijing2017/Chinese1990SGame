using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelTimer : MonoBehaviour
{
    public CDBase LevelTime;
    [SerializeField] public float ChangeColorTime;
    [SerializeField] private Image m_progress;
    [SerializeField] private Text m_progressText;
    [SerializeField] private Color m_textStartColor;
    [SerializeField] private Color m_imageStartColor;
    [SerializeField] private Color m_textEndColor;
    [SerializeField] private Color m_imageEndColor;

    private void Start()
    {
        LevelTime.Start();
    }

    private void Update()
    {
        ImageShow();
        TextShow();
        ChangeColor();
    }


    void ChangeColor()
    {
        if ((1 - LevelTime.CD) * LevelTime.CDTime <= ChangeColorTime)
        {
            m_progressText.color = m_textEndColor;
            m_progress.color = m_imageEndColor;
        }
        else
        {
            m_progressText.color = m_textStartColor;
            m_progress.color = m_imageStartColor;
        }
    }
    void ImageShow()
    {
        m_progress.fillAmount = 1 - LevelTime.CD;
    }


    void TextShow()
    {
        float secondAll = LevelTime.CDTime * (1 - LevelTime.CD);
        float min = secondAll / 60.0f;
        float second = secondAll % 60.0f;

        string s_second;
        if (second < 10)
        {
            s_second = "0" + ((int)second).ToString();
        }
        else
        {
            s_second = ((int)second).ToString();
        }

        m_progressText.text = ((int)min).ToString() + ":" + s_second;
    }
}
