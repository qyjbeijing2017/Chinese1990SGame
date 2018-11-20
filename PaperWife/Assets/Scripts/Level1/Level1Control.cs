using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class Level1Control : MonoSingleton<Level1Control>
{

    [SerializeField,Tooltip("AI预制体")] Level1AI m_level1AI;
    [SerializeField,Tooltip("AI生成点")] Transform m_aiCreatPoint;
    /// <summary>
    /// AI列表
    /// </summary>
    public List<Level1AI> level1AIs;
    /// <summary>
    /// 座位列表
    /// </summary>
    [HideInInspector] public List<Seat> Seats;
    /// <summary>
    /// 座位占领列表
    /// </summary>
    [HideInInspector] public List<bool> IsSeatEmpty;

    [SerializeField,Tooltip("呼叫Waiter气泡预制体")] Buddle m_buddle;
    /// <summary>
    /// Waiter是否被占用
    /// </summary>
    [SerializeField] bool WaiterISHold = false;

    /// <summary>
    /// Waiter被呼叫次数
    /// </summary>
    int m_waiterCallNum = 0;
    [SerializeField,Tooltip("Waiter呼叫上限")] int m_CallMax = 5;

    enum Level1Type
    {
        WaitStart = 0,
        Ing = 1,
        End = 2,
    }
    Level1Type m_level1Type = Level1Type.WaitStart;

    // Use this for initialization
    void Start()
    {
        //tag查找当前场景中所有座位
        Seat[] seats = FindObjectsOfType<Seat>();
        Seats = new List<Seat>();
        for (int i = 0; i < seats.Length; i++)
        {
            Seats.Add(seats[i]);
        }
        for (int i = 0; i < Seats.Count; i++)
        {
            IsSeatEmpty.Add(true);
        }

        m_buddle = Instantiate(m_buddle);
        m_buddle.transform.SetParent(UIManager.Instance.Canvas.transform, false);
        m_buddle.transform.SetAsFirstSibling();
        m_buddle.gameObject.SetActive(false);


        CreatAI().StartIn();
        CreatAI().StartIn();
    }
    private void OnDestroy()
    {
    }

    IEnumerator Level1TechnologicalProcess()
    {
        yield return new WaitForSeconds(3);

    }

    /// <summary>
    /// 尝试呼叫Waiter
    /// </summary>
    public void OnCallWaiter(Level1AI ai, UnityAction callback)
    {
        if (!WaiterISHold)
        {
            WaiterISHold = true;
            m_waiterCallNum++;
            m_buddle.StartShow(ai);            
            callback();
        }
    }
    /// <summary>
    /// 生成AI
    /// </summary>
    Level1AI CreatAI()
    {
        Level1AI ai = Instantiate(m_level1AI, m_aiCreatPoint.position, m_aiCreatPoint.rotation);
        ai.ID = level1AIs.Count;
        level1AIs.Add(ai);
        ai.CallWaiter += OnCallWaiter;
        return ai;
    }

}
