using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class Level1Control : MonoSingleton<Level1Control>
{

    [SerializeField, Tooltip("AI预制体")] Level1AI m_level1AI;
    [SerializeField, Tooltip("AI生成点")] Transform m_aiCreatPoint;
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

    [SerializeField, Tooltip("呼叫Waiter气泡预制体")] Buddle m_buddle;
    /// <summary>
    /// Waiter是否被占用
    /// </summary>
    [SerializeField] bool WaiterISHold = false;

    /// <summary>
    /// Waiter被呼叫次数
    /// </summary>
    int m_waiterCallNum = 0;
    [SerializeField, Tooltip("Waiter呼叫上限")] int m_CallMax = 5;

    [SerializeField, Tooltip("结束呼叫后等待时间")] float m_endCallWaitTime = 0.0f;

    enum Level1Type
    {
        WaitStart = 0,
        Ing = 1,
        End = 2,
    }
    Level1Type m_level1Type = Level1Type.WaitStart;

    //初始对话
    List<Level1DialogConfig> m_madDialogs = new List<Level1DialogConfig>();
    List<Level1DialogConfig> m_sadDialogs = new List<Level1DialogConfig>();
    List<Level1DialogConfig> m_complacentDialogs = new List<Level1DialogConfig>();
    List<BettingData> m_madDialogsBetting = new List<BettingData>();
    List<BettingData> m_sadDialogsBetting = new List<BettingData>();
    List<BettingData> m_complacentDialogsBetting = new List<BettingData>();

    //结束对话
    List<Level1ReplyConfig> m_madReply = new List<Level1ReplyConfig>();
    List<Level1ReplyConfig> m_sadReply = new List<Level1ReplyConfig>();
    List<Level1ReplyConfig> m_complacentReply = new List<Level1ReplyConfig>();

    /// <summary>
    /// 当前食物记录
    /// </summary>
    public Temperament Food = Temperament.None;

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

        //气泡初始化
        m_buddle = Instantiate(m_buddle);
        m_buddle.transform.SetParent(UIManager.Instance.Canvas.transform, false);
        m_buddle.transform.SetAsFirstSibling();
        m_buddle.gameObject.SetActive(false);

        //拆分对话
        Dictionary<int, Level1DialogConfig> dialogData = ConfigManager.Instance.Level1DialogConfigData;
        Dictionary<int, Level1ReplyConfig> replyData = ConfigManager.Instance.Level1ReplyConfigData;

        var dialogEnumerator = dialogData.GetEnumerator();
        var replyEnumerator = replyData.GetEnumerator();
        while (dialogEnumerator.MoveNext())
        {
            switch (dialogEnumerator.Current.Value.TemperamentData.MainType)
            {
                case Temperament.TemperamentType.Mad:
                    m_madDialogs.Add(dialogEnumerator.Current.Value);
                    BettingData bettingData = new BettingData((m_madDialogs.Count-1).ToString(),1);
                    m_madDialogsBetting.Add(bettingData);
                    break;
                case Temperament.TemperamentType.Sad:
                    m_sadDialogs.Add(dialogEnumerator.Current.Value);
                    BettingData bettingData1 = new BettingData((m_sadDialogs.Count - 1).ToString(), 1);
                    m_sadDialogsBetting.Add(bettingData1);
                    break;
                case Temperament.TemperamentType.Complacent:
                    m_complacentDialogs.Add(dialogEnumerator.Current.Value);
                    BettingData bettingData2 = new BettingData((m_complacentDialogs.Count - 1).ToString(), 1);
                    m_complacentDialogsBetting.Add(bettingData2);
                    break;
                default:
                    break;
            }
        }
        while (replyEnumerator.MoveNext())
        {
            switch (replyEnumerator.Current.Value.MainType)
            {
                case Temperament.TemperamentType.Mad:
                    m_madReply.Add(replyEnumerator.Current.Value);
                    break;
                case Temperament.TemperamentType.Sad:
                    m_sadReply.Add(replyEnumerator.Current.Value);
                    break;
                case Temperament.TemperamentType.Complacent:
                    m_complacentReply.Add(replyEnumerator.Current.Value);
                    break;
                default:
                    break;
            }
        }
        

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
    /// 结束呼叫Waiter
    /// </summary>
    public void OnEndCall()
    {
        StartCoroutine("EndCall");
    }
    IEnumerator EndCall()
    {
        yield return new WaitForSeconds(m_endCallWaitTime);
        WaiterISHold = false;
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

    public Level1DialogConfig ApplyDialog(Temperament.TemperamentType temperamentType)
    {
        List < Level1DialogConfig > dialogs = new List< Level1DialogConfig > ();
        int a = 0;
        switch (temperamentType)
        {
            case Temperament.TemperamentType.Mad:                
                int.TryParse(BettingData.BettingWheel(m_madDialogsBetting),out a);
                m_madDialogsBetting[a] = new BettingData(m_madDialogsBetting[a].Name, m_madDialogsBetting[a].Weight / 2);
                return m_madDialogs[a];
            case Temperament.TemperamentType.Sad:
                int.TryParse(BettingData.BettingWheel(m_sadDialogsBetting), out a);
                return m_sadDialogs[a];
            case Temperament.TemperamentType.Complacent:
                int.TryParse(BettingData.BettingWheel(m_complacentDialogsBetting), out a);
                return m_complacentDialogs[a];
            default:
                Debug.Log("没有这个语句");
                return null;
        }
    }

    public string ApplyReply(Temperament.TemperamentType temperamentType,bool isNear)
    {
        switch (temperamentType)
        {
            case Temperament.TemperamentType.Mad:
                UnityEngine.Random.seed = System.DateTime.Now.Second;
                if (isNear)
                    return m_madReply[Random.Range(0, m_madReply.Count)].DialogTrue;
                else
                    return m_madReply[Random.Range(0, m_madReply.Count)].DialogFalse;
            case Temperament.TemperamentType.Sad:
                UnityEngine.Random.seed = System.DateTime.Now.Second;
                if (isNear)
                    return m_sadReply[Random.Range(0, m_sadReply.Count)].DialogTrue;
                else
                    return m_sadReply[Random.Range(0, m_sadReply.Count)].DialogFalse;
            case Temperament.TemperamentType.Complacent:
                UnityEngine.Random.seed = System.DateTime.Now.Second;
                if (isNear)
                    return m_complacentReply[Random.Range(0, m_complacentReply.Count)].DialogTrue;
                else
                    return m_complacentReply[Random.Range(0, m_complacentReply.Count)].DialogFalse;
            default:
                return string.Empty;
        }
    }
}
