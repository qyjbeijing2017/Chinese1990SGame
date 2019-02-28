using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaemonTools;
using UnityEngine.UI;
using System;
using System.Reflection;

/// <summary>
/// 在开始界面调用Daemon.Instance.Init();
/// </summary>

public class Daemon : MonoSingleton<Daemon>
{

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //初始化配置表
        ConfigManager.Instance.InitUIConfig();
        //初始化UI控制
        UIManager.Instance.Init();

    }

    private void Start()
    {
        //初始化所有配置表
        ConfigManager.Instance.InitConfigManager();



        //遍历example
        var enumerator = ConfigManager.Instance.ConfigExampleData.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Debug.Log("ID:" + enumerator.Current.Value.ID + " ");
            Debug.Log("contentInt:" + enumerator.Current.Value.ContentInt + " ");
            Debug.Log("contentString:" + enumerator.Current.Value.ContentString + " ");
            Debug.Log("contentBool:" + enumerator.Current.Value.ContentBool + " ");
            Debug.Log("contentEnum:" + enumerator.Current.Value.contentEnum + " ");
            Debug.Log("contentFloat:" + enumerator.Current.Value.ContentFloat + " ");
        }
    }

    private void Update()
    {

    }


    public Daemon Init()
    {
        return Instance;
    }
}

