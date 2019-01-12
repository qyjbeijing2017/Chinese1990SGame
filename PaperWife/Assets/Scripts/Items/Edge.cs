using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour,MagneticItem
{
    public Polarity PolarityMy
    {
        get
        {
            return PolarityA;
        }

    }

    public MagneticType Type
    {
        get
        {
            return m_type;
        }
    }

    private MagneticType m_type = MagneticType.Edge;
    public Polarity PolarityA = Polarity.Sourth;
    public float BackSpeed;


    public void OnMagnetic(MagneticData md)
    {
        return;

    }

    /// <summary>
    /// 是否使用玩家磁场返回
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public bool UseMagnetic(bool b)
    {
        if (b)
        {
            gameObject.layer = 14;
        }
        else
        {
            gameObject.layer = 0;
        }
        return b;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        MPlayerController player = other.GetComponent<MPlayerController>();

        if (player && gameObject.layer != 14)
        {
            if (player)
            {
                if (!player.OnEdge())
                {
                    return;
                }
            }

            Rigidbody2D rigidbody2 = other.GetComponent<Rigidbody2D>();
            if (rigidbody2)
            {
                rigidbody2.velocity = BackSpeed * new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y).normalized;
            }
            
        }
    }

}
