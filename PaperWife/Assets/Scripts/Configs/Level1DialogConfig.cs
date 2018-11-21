using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1DialogConfig : BaseConfig
{
    public int ID;
    public List<string> Dialog = new List<string>();
    public Temperament TemperamentData;


    public void InitConfig(List<string> m_data)
    {
        int.TryParse(m_data[0], out ID);
        for (int i = 0; i < 5; i++)
        {
            Dialog.Add(m_data[i + 2]);
        }

        string[] temperamentData = m_data[7].Split('#');
        float mad = 0.0f;
        float sad = 0.0f;
        float complacent = 0.0f;

        if (temperamentData.Length != 3)
        {
            Debug.Log("情绪量出错 ID:"+ID);
        }
        else
        {
            float.TryParse(temperamentData[0], out mad);
            float.TryParse(temperamentData[1], out sad);
            float.TryParse(temperamentData[2], out complacent);
        }
        TemperamentData = new Temperament(mad, sad, complacent);
    }
}

public struct Temperament
{
    public enum TemperamentType
    {
        Gentle = 0,
        Mad = 1,
        Sad = 2,
        Complacent = 3,
        LoseMad = 4,
        LoseSad = 5,
        LoseComplacent = 6
    }

    Vector3 emotionData;

    public Vector3 EmotionVec3
    {
        get
        {
            return emotionData;
        }
        set
        {
            emotionData = value;
            m_mainType = GetMain();
        }
    }

    public float Mad
    {
        get
        {
            return emotionData.x;
        }
        set
        {
            emotionData.x = value;
            m_mainType = GetMain();
        }
    }
    public float Sad
    {
        get
        {
            return emotionData.y;
        }
        set
        {
            emotionData.y = value;
            m_mainType = GetMain();
        }
    }
    public float Complacent
    {
        get
        {
            return emotionData.z;
        }
        set
        {
            emotionData.z = value;
            m_mainType = GetMain();
        }
    }

    /// <summary>
    /// 返回一个Gentle性格
    /// </summary>
    /// <returns></returns>
    public static Temperament Gentle
    {
        get
        {
            return new Temperament(0, 0, 0);
        }
    }
  
    TemperamentType m_mainType;
    /// <summary>
    /// 主情绪
    /// </summary>
    public TemperamentType MainType
    {
        get
        {
            return m_mainType;
        }
    }
    /// <summary>
    /// 性格
    /// </summary>
    /// <param name="mad">愤怒值</param>
    /// <param name="sad">悲伤值</param>
    /// <param name="complacent">踌躇满志值</param>
    public Temperament(float mad, float sad, float complacent)
    {
        emotionData = new Vector3(mad, sad, complacent);
        m_mainType = TemperamentType.Gentle;
        m_mainType = GetMain();
    }

    TemperamentType GetMain()
    {
        TemperamentType type = TemperamentType.Gentle;
        if (Mad > Sad)
        {
            if (Complacent > Mad)
            {
                type = TemperamentType.Complacent;
            }
            else if (Complacent == Mad)
            {
                type = TemperamentType.LoseSad;
            }
            else
            {
                type = TemperamentType.Mad;
            }
        }
        else if (Mad == Sad)
        {
            if (Complacent > Mad)
            {
                type = TemperamentType.Complacent;
            }
            else if (Complacent == Mad)
            {
                type = TemperamentType.Gentle;
            }
            else
            {
                type = TemperamentType.LoseComplacent;
            }
        }
        else
        {
            if (Complacent > Sad)
            {
                type = TemperamentType.Complacent;
            }
            else if (Complacent == Sad)
            {
                type = TemperamentType.LoseMad;
            }
            else
            {
                type = TemperamentType.Sad;
            }
        }

        return type;
    }

    /// <summary>
    /// 返回ta与tb相似程度
    /// </summary>
    /// <param name="ta">from</param>
    /// <param name="tb">to</param>
    /// <returns></returns>
    static float Like(Temperament ta, Temperament tb)
    {
        float angle = Vector3.Angle(ta.EmotionVec3, tb.EmotionVec3);
        return 1.0f - (angle / 180.0f);
    }


}

