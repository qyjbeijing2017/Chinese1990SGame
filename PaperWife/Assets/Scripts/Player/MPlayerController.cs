using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayerController : MonoBehaviour, MagneticItem
{

    private Rigidbody2D m_rigidbody;
    private bool victory = false;
    private bool release = false;//控制动画胜利和链条

    [SerializeField, Tooltip("人物id")] private int m_playerID = 1;
    [SerializeField, Tooltip("移动力")] private float m_playerForce = 10;
    [SerializeField, Tooltip("最大移动速度")] private float m_maxMoveSpeed = 10;
    [SerializeField, Tooltip("跳跃速度")] private List<float> m_JumpsVelocity;
    [SerializeField, Tooltip("磁极")] Polarity m_polarity = Polarity.North;
    [SerializeField, Tooltip("磁场")] MagneticField m_magneticField;
    [SerializeField, Tooltip("最大生命")] private int m_playerHPMax = 5;
    [SerializeField, Tooltip("重生时间")] public float m_rebornTime = 3;

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

    private Animator selfanimator;
    private Animator particleanimator;

    /// <summary>
    /// 锁定所有操作
    /// </summary>
    public bool m_lockOption;

    void Start()
    {
        m_playerHP = m_playerHPMax;
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        selfanimator = this.GetComponent<Animator>();
        particleanimator = transform.Find("Particle1").GetComponent<Animator>();
        m_magneticField.magneticItem = this;
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
        if (!m_lockOption && !m_isdead)
        {
            Animated();
            PolarityChange();
            //磁场触发
            if (Input.GetButtonDown("Attack" + m_playerID.ToString()))
            {
                m_magneticField.OnMagneticStart();
            }
        }
    }

    void Animated()//  控制动画基
    {
        float currentSpeed;
        
        if (m_rigidbody.velocity.x > 0)
            currentSpeed = m_rigidbody.velocity.x;
        else
            currentSpeed = -m_rigidbody.velocity.x;

        selfanimator.SetFloat("speed", currentSpeed);
        particleanimator.SetFloat("runspeed", currentSpeed);
        selfanimator.SetBool("victory", victory);
        selfanimator.SetBool("release", release);
        if (Input.GetKeyDown(KeyCode.V))
        {
            victory = !victory;
            Debug.Log("victory");
        }
        if (Input.GetKeyDown(KeyCode.R))
            release = !release;
        if (m_rigidbody.velocity.x >= 0.5)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            transform.Find("Particle1").eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }
        else
        {

            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            transform.Find("Particle1").eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
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

        if (m_rigidbody.velocity.x > m_maxMoveSpeed && Inputf>0)
        {
            Inputf = 0;
        }

        if (m_rigidbody.velocity.x < -m_maxMoveSpeed && Inputf<0)
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

    //更改极性
    void PolarityChange()
    {
        if (Input.GetButtonDown("Polarity" + m_playerID.ToString()))
        {
            if (m_polarity == Polarity.North)
            {
                m_polarity = Polarity.Sourth;
            }
            else if (m_polarity == Polarity.Sourth)
            {
                m_polarity = Polarity.North;
            }
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
    public void OnMagnetic(MagneticData md)
    {
        // 如果吸引玩家的物体是没有极性的，不做任何处理，直接结束该方法。
        if (md.Polarity == Polarity.None)
        {
            return;
        }

        if (md.Polarity != PolarityMy)
        {
            // 极性不同
            m_rigidbody.AddForce(md.Mforce);                              // 同性相吸，Mforce为从我指向对手
        }
        else
        {
            // 极性相同
            m_rigidbody.AddForce(-md.Mforce);
        }
    }


    //重生
    public void Reborn()
    {
        m_rigidbody.velocity = Vector3.zero;
        int rebornPositionNum = Random.Range(0, MagnetLevelControl.Instance.RebornPosition.Count);
        transform.position = MagnetLevelControl.Instance.RebornPosition[rebornPositionNum].position;
        gameObject.SetActive(true);
        m_magneticField.MagneticOff();
    }


}