using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class TestConfig : BaseConfig
{
    public int ID;
    public float Test1;
    public string Test2;
    public int Test3;
    public override void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
        float.TryParse(m_data[1], out Test1);
        Test2 = m_data[2];
        int.TryParse(m_data[3], out Test3);
    }
}
