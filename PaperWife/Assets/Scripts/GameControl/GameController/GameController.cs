using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class GameEventsController : Singleton<GameEventsController>
{

    public enum State
    {
        Start = 0,
    }

    public event UnityAction OnStart;
    public event UnityAction BookIn;


    public void StartTrigger()
    {

        OnStart.Invoke();
    }

    public void BookInTrigger()
    {
        
        BookIn.Invoke();
    }


}
