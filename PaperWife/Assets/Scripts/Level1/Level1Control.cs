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
    [HideInInspector] public List<Transform> Seats;
    /// <summary>
    /// 座位占领列表
    /// </summary>
    [HideInInspector] public List<bool> IsSeatEmpty;

    [SerializeField,Tooltip("呼叫Waiter气泡预制体")] GameObject Buddle;
    /// <summary>
    /// Waiter是否被占用
    /// </summary>
    [SerializeField] bool WaiterISHold = false;
    /// <summary>
    /// Waiter被呼叫次数
    /// </summary>
    int m_waiterCallNum = 0;
    [SerializeField,Tooltip("Waiter呼叫上限")] int m_CallMax = 5;


    // Use this for initialization
    void Start()
    {
        //tag查找当前场景中所有座位
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Seat");
        Seats = new List<Transform>();
        for (int i = 0; i < gos.Length; i++)
        {
            Seats.Add(gos[i].transform);
        }
        for (int i = 0; i < Seats.Count; i++)
        {
            IsSeatEmpty.Add(true);
        }

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
    void OnCallWaiter()
    {
        if (!WaiterISHold)
        {
            WaiterISHold = true;
        }
    }
    /// <summary>
    /// 生成AI
    /// </summary>
    Level1AI CreatAI()
    {
        Level1AI ai = Instantiate(m_level1AI, m_aiCreatPoint);
        ai.ID = level1AIs.Count;
        level1AIs.Add(ai);
        return ai;
    }

}
