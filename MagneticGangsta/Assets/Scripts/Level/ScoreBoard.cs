using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;
/// <summary>
/// 用于计分，该脚本应在LevelControl后执行
/// </summary>
public class ScoreBoard : MonoSingleton<ScoreBoard>
{
    Dictionary<int, int> Scores = new Dictionary<int, int>();

    public int KillPoint = 30;

    public event UnityAction<PlayerBase> GetScores;

    private void Awake()
    {
        if (LevelControl.Instance.playerReborn)
        {
            LevelControl.Instance.playerReborn.OnDie += OnPlayerDie;
        }
    }

    void OnPlayerDie(PlayerBase player)
    {

        if (player.FunctionBases.ContainsKey("HitAttacker"))
        {
            HitAttacker hitAttacker = player.FunctionBases["HitAttacker"] as HitAttacker;
            if (hitAttacker.Attacker)
            {
                PlayerBase attacker = hitAttacker.Attacker;
                if (Scores.ContainsKey(attacker.ID))
                {
                    Scores[attacker.ID] += KillPoint;
                }
                else
                {
                    Scores.Add(attacker.ID, KillPoint);
                }
                GetScores?.Invoke(attacker);


                var enumerator = Scores.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    print(enumerator.Current.Key + ":" + enumerator.Current.Value);
                }
            }
        }
    }
}
