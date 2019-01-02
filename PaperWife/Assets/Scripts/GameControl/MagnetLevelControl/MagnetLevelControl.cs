using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class MagnetLevelControl : MonoSingleton<MagnetLevelControl>
{

    public List<MPlayerController> Players;
    public List<Transform> RebornPosition;

    // Use this for initialization
    void Start()
    {
        //初始化玩家
        MPlayerController[] players = FindObjectsOfType<MPlayerController>();
        Players = new List<MPlayerController>();
        for (int i = 0; i < players.Length; i++)
        {
            Players.Add(players[i]);

        }

        //初始化重生点
        GameObject[] reborn = GameObject.FindGameObjectsWithTag("RebornPosition");
        RebornPosition = new List<Transform>();

        for (int i = 0; i < reborn.Length; i++)
        {
            RebornPosition.Add(reborn[i].transform);
        }

    }

    /// <summary>
    /// 玩家死亡事件，因为玩家被active= false 所以在这里处理。
    /// </summary>
    /// <param name="player"></param>
    public void OnReborn(MPlayerController player)
    {
        StartCoroutine(RebornTimer(player));
    }
    IEnumerator RebornTimer(MPlayerController player)
    {
        yield return new WaitForSeconds(player.m_rebornTime);
        player.Reborn();

    }

}
