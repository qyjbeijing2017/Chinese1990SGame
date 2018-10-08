using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	
    public float m_length;
    public float m_Width;
    public float m_Distance;
    public Texture2D m_CrosshairTexture;

    private GUIStyle m_LineStyle;
    private Texture tex;
    
    void Start()
    {
        m_LineStyle = new GUIStyle();
        m_LineStyle.normal.background = m_CrosshairTexture;

    }
    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width - m_Distance) / 2 - m_length, (Screen.height - m_Width) / 2 , m_length , m_Width), tex, m_LineStyle);
        GUI.Box(new Rect((Screen.width + m_Distance) / 2 , (Screen.height - m_Width) / 2, m_length, m_Width), tex, m_LineStyle);
        GUI.Box(new Rect((Screen.width - m_Width) / 2, (Screen.height - m_Distance) / 2 - m_length, m_Width, m_length), tex, m_LineStyle);
        GUI.Box(new Rect((Screen.width - m_Width) / 2, (Screen.height + m_Distance) / 2 - m_length, m_Width, m_length), tex, m_LineStyle);
    }
}
