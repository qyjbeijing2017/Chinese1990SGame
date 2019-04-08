using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;
public class LevelControl : MonoSingleton<LevelControl>
{
    public Dictionary<int, PlayerBase> Players = new Dictionary<int, PlayerBase>();
    public PlayerReborn playerReborn;
    public ScoreBoard LevelScoreBoard = new ScoreBoard();

    new void Awake()
    {
        base.Awake();
        PlayerBase[] players = FindObjectsOfType<PlayerBase>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].IsPlayer)
                Players.Add(players[i].ID, players[i]);
        }

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
}
