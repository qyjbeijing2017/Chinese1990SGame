using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConsole : UIBase
{
    [SerializeField] InputField m_input;
    [SerializeField] Text m_textShow;
    
    public override void close()
    {
    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnInput();
        }
    }

    private void OnInput()
    {
        string inputText;
        m_textShow.text += m_input.text +"\n>";
        inputText = m_textShow.text;
        m_input.text = string.Empty;

    }
}
