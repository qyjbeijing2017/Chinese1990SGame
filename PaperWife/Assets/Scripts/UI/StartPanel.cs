using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaemonTools;

public class StartPanel : UIBase {
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _exitButton;

    void AddEvent()
    {
        _startButton.onClick.AddListener(OnStartClicked);
        _aboutButton.onClick.AddListener(OnAboutClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    void OnStartClicked()
    {
        LoadSceneManager.Instance.LoadSceneAsync("Level1",()=>{
        },() => {
            ConfigManager.Instance.InitConfigManager();       
        });
    }
    void OnAboutClicked()
    {
        UIManager.Instance.Open("AboutPanel");
    }
    void OnExitClicked()
    {
        Application.Quit();
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
