using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DaemonTools;

public class GameConsole : UIBase
{
    [SerializeField] InputField m_input;
    [SerializeField] Text m_textShow;
    public delegate void OnInputHandler(string t);
    public event OnInputHandler OnInput;
    List<string> lines = new List<string>();
    int lineNow = 0;

    public override void close()
    {
        Time.timeScale = 1;
    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        m_input.ActivateInputField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputText;
            m_textShow.text += m_input.text + "\n>";
            inputText = m_input.text;
            lines.Add(inputText);
            lineNow = lines.Count;
            m_input.text = string.Empty;
            m_input.ActivateInputField();
            if (null != OnInput)
                OnInput(inputText);            
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.close("GameConsole");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && lineNow > 0)
        {
            lineNow--;
            m_input.text = lines[lineNow];
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && lineNow < lines.Count-1)
        {
            lineNow++;
            m_input.text = lines[lineNow];
        }
    }

    public void Log(string log)
    {
        m_textShow.text += "<color=#F5F000>" + log + "</color>\n>";
    }

    public void LogError(string log)
    {
        m_textShow.text += "<color=#FF000>" + log + "</color>\n>";
    }

}
