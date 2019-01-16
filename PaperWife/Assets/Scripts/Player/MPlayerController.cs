using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class MPlayerController : MonoBehaviour, MagneticItem
{

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider2D;
    private bool victory = false;
    private bool release = false;//控制动画胜利和链条

    public Camera CharactorCamera;

    [SerializeField, Tooltip("人物id")] public int m_playerID = 1;
    [SerializeField, Tooltip("移动力")] public float m_playerForce = 10;
    [SerializeField, Tooltip("最大移动速度")] public float m_maxMoveSpeed = 10;
    [SerializeField, Tooltip("跳跃速度")] public List<float> m_JumpsVelocity;
    [SerializeField, Tooltip("磁极")] public Polarity m_polarity = Polarity.North;
    [SerializeField, Tooltip("磁场")] public MagneticField m_magneticField;
    [SerializeField, Tooltip("最大生命")] public int m_playerHPMax = 5;
    [SerializeField, Tooltip("重生时间")] public float m_rebornTime = 3.0f;
    [SerializeField, Tooltip("磁场切换冷却")] public float DefenceCD = 3.0f;
    [SerializeField, Tooltip("磁场切换持续时间")] public float DefenceTime = 3.0f;
    /// <summary>
    /// 拉拽系数
    /// </summary>
    public float DrawCoefficient = 0.3f;
    /// <summary>
    /// 当前防御冷却
    /// </summary>
    public float PolaityDefenceCD
    {
        get
        {
            return m_PolaityDefenceCD;
        }
    }

    private int m_playerHP;
    /// <summary>
    ///  当前生命值
    /// </summary>
    [HideInInspector]
    public int PlayerHP
    {
        get { return m_playerHP; }
        set
        {
            m_playerHP = value;
            if (m_playerHP <= 0)
            {
                m_isdead = true;
                Destroy(this);
            }
        }
    }
    [SerializeField, Tooltip("是否死亡")] private bool m_isdead = false;
    public bool IsDead
    {
        get
        {
            return m_isdead;
        }
    }

    private Animator m_animator;
    private Animator particleanimator;


    public bool IsDizz
    {
        get
        {
            return m_isDizz;
        }
    }
    private bool m_isDizz;
    public UnityAction OnDizz;
    public float DizzForce;
    public float PlayerDizzForce;
    public float DizzTime;
    public Vector3 LastFrameVelocity { get { return m_lastFrameVelocity; } }
    private Vector3 m_lastFrameVelocity;

    /// <summary>
    /// 锁定所有操作
    /// </summary>
    public bool m_lockOption;
    /// <summary>
    /// 边界返回次数
    /// </summary>
    public int EdgeBack;




    public MagneticType Type
    {
        get
        {
            return MagneticType.Player;
        }
    }


    public void Init()
    {
        m_playerHP = m_playerHPMax;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_collider2D = GetComponent<Collider2D>();
        particleanimator = transform.Find("Particle1").GetComponent<Animator>();
        m_magneticField.MagneticItem = this;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_lockOption && !m_isdead)
        {
            move();
        }

    }

    void Update()
    {
        if (!m_lockOption && !m_isdead && Time.timeScale != 0)
        {
            Animated();
            Defence();
            PolarityChange();
            //磁场触发
            if (Input.GetButtonDown("Attack" + m_playerID.ToString()))
            {
                m_magneticField.OnMagneticStart(MagneticField.FieldType.Circle);
            }
            if (Input.GetButtonDown("HAttack" + m_playerID.ToString()))
            {
                m_magneticField.OnMagneticStart(MagneticField.FieldType.BoxH);
            }
            if (Input.GetButtonDown("VAttack" + m_playerID.ToString()))
            {
                m_magneticField.OnMagneticStart(MagneticField.FieldType.BoxV);
            }

        }

        m_lastFrameVelocity = m_rigidbody.velocity;
    }

    void Animated()//  控制动画基
    {
        m_animator.SetFloat("MoveSpeed", Mathf.Abs(m_rigidbody.velocity.x));
        m_animator.SetFloat("jumpspeed", Mathf.Abs(m_rigidbody.velocity.y));
        //if (m_polarity == Polarity.Sourth)
        //{
        //    m_animator.SetLayerWeight(1, 1);
        //}
        //else
        //{
        //    m_animator.SetLayerWeight(1, 0);
        //}


        if (m_rigidbody.velocity.x < -0.3f && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (m_rigidbody.velocity.x > 0.3f && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }


    int m_jumpNum = 0; //跳跃计数
    //移动脚本
    void move()
    {
        float Inputf = Input.GetAxis("Horizontal" + m_playerID.ToString()) * m_playerForce;
        if (Mathf.Abs(Input.GetAxis("Horizontal" + m_playerID.ToString())) <= 0.1f)
        {
            Inputf = Input.GetAxis("HorizontalKB" + m_playerID.ToString()) * m_playerForce;
        }

        if (m_rigidbody.velocity.x > m_maxMoveSpeed && Inputf > 0)
        {
            Inputf = 0;
        }

        if (m_rigidbody.velocity.x < -m_maxMoveSpeed && Inputf < 0)
        {
            Inputf = 0;
        }

        m_rigidbody.AddForce(new Vector3(Inputf, 0));



        if (Input.GetButtonDown("Jump" + m_playerID.ToString()))                                                                             // 多段跳跃
        {
            if (m_JumpsVelocity != null && m_jumpNum < m_JumpsVelocity.Count)
            {
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_JumpsVelocity[m_jumpNum]);
                m_jumpNum++;
            }
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)                                      //地面层为13层
        {
            m_jumpNum = 0;                                                         //重置跳跃次数
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 11 && InFight && !IsDefence)
        {
            Vector3 dir = collision.transform.position - transform.position;
            float force2 = Vector3.Dot(collision.gameObject.GetComponent<MPlayerController>().LastFrameVelocity, -dir.normalized);
            float force1 = Vector3.Dot(LastFrameVelocity, dir.normalized);
            float force = force1 + force2;
            if (force >= PlayerDizzForce)
            {
                OnDizzStart();
            }

        }
        else if (collision.gameObject.layer == 16 && InFight && !IsDefence)
        {
            Vector3 dir = collision.transform.position - transform.position;
            float force = Vector3.Dot(LastFrameVelocity, dir.normalized);
            if (force >= DizzForce)
            {
                OnDizzStart();
            }
        }
    }

    public void OnDizzStart()
    {
        m_isDizz = true;
        m_lockOption = true;
        StartCoroutine("Dizzing");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 1);
        m_rigidbody.velocity = Vector3.zero;
        if (OnDizz != null)
        {
            OnDizz.Invoke();
        }
    }

    IEnumerator Dizzing()
    {
        yield return new WaitForSeconds(DizzTime);
        m_isDizz = false;
        m_lockOption = false;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }



    float m_PolaityDefenceTimer = 0.0f;
    float m_PolaityDefenceCD = 1.0f;
    [SerializeField]bool IsDefence = false;
    //防御
    void Defence()
    {
        if (Input.GetButton("Defence" + m_playerID.ToString()) && m_PolaityDefenceCD == 1 && !IsDefence)
        {

            IsDefence = true;
            StartCoroutine("Defencing");

            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

        }
        if (Input.GetButtonUp("Defence" + m_playerID.ToString()) && IsDefence == true)
        {
            m_PolaityDefenceCD = 0.0f;
            m_PolaityDefenceTimer = 0.0f;
            IsDefence = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //if (m_polarity == Polarity.North)
            //{
            //    m_polarity = Polarity.Sourth;
            //}
            //else if (m_polarity == Polarity.Sourth)
            //{
            //    m_polarity = Polarity.North;
            //}
        }

        if (m_PolaityDefenceCD != 1)
        {
            if (m_PolaityDefenceTimer < DefenceCD)
            {
                m_PolaityDefenceTimer += Time.deltaTime;
                m_PolaityDefenceCD = m_PolaityDefenceTimer / DefenceCD;
            }
            else
            {
                m_PolaityDefenceCD = 1;
            }
        }

    }
    IEnumerator Defencing()
    {
        yield return new WaitForSeconds(DefenceTime);
        if (IsDefence == true)
        {
            m_PolaityDefenceCD = 0.0f;
            m_PolaityDefenceTimer = 0.0f;
            IsDefence = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //if (m_polarity == Polarity.North)
            //{
            //    m_polarity = Polarity.Sourth;
            //}
            //else if (m_polarity == Polarity.Sourth)
            //{
            //    m_polarity = Polarity.North;
            //}
        }

    }

    //磁场切换
    public void PolarityChange()
    {
        if (m_PolarityChangeCD == 1.0f && Input.GetButtonDown("Polarity" + m_playerID.ToString()))
        {
            if (PolarityMy == Polarity.North)
            {
                m_polarity = Polarity.Sourth;
            }
            else if (m_polarity == Polarity.Sourth)
            {
                m_polarity = Polarity.North;
            }
            m_PolarityChangeCD = 0.0f;
            StartCoroutine("PolarityChanging");

        }
    }

    private float m_PolarityChangeCD = 1.0f;
    public float PolarityChangeCD { get { return m_PolarityChangeCD; } }
    public float PolarityChangeTime;

    IEnumerator PolarityChanging()
    {
        float timer = 0.0f;

        while (m_PolarityChangeCD < 1.0f)
        {
            m_PolarityChangeCD = timer / PolarityChangeTime;
            if (m_PolarityChangeCD >1.0f)
            {
                m_PolarityChangeCD = 1.0f;
            }
            timer += Time.deltaTime;
            yield return 0;
        }
    }



    //我的极性
    public Polarity PolarityMy
    {
        get
        {
            return m_polarity;
        }
    }

    public PlatformEffector2D platformEffector;
    public void OnMagnetic(MagneticData md)
    {


        //if (md.OriginType == MagneticType.Edge)
        //{
        //    if (!OnEdge())
        //    {
        //        return;
        //    }
        //}

        // 如果吸引玩家的物体是没有极性的，不做任何处理，直接结束该方法。
        if (md.Polarity == Polarity.None)
        {
            return;
        }

        Vector3 force = Vector3.zero;
        if (md.Polarity != PolarityMy)
        {
            // 极性不同
            m_rigidbody.AddForce(md.Mforce * DrawCoefficient);                              // 异性相吸，Mforce为从我指向对手
            force = md.Mforce * DrawCoefficient;
        }
        else
        {
            // 极性相同
            m_rigidbody.AddForce(-md.Mforce);
            force = -md.Mforce;
        }

        if (md.IsReactionForce == false)
        {
            StopCoroutine("InFighting");
            StartCoroutine("InFighting");
            m_infight = true;
        }

        if (md.IsReactionForce == false && !platformEffector.useColliderMask && Vector3.Angle(force, Vector3.down) <= 70)
        {

            
            platformEffector.useColliderMask = true;
            StartCoroutine("CloseColliderEffect");
        }
    }

    bool m_infight = false;
    public bool InFight { get { return m_infight; } }
    public float InFightTime = 1.0f;
    IEnumerator InFighting()
    {
        yield return new WaitForSeconds(InFightTime);
        m_infight = false;
    }




    IEnumerator CloseColliderEffect()
    {
        yield return new WaitForSeconds(0.1f);
        platformEffector.useColliderMask = false;
    }

    //重生
    public void Reborn()
    {
        m_rigidbody.velocity = Vector3.zero;
        int rebornPositionNum = Random.Range(0, MagnetLevelControl.Instance.RebornPosition.Count);
        transform.position = MagnetLevelControl.Instance.RebornPosition[rebornPositionNum].position;
        gameObject.SetActive(true);
        m_magneticField.MagneticOff();
        StopAllCoroutines();
        m_jumpNum = 0;
        m_isDizz = false;
        m_PolaityDefenceCD = 1.0f;
        m_PolarityChangeCD = 1.0f;
        m_infight = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }

    /// <summary>
    /// 是否还能返回
    /// </summary>
    /// <returns></returns>
    public bool OnEdge()
    {
        if (EdgeBack > 0)
        {
            EdgeBack--;
            return true;
        }
        else
        {
            return false;
        }
    }


    //能量槽

    public float Power;
    public float AttackPowerCost;
    

}