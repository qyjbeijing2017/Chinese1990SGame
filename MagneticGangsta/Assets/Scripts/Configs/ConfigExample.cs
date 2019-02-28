using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 所有具体的config类需要继承自BaseConfig，每个Config类代表一行数据。
/// 当写完整个类后，需要到ConfigManager去注册。
/// </summary>
public class ConfigExample : BaseConfig
{
    public enum configExamplenum
    {
        one = 1,
        two = 2,
        three = 3
    }
    public int ID = 0;
    public int ContentInt = 0;
    public string ContentString = "zero";
    public bool ContentBool = false;
    public configExamplenum contentEnum = configExamplenum.one;
    public float ContentFloat = 0.0f;

    /// <summary>
    /// 实现接口函数InitConfig用于数据从string转为正确类型
    /// </summary>
    /// <param name="m_data">传回一行数据</param>
    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
        int.TryParse(m_data[1], out ContentInt);
        ContentString = m_data[2];
        bool.TryParse(m_data[3], out ContentBool);
        contentEnum = (configExamplenum)Enum.Parse(typeof(configExamplenum), m_data[4]);
        float.TryParse(m_data[5], out ContentFloat);
    }
}
