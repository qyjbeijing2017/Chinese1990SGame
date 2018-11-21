using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

    public delegate void CallWaiterHandler(Level1AI ai, UnityAction callback);

    public event CallWaiterHandler CallWaiter;

    public void StartIn()
    {
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

    IEnumerator CallingWaiter()
    {
        while(m_state == AIState.CallWaiter)
        {
            Level1Control.Instance.OnCallWaiter(this, WaiterCallBack);
            yield return null;
        }
    }

    void WaiterCallBack()
    {
        if (m_state == AIState.CallWaiter)
        {
            m_state++;
            Level1Control.Instance.Seats[m_selectedSeat].OnEachOther += TalkWithWaiter;
        }
    }

    void TalkWithWaiter()
    {
        Debug.Log(ID+"Talk");
        Level1Control.Instance.Seats[m_selectedSeat].OnEachOther -= TalkWithWaiter;


    }

    private void Awake() 
    {
        m_NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
