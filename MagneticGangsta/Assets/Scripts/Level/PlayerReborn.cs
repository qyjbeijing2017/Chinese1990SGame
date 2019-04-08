﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReborn : MonoBehaviour
{
    public event UnityAction<PlayerBase> OnDie;

    public float RebornWaitTime = 3.0f;

    private List<Transform> m_rebornPoints = new List<Transform>();

    public Collider2D Area;

    private void Awake()
    {
        GameObject[] rebornpoints = GameObject.FindGameObjectsWithTag("RebornPoint");
        for (int i = 0; i < rebornpoints.Length; i++)
        {
            m_rebornPoints.Add(rebornpoints[i].transform);
        }
        OnDie += OnPlayerDie;
        Area = !Area ? GetComponent<Collider2D>() : Area;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBase player = collision.GetComponent<PlayerBase>();
        if (player != null && player.IsPlayer)
        {
            if (!player.MainCollider.IsTouching(Area)) return;
            player.OnDie?.Invoke();
            OnDie?.Invoke(player);
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
        }
    }
}
