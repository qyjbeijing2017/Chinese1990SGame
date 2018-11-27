using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class AboutPanel :UIBase {
    [SerializeField] Button _backButton;

    public void AddEvent()
    {
        _backButton.onClick.AddListener(OnBackClick);
    }

    public void OnBackClick()
    {
        UIManager.Instance.close();
    }

    public override void close()
    {
        
    }

    public override void show(bool IsfirstOpen, object[] value)
    {
        if (IsfirstOpen)
        {
            AddEvent();
        }
    }


}
