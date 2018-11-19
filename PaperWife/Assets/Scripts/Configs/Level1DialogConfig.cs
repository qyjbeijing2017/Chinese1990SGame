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


public struct Level1Disposition
{
    public float Angry;
    public float Sad;
    public float Complacent;
    public enum MainDisPosition
    {
        e_angry = 1,
        e_sad = 2,
        e_complacent = 3
    }
    public MainDisPosition mainDisPosition;

    public Level1Disposition(float angry,float sad, float complacent)
    {
        Angry = angry;
        Sad = sad;
        Complacent = complacent;

        if (Angry >= Sad)
        {
            if (Angry >= complacent)
            {
                mainDisPosition = MainDisPosition.e_angry;
            }
            else
            {
                mainDisPosition = MainDisPosition.e_complacent;
            }
        }
        else
        {
            if (Sad >= complacent)
            {
                mainDisPosition = MainDisPosition.e_sad;
            }
            else
            {
                mainDisPosition = MainDisPosition.e_complacent;
            }
        }

    }

}
