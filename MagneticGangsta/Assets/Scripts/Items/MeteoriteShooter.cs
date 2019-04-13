using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteShooter : MonoBehaviour
{
    [Range(-20f, 0f), SerializeField] float m_shootLeft = -13.0f;
    [Range(0f, 20f), SerializeField] float m_shootRight = 13.0f;
    [Range(0, 20f), SerializeField] float m_shootHeight = 10.0f;

    [SerializeField] CDBase m_shootCD = new CDBase(3.0f);
    [SerializeField] MeteoriteFall m_meteorite;
    [SerializeField] LayerMask m_rayMask = 1 << 10;

    public float ShootLeft
    {
        get { return m_shootLeft; }
    }
    public float ShootRight
    {
        get { return m_shootRight; }
    }
    public float ShootHeight
    {
        get { return m_shootHeight; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_shootCD.OnTimeOut += OnTimeOut;
        m_shootCD.Start();
    }

    void OnTimeOut()
    {
        Vector2 creatPos = new Vector2(Random.Range(m_shootLeft, m_shootRight), m_shootHeight);
        Vector2 targetOrigin = new Vector2(Random.Range(m_shootLeft, m_shootRight), m_shootHeight);

        RaycastHit2D hit = Physics2D.Raycast(targetOrigin, Vector2.down, 100.0f, m_rayMask);
        if (hit.collider != null)
        {
            MeteoriteFall meteoriteFall = Instantiate(m_meteorite,creatPos,Quaternion.Euler(0,0,0));
            meteoriteFall.TargetPoint = hit.point;
        }
        m_shootCD.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
