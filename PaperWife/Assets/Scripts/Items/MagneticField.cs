using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    /// <summary>
    /// 磁场的圆形碰撞
    /// </summary>
    CircleCollider2D m_collider;
    /// <summary>
    /// 磁场是否在开启中
    /// </summary>
    bool m_isMagnetic = false;
    /// <summary>
    /// 磁场发出者
    /// </summary>
    public MagneticItem magneticItem;
    /// <summary>
    /// 磁场持续时间
    /// </summary>
    public float MagneticTime;
    /// <summary>
    /// 磁场强度
    /// </summary>
    public float MagneticForce;
    /// <summary>
    /// 磁场系数
    /// </summary>
    public float MagneticCoefficient;
    /// <summary>
    /// 是否开启磁场距离衰减
    /// </summary>
    public bool MagnetDecrease = true;
    /// <summary>
    /// 磁场发射冷却时间
    /// </summary>
    public float MagneticCDTime;
    /// <summary>
    /// 磁场发射冷却状态
    /// </summary>
    public float MagneticCD
    {
        get
        {
            return m_magneticCD;
        }
    }
    /// <summary>
    /// 反射系数
    /// </summary>
    [Range(0,1)]public float ReatctionForceCoefficient;


    [SerializeField]private GameObject UI;


    void Start()
    {
        gameObject.layer = 12;                                                               // 设置物理层为12层，磁场层
        m_collider = GetComponent<CircleCollider2D>();                                       // 查找圆形碰撞
        m_collider.enabled = false;                                                          // 将碰撞关闭

        UI.transform.localScale = new Vector3(m_collider.radius, m_collider.radius, m_collider.radius);
        UI.SetActive(false);
    }

    /// <summary>
    /// 让磁场打开
    /// </summary>
    public void OnMagneticStart()
    {
        if (!m_isMagnetic && m_magneticCD >= 1)
        {
            m_collider.enabled = true;
            StartCoroutine("OnMagnetic");
            m_isMagnetic = true;
            m_magneticCD = 0;
            UI.SetActive(true);
        }
    }

    /// <summary>
    /// 磁场开启计时
    /// </summary>
    /// <returns></returns>
    IEnumerator OnMagnetic()
    {
        yield return new WaitForSeconds(MagneticTime);
        m_collider.enabled = false;
        m_isMagnetic = false;
        UI.SetActive(false);
    }

    /// <summary>
    /// 磁场强行关闭
    /// </summary>
    public void MagneticOff()
    {
        if (m_isMagnetic)
        {
            StopCoroutine("OnMagnetic");
            m_collider.enabled = false;
            m_isMagnetic = false;
            UI.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            // 查找可吸引物体接口,如果没有直接跳转catch
            MagneticItem magneticItemOther = collision.GetComponent<MagneticItem>();


            // 给被磁场作用物体发出消息
            MagneticData md = new MagneticData();                                                                               // 新建一个数据类
            md.OriginPosition = transform.position;                                                                             // 设置发出者为自身
            md.Polarity = magneticItem.PolarityMy;                                                                              // 设置发出者磁场
            Vector3 origin2target = transform.position - collision.transform.position;                                              // 计算发出者到接收者的位置向量
            Vector3 force = Vector3.zero;
            if (MagnetDecrease)
            {
                force = Mathf.Pow(origin2target.magnitude / m_collider.radius, MagneticCoefficient) * MagneticForce * origin2target.normalized;             // 计算一个从发出者到接收者的向量力
            }
            else
            {
                force = MagneticForce * origin2target.normalized;
            }
            md.Mforce = force;
            magneticItemOther.OnMagnetic(md);                                                                                   // 向被作用物体发出消息并传递数据

            // 给磁场发出者发消息，因为可以看做是被磁场作用物体给磁场发出者相同的力，所以与上一部分赋值相反
            MagneticData mdMy = new MagneticData();
            mdMy.OriginPosition = collision.transform.position;
            mdMy.Polarity = magneticItemOther.PolarityMy;
            mdMy.Mforce = -force * ReatctionForceCoefficient;
            mdMy.IsReactionForce = true;                                                                                         // 设置为反作用力 
            magneticItem.OnMagnetic(mdMy);
        }
        catch (System.Exception e)
        {
            //异常处理，往往是因为磁场物理层碰到了非可吸引物体。
            Debug.LogError(e.Message + " name:"+ collision.name);
            
        }
    }


    //冷却计时
    [SerializeField]private float m_magneticCD = 1.0f;
    private float m_CDtimer = 0.0f;
    private void FixedUpdate()
    {
        if (m_magneticCD < 1)
        {
            if (m_CDtimer >= MagneticCDTime)
            {
                m_CDtimer = 0;
                m_magneticCD = 1;
            }
            else
            {
                m_magneticCD = m_CDtimer / MagneticCDTime;
                m_CDtimer += Time.fixedDeltaTime;
            }
        }
    }
}
