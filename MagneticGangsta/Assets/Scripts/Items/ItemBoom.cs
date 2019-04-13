using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemBoom : PlayerFunctionBase
{
    public override string Name { get { return "ItemBoom"; } }
    public event UnityAction OnCreatItem;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Collider2D m_collider;
    private SpriteRenderer m_spriteRenderer;
    private TrailRenderer m_trailRenderer;

    // Start is called before the first frame update
    public override void PlayerInit()
    {
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }
        if (m_collider == null)
        {
            m_collider = GetComponent<Collider2D>();
        }
        m_collider.isTrigger = true;
        m_collider.enabled = false;
        GetComponent<MeteoriteFall>().OnBoom += OnBoom;
        m_animator.speed = 0;

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnBoom()
    {
        m_animator.speed = 1;
        m_spriteRenderer.enabled = false;
    }

    public void BoomStart()
    {
        m_collider.enabled = true;
    }

    public void CreatItem()
    {
        OnCreatItem?.Invoke();
        print("creatItem");
    }

    public void BoomEnd()
    {
        m_collider.enabled = false;
    }

    public void BoomDestory()
    {
        Destroy(gameObject);
    }

}
