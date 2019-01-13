using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerConfig : BaseConfig
{

    public float             PlayerForce                  = 15f;
    public float             MaxMoveSpeed                 = 10f;
    public List<float>       JumpsVelocity                = new List<float>() { 8, 12 };
    public Polarity          Polarity                     = Polarity.North;
    public int               PlayerHPMax                  = 5;
    public float             RebornTime                   = 1f;
    public float             MagneticChangeCD             = 3f;
    public float             MagneticChangeTime           = 3f;
    public float             MagneticTime                 = 0.1f;
    public float             MagneticForce                = 500f;
    public float             MagneticCoefficient          = 1f;
    public bool              MagnetDecrease               = true;
    public float             MagneticCDTime               = 1.0f;
    public float             ReatctionForceCoefficient    = 0.5f;
    public float             GScale                       = 1f;
    public float             DragGround                   = 0.3f;
    public float             DragRB                       = 0.5f;
    public float             DrawCoefficient                    = 0.3f;
    public int EdgeBackNum = 3;
    public float BackSpeed = 5.0f;
    public Polarity EdgePolarity;


    public void InitConfig(List<string> m_data)
    {
        if (m_data[0] == "Value")
        {
            float.TryParse(m_data[2], out PlayerForce);
            float.TryParse(m_data[3], out MaxMoveSpeed);

            string[] jumpLi = m_data[4].Split('#');
            JumpsVelocity.Clear();
            for (int i = 0; i < jumpLi.Length; i++)
            {
                float speed = 0f;
                float.TryParse(jumpLi[i], out speed);
                JumpsVelocity.Add(speed);
            }

            try
            {
                Polarity = (Polarity)Enum.Parse(typeof(Polarity), m_data[5]);
            }
            catch (Exception e)
            {

                Debug.LogError("enum error:" +e.Message);
            }

            int.TryParse(m_data[6], out PlayerHPMax);
            float.TryParse(m_data[7], out RebornTime);
            float.TryParse(m_data[8], out MagneticChangeCD);
            float.TryParse(m_data[9], out MagneticChangeTime);
            float.TryParse(m_data[10], out MagneticTime);
            float.TryParse(m_data[11], out MagneticForce);
            float.TryParse(m_data[12], out MagneticCoefficient);
            bool.TryParse(m_data[13], out MagnetDecrease);
            float.TryParse(m_data[14], out MagneticCDTime);
            float.TryParse(m_data[15], out ReatctionForceCoefficient);
            float.TryParse(m_data[16], out GScale);
            float.TryParse(m_data[17], out DragGround);
            float.TryParse(m_data[18], out DragRB);
            float.TryParse(m_data[19], out DrawCoefficient);
            int.TryParse(m_data[20], out EdgeBackNum);
            float.TryParse(m_data[21], out BackSpeed);
            try
            {
                EdgePolarity = (Polarity)Enum.Parse(typeof(Polarity), m_data[22]);
            }
            catch (Exception e)
            {

                Debug.LogError("enum error:" + e.Message);
            }


        }
    }

}
