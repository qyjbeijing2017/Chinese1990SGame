using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Level1ReplyConfig : BaseConfig
{
    public int ID;
    public Temperament.TemperamentType MainType;
    public string DialogTrue;
    public string DialogFalse;
    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
        if (m_data[1] != "1" || m_data[1] != "2" || m_data[1] != "3")
        {
            MainType = (Temperament.TemperamentType)Enum.Parse(typeof(Temperament.TemperamentType), m_data[1]);
        }
        else
        {
            int mainType = 0;
            int.TryParse(m_data[1], out mainType);
            MainType = (Temperament.TemperamentType)mainType;
        }
        DialogTrue = m_data[2];
        DialogFalse = m_data[3];
    }
}
