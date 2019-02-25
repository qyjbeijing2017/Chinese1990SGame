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
        
    }

    private void Start()
    {
        //UIManager.Instance.Open("StartPanel");
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

    public Daemon Init()
    {
        return Instance;
    }
    /// <summary>
    /// console响应
    /// </summary>
    /// <param name="input"></param>
    private void OnConsoleInput(string input)
    {

        string[] inputs = input.Split(' ');




        if (ConsoleObjs.ContainsKey(inputs[0]))
        {
            object obj = ConsoleObjs[inputs[0]];
            ReadConsoleInput(obj, inputs, 1);
        }

    }
    /// <summary>
    /// 读取数据运行方法或者读取属性
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="input"></param>
    /// <param name="index"></param>
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
            object[] value = new object[parameters.Length]; 
            for (int i = 0; i < parameters.Length; i++)
            {
                if (index + 1 + i < input.Length)
                {
                    string inputnow = input[index + 1 + i];

                    if (parameters[i].ParameterType.Name == "Int32")
                    {
                        int vi = 0;
                        int.TryParse(inputnow, out vi);
                        value[i] = vi;
                        
                    }
                    else if (parameters[i].ParameterType.Name == "Single")
                    {
                        float vf = 0;
                        float.TryParse(inputnow, out vf);
                        value[i] = vf;
                        
                    }
                    else if (parameters[i].ParameterType.Name == "Boolean")
                    {
                        bool vb = false;
                        bool.TryParse(inputnow, out vb);
                        value[i] = vb;
                    }
                    else if (parameters[i].ParameterType.Name == "Vector3")
                    {
                        Vector3 v3 = Vector3.zero;
                        string[] st = inputnow.Split('#');
                        if (st.Length == 3)
                        {
                            float.TryParse(st[0], out v3.x);
                            float.TryParse(st[1], out v3.y);
                            float.TryParse(st[2], out v3.z);
                            value[i] = v3;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (parameters[i].ParameterType.Name == "Polarity")
                    {
                        try
                        {
                            Polarity polarity = Polarity.None;
                            polarity = (Polarity)Enum.Parse(typeof(Polarity), inputnow);
                            value[i] = polarity;
                        }
                        catch
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            tm.Invoke(obj, value);
            console.Log("run:" + tm.Name);
            return;

        }

        FieldInfo tp = t.GetField(input[index]);
        if (null != tp)
        {
            if (index+1 < input.Length)
            {
                if (tp.FieldType.Name == "Int32")
                {
                    int vi = 0;
                    int.TryParse(input[index + 1], out vi);
                    tp.SetValue(obj, vi);
                    console.Log("set " + tp.Name + " to " + vi);
                }
                else if (tp.FieldType.Name == "Single")
                {
                    float vf = 0;
                    float.TryParse(input[index + 1], out vf);
                    tp.SetValue(obj, vf);
                    console.Log("set " + tp.Name + " to " + vf);
                }

                else if (tp.FieldType.Name == "Boolean")
                {
                    bool vb = false;
                    bool.TryParse(input[index + 1], out vb);
                    tp.SetValue(obj, vb);
                    console.Log("set " + tp.Name + " to " + vb);
                }
                else if (tp.FieldType.Name == "Vector3")
                {
                    Vector3 v3 = Vector3.zero;
                    string[] st = input[index + 1].Split('#');
                    if (st.Length == 3)
                    {
                        float.TryParse(st[0], out v3.x);
                        float.TryParse(st[1], out v3.y);
                        float.TryParse(st[2], out v3.z);
                        tp.SetValue(obj, v3);
                        console.Log("set " + tp.Name + " to " + v3);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (tp.FieldType.Name == "Polarity")
                {
                    try
                    {
                        Polarity polarity = Polarity.None;
                        polarity = (Polarity)Enum.Parse(typeof(Polarity), input[index + 1]);
                        tp.SetValue(obj, polarity);
                        console.Log("set " + tp.Name + " to " + polarity.ToString());
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {
                    object objNew = tp.GetValue(obj);
                    ReadConsoleInput(objNew, input, index + 1);

                    return;
                }


            }
            else
            {
                console.Log(tp.Name + ": " + tp.GetValue(obj).ToString());
                return;
            }

        }

        
        
    }


    


}
