using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1DialogConfig : BaseConfig
{
    public int ID;

    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
    }
}

