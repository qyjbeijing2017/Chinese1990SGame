using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DaemonTools;

public class Level1AI : MonoBehaviour
{
    public int ID;
    /// <summary>
    /// AI状态
    /// </summary>
    public enum AIState
    {
        Wait = 0,
        FindSeat = 1,
        CallWaiter = 2,
        WaitWaiter = 3,
        WaitFood = 4,
        End = 5
    }
    private AIState m_state = AIState.Wait;
    public AIState State
    {
        get
        {
            return m_state;
        }
    }

    private NavMeshAgent m_NMA;
    private int m_selectedSeat = 100;

    Temperament.TemperamentType m_temperamentType = Temperament.TemperamentType.Gentle;

    Level1DialogConfig dialongs;
    [SerializeField, Range(0, 1)] float m_replyTrue;

    /// <summary>
    /// 呼叫waiter
    /// </summary>
    /// <param name="ai"></param>
    /// <param name="callback"></param>
    public delegate void CallWaiterHandler(Level1AI ai, UnityAction callback);
    public event CallWaiterHandler CallWaiter;
    /// <summary>
    /// 开始入场
    /// </summary>
    public void StartIn(Temperament.TemperamentType temperamentType)
    {
        m_temperamentType = temperamentType;
        m_state = AIState.FindSeat;
        List<bool> seats = Level1Control.Instance.IsSeatEmpty;
        Random.seed = System.DateTime.Now.Second;
        int seatNum = Random.Range(0, seats.Count);
        while (!seats[seatNum]) { seatNum = Random.Range(0, seats.Count); }
        m_NMA.destination = Level1Control.Instance.Seats[seatNum].transform.position;
        seats[seatNum] = false;
        m_selectedSeat = seatNum;
        StartCoroutine("SitDown");
    }
    /// <summary>
    /// 等待人物坐下
    /// </summary>
    /// <returns></returns>
    IEnumerator SitDown()
    {
        yield return null;
        while (m_NMA.hasPath)
        {
            yield return null;
        }
        transform.forward = Level1Control.Instance.Seats[m_selectedSeat].transform.forward;
        m_state++;
        StartCoroutine("CallingWaiter");
    }
    /// <summary>
    /// 持续保持抢占呼叫
    /// </summary>
    /// <returns></returns>
    IEnumerator CallingWaiter()
    {
        while (m_state == AIState.CallWaiter)
        {
            Level1Control.Instance.OnCallWaiter(this, WaiterCallBack);
            yield return null;
        }
    }
    /// <summary>
    /// 当呼叫到waiter后绑定座位交互
    /// </summary>
    void WaiterCallBack()
    {
        if (m_state == AIState.CallWaiter)
        {
            m_state++;
            Level1Control.Instance.Seats[m_selectedSeat].OnEachOther += TalkWithWaiter;
        }
    }
    /// <summary>
    /// 当player交互后进行对话
    /// </summary>
    void TalkWithWaiter()
    {
        Debug.Log(ID + "Talk");
        Level1Control.Instance.Seats[m_selectedSeat].OnEachOther -= TalkWithWaiter;

        dialongs = Level1Control.Instance.ApplyDialog(m_temperamentType);
        for (int i = 0; i < dialongs.Dialog.Count; i++)
        {
            DialogueManager.Instance.AddDialogues(dialongs.Dialog[i]);
        }
        DialogueManager.Instance.DialogueEnd += TalkEnd;
    }
    /// <summary>
    /// 结束对话后释放对话系统、绑定交互系统
    /// </summary>
    void TalkEnd()
    {
        DialogueManager.Instance.DialogueEnd -= TalkEnd;
        Level1Control.Instance.Seats[m_selectedSeat].OnEachOther += WaitFood;
        m_state = AIState.WaitFood;
    }
    /// <summary>
    /// 等待食物结束释放交互
    /// </summary>
    void WaitFood()
    {
        Level1Control.Instance.Seats[m_selectedSeat].OnEachOther -= WaitFood;
        if (Level1Control.Instance.Food != Temperament.None)
        {
            if (Temperament.Like(Level1Control.Instance.Food, dialongs.TemperamentData) >= m_replyTrue)
            {
                DialogueManager.Instance.AddDialogues(Level1Control.Instance.ApplyReply(m_temperamentType, true));
            }
            else
            {
                DialogueManager.Instance.AddDialogues(Level1Control.Instance.ApplyReply(m_temperamentType, false));
            }
            Level1Control.Instance.Food = Temperament.None;
            Level1Control.Instance.OnEndCall();
            m_state = AIState.End;
        }
        else
        {
            for (int i = 0; i < dialongs.Dialog.Count; i++)
            {
                DialogueManager.Instance.AddDialogues(dialongs.Dialog[i]);
            }
            DialogueManager.Instance.DialogueEnd += TalkEnd;
        }
    }

    private void Awake()
    {
        m_NMA = GetComponent<NavMeshAgent>();
    }

}
