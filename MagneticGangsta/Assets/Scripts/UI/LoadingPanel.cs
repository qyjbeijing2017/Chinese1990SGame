using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class LoadingPanel : UIBase
{
    [SerializeField] Text m_loadingText;
    string m_loadingTextStart;
    public override void close()
    {

    }

    public override void show(bool isfirstOpen, object[] value)
    {
        if (isfirstOpen)
        {
            m_loadingTextStart = m_loadingText.text;
        }
    }


    // Update is called once per frame
    void Update()
    {
        m_loadingText.text = m_loadingTextStart + LoadSceneManager.Instance.LoadProgress * 100 + "%";
    }
}
