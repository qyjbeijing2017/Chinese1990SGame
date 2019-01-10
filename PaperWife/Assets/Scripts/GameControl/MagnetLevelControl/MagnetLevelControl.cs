using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class MagnetLevelControl : MonoSingleton<MagnetLevelControl>
{

    public List<MPlayerController> Players = new List<MPlayerController>();
    public List<Transform> RebornPosition = new List<Transform>();
    public PhysicsMaterial2D pm;
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

        Daemon.Instance.InitConsoleObjs();
        ReadConfig();
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



    public void ReadConfig()
    {
        PlayerConfig playerConfig = ConfigManager.Instance.PlayerConfigData["Value"];
        for (int i = 0; i < Players.Count; i++)
        {
            MPlayerController player                         = Players[i];
            player.m_playerForce                             = playerConfig.PlayerForce;
            player.m_maxMoveSpeed                            = playerConfig.MaxMoveSpeed;
            player.m_JumpsVelocity                           = playerConfig.JumpsVelocity;
            player.m_polarity                                = playerConfig.Polarity;
            player.m_playerHPMax                             = playerConfig.PlayerHPMax;
            player.m_rebornTime                              = playerConfig.RebornTime;
            player.MagneticChangeTime                        = playerConfig.MagneticChangeTime;
            player.MagneticChangeCD                          = playerConfig.MagneticChangeCD;
            player.m_magneticField.MagneticTime              = playerConfig.MagneticTime;
            player.m_magneticField.MagneticForce             = playerConfig.MagneticForce;
            player.m_magneticField.MagneticCoefficient       = playerConfig.MagneticCoefficient;
            player.m_magneticField.MagnetDecrease            = playerConfig.MagnetDecrease;
            player.m_magneticField.MagneticCDTime            = playerConfig.MagneticCDTime;
            player.m_magneticField.ReatctionForceCoefficient = playerConfig.ReatctionForceCoefficient;
            player.GetComponent<Rigidbody2D>().gravityScale  = playerConfig.GScale;
            pm.friction                                      = playerConfig.DragGround;
            player.GetComponent<Rigidbody2D>().drag          = playerConfig.DragRB;
        }
    }



}
