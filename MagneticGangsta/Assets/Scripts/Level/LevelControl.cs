using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;
public class LevelControl : MonoSingleton<LevelControl>
{
    private Dictionary<int, PlayerBase> m_players = null;

    public Dictionary<int, PlayerBase> Players
    {
        get
        {
            if (m_players != null)
            {
                return m_players;
            }
            else
            {
                return FindPlayers();
            }
        }
    }
    public PlayerReborn playerReborn;
    public ScoreBoard LevelScoreBoard = new ScoreBoard();

    new void Awake()
    {
        base.Awake();


        if (!playerReborn)
        {
            playerReborn = FindObjectOfType<PlayerReborn>();
        }

        LevelScoreBoard.Init();
        Daemon.Instance.Init();
        UIManager.Instance.Open("GUIPanel");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public Dictionary<int, PlayerBase> FindPlayers()
    {

        m_players = new Dictionary<int, PlayerBase>();
        PlayerBase[] players = FindObjectsOfType<PlayerBase>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].IsPlayer)
                m_players.Add(players[i].ID, players[i]);
        }
        return m_players;
    }
}
