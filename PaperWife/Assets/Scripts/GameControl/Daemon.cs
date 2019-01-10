using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaemonTools;
using UnityEngine.UI;
using XLua;
using System;
using System.Reflection;



public class Daemon : MonoSingleton<Daemon>
{
    public LuaEnv Luaenv;
    public GameConsole console;

    new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        // Luaenv = new LuaEnv();
        // Luaenv.DoString("require 'XLuaMain'");
        ConfigManager.Instance.InitUIConfig();
        UIManager.Instance.Init();
        InitConsoleObjs();
    }

    private void Start()
    {
        UIManager.Instance.Open("StartPanel");
        UIManager.Instance.Open("GameConsole", false);
        UIManager.Instance.close("GameConsole");
        console = UIManager.Instance.GetUI<GameConsole>();
        console.OnInput += OnConsoleInput;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.Open("GameConsole", false);
        }
    }

    private void OnDestroy()
    {
        //Luaenv.Dispose();
    }


    public Dictionary<string, object> ConsoleObjs = new Dictionary<string, object>();

    public void InitConsoleObjs()
    {
        ConsoleObjs.Clear();
        for (int i = 0; i < MagnetLevelControl.Instance.Players.Count; i++)
        {
            ConsoleObjs.Add("player" + i.ToString(), MagnetLevelControl.Instance.Players[i]);
        }



    }



    private void OnConsoleInput(string input)
    {

        string[] inputs = input.Split(' ');




        if (ConsoleObjs.ContainsKey(inputs[0]))
        {
            object obj = ConsoleObjs[inputs[0]];
            ReadConsoleInput(obj, inputs, 1);
        }

    }

    private void ReadConsoleInput(object obj, string[] input, int index)
    {
        if (obj == null && index >= input.Length)
        {
            return;
        }
        Type t = obj.GetType();



        MethodInfo tm = t.GetMethod(input[index]);
        if (null !=tm)
        {
            ParameterInfo[] parameters = tm.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                print(parameters[i].ParameterType.Name);
            }
        }

    }


}
