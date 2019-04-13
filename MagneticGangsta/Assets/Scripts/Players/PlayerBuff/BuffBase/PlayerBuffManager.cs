using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBuffManager : MonoBehaviour
{
    PlayerBase m_player;
    public PlayerBase Player { get { return m_player; } }

    private Dictionary<string, PlayerBuffBase> m_buffes = new Dictionary<string, PlayerBuffBase>();
    Dictionary<string, PlayerBuffBase> Buffes { get { return m_buffes; } }

    private BuffAttributesData m_arrtibutes;
    public BuffAttributesData Attributes { get { return m_arrtibutes; } }

    public void Init(PlayerBase player)
    {
        m_player = player;
        m_arrtibutes = new BuffAttributesData(Player);
    }

    public void AddBuff(PlayerBuffBase buff)
    {
        buff.BuffManager = this;

        if (m_buffes.ContainsKey(buff.Name))
        {
            RemoveBuff(buff.Name);
        }
        buff.StartBefore(m_arrtibutes);
        m_buffes.Add(buff.Name, buff);
        m_arrtibutes.OutData();
    }

    public void RemoveBuff(string buffname)
    {
        if (m_buffes.ContainsKey(buffname))
        {
            m_buffes[buffname].BuffEnd(m_arrtibutes);
            m_buffes.Remove(buffname);
            m_arrtibutes.OutData();
        }
    }


    #region Mono功能

    private void Update()
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.Update();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnTriggerEnter2D(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnTriggerExit2D(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnTriggerStay2D(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnCollisionEnter2D(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnCollisionExit2D(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var enumerator = m_buffes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.OnCollisionStay2D(collision);
        }
    }

    #endregion
}

