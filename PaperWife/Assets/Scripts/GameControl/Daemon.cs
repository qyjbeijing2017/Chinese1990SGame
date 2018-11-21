using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaemonTools;
using UnityEngine.UI;
using XLua;




public class Daemon : MonoSingleton<Daemon>
{
    public LuaEnv Luaenv;

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Luaenv = new LuaEnv();
        Luaenv.DoString("require 'XLuaMain'");
        ConfigManager.Instance.InitUIConfig();
        UIManager.Instance.Init();
    }


    private void Start() {
        UIManager.Instance.Open("StartPanel");
        
    }

    private void OnDestroy()
    {
        Luaenv.Dispose();
    }

}
