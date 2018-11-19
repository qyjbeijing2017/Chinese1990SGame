using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level1AI : MonoBehaviour
{
    public int ID;

    public enum AIState
    {
        Wait = 0,
        FindSeat = 1,
        WaitWaiter = 2,
        WaitFood = 3,
        End = 4
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

    public void StartIn()
    {
        m_state = AIState.FindSeat;
        List<bool> seats = Level1Control.Instance.IsSeatEmpty;
        int seatNum = Random.Range(0, seats.Count);
        while (!seats[seatNum]) { seatNum = Random.Range(0, seats.Count); }
        m_NMA.destination = Level1Control.Instance.Seats[seatNum].position;
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
        transform.forward = Level1Control.Instance.Seats[m_selectedSeat].forward;

        m_state = AIState.WaitWaiter;
    }

    private void Awake() 
    {
        m_NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        //释放AIList
        Level1Control.Instance.level1AIs.RemoveAt(ID);
    }


}
