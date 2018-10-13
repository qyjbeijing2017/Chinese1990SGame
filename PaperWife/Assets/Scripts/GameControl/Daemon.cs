using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaemonTools;
using UnityEngine.UI;

public class Daemon : MonoSingleton<Daemon>
{
    [SerializeField] Canvas m_canvasMain;
    new void Awake()
    {
        base.Awake();
        ConfigManager.Instance.InitUIConfig();
        UIManager.Instance.Init();
    }

    private void Start() {
        Instantiate(new Canvas());

    }

}
