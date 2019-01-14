using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MagneticItem{
    /// <summary>
    /// 可吸引物体磁性
    /// </summary>
    Polarity PolarityMy
    {
        get;
    }
    /// <summary>
    /// 属性、类型
    /// </summary>
    MagneticType Type
    {
        get;
    }


    /// <summary>
    /// 被吸引时触发
    /// </summary>
    /// <param name="md">吸引数据</param>
    void OnMagnetic(MagneticData md);
}

public class MagneticData
{
    /// <summary>
    /// 磁场发出者位置
    /// </summary>
    public Vector3 OriginPosition = Vector3.zero;
    /// <summary>
    /// 磁场发出者极性
    /// </summary>
    public Polarity Polarity = Polarity.None;
    /// <summary>
    /// 磁场力,带方向，为一个从发出者到自身的向量
    /// </summary>
    public Vector3 Mforce = Vector3.zero;
    /// <summary>
    /// 是否是反作用力
    /// </summary>
    public bool IsReactionForce = false;
    


    public MagneticType OriginType = MagneticType.None;

    public MagneticData Copy()
    {
        MagneticData md = new MagneticData();

        md.OriginPosition = OriginPosition;
        md.Polarity = Polarity;
        md.Mforce = Mforce;
        md.IsReactionForce = IsReactionForce;
        md.OriginType = OriginType;

        return md;
    }
}


public enum MagneticType
{
    None = 0,
    Player = 1,
    Item = 2,
    Edge = 3
}

public enum Polarity
{
    None = 0,
    North = 1,
    Sourth = 2
}