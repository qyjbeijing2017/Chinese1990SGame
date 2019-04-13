using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReborn : MonoBehaviour
{

    public Dictionary<int, PlayerBase> Players = null;


    public event UnityAction<PlayerBase> OnDie;

    public float RebornWaitTime = 3.0f;

    private List<Transform> m_rebornPoints = new List<Transform>();


    [Range(-150,0), SerializeField] float m_leftEdge = -20.0f;
    [Range(0, 150), SerializeField] float m_rightEdge = 20.0f;
    [Range(0, 150), SerializeField] float m_topEdge = 20.0f;
    [Range(-150, 0), SerializeField] float m_buttonEdge = -20.0f;

    public float LeftEdge { get { return m_leftEdge; } }
    public float RightEdge { get { return m_rightEdge; } }
    public float TopEdge { get { return m_topEdge; } }
    public float ButtonEdge { get { return m_buttonEdge; } }

    private void Awake()
    {
        GameObject[] rebornpoints = GameObject.FindGameObjectsWithTag("RebornPoint");
        for (int i = 0; i < rebornpoints.Length; i++)
        {
            m_rebornPoints.Add(rebornpoints[i].transform);
        }
        OnDie += OnPlayerDie;
        Players = LevelControl.Instance.Players;

    }



    private void Update()
    {
        var enumerator = Players.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value.IsDead)
            {
                continue;
            }
            Vector2 pos = enumerator.Current.Value.transform.position;
            if(pos.x<m_leftEdge || pos.x>m_rightEdge || pos.y>m_topEdge || pos.y < m_buttonEdge)
            {
                OnDie?.Invoke(enumerator.Current.Value);
                enumerator.Current.Value.OnDie?.Invoke();
            }

        }
    }


    private void OnPlayerDie(PlayerBase player)
    {
        StartCoroutine("Reborn", player);

    }

    IEnumerator Reborn(PlayerBase player)
    {
        if (RebornWaitTime > 0)
        {

            yield return new WaitForSeconds(RebornWaitTime);

            player.ReBorn(m_rebornPoints[Random.Range(0, 4)].position);
            player.OnReborn?.Invoke();
        }
    }



}
