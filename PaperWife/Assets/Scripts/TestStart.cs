using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DaemonTools;

public class TestStart : MonoBehaviour
{
    [SerializeField] Button m_startGame;


    // Use this for initialization
    void Start()
    {
        m_startGame.onClick.AddListener(OnStartGameClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnStartGameClick()
    {
        DaemonTools.LoadSceneManager.Instance.LoadSceneAsync("GameStage1", () =>
        {
            Debug.Log("end");
            Debug.Log(ConfigManager.Instance.TestConfigData[1].ID);
//            Debug.Log(ConfigManager.Instance.Test10_11ConfigData[6].Fortest1);
        }, () =>
        {
            ConfigManager.Instance.InitConfigManager();
            Debug.Log("before");
        });
    }
}
