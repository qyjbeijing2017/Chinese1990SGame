using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate T BuffAttributeAction<T>(T attribute,T attributebase) where T : struct;
public class BuffAttributeBase<T> where T : struct
{
    private T m_attributeBase;
    public T AttributeBase { get { return m_attributeBase; } }

    protected Dictionary<string, BuffAttributeAction<T>> m_atributeChanges =  new Dictionary<string, BuffAttributeAction<T>>();

    public T Attribute
    {
        get
        {
            T a = m_attributeBase;
            var enumerator = m_atributeChanges.GetEnumerator();
            while (enumerator.MoveNext())
            {
                a = enumerator.Current .Value.Invoke(a, m_attributeBase);
            }
            return a;
        }
    }

    public BuffAttributeBase(T attributeBase)
    {
        m_attributeBase = attributeBase;
    }


    public void AddBuffEffect(string buffname,BuffAttributeAction<T> action)
    {
        if (m_atributeChanges.ContainsKey(buffname))
        {
            m_atributeChanges[buffname] = action;
        }
        else
        {
            m_atributeChanges.Add(buffname, action);
        }
    }

    public bool RemoveBuffEffect(string buffname)
    {
        if (m_atributeChanges.ContainsKey(buffname))
        {
            m_atributeChanges.Remove(buffname);
            return true;
        }
        return false;
    }
}
