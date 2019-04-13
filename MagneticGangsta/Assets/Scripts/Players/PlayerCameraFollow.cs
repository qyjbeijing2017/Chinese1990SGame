using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : PlayerFunctionBase
{
    public override string Name { get { return "PlayerCameraFollow"; } }

    int m_cameraFollowIndex = 0;

    public override void PlayerInit()
    {
        m_cameraFollowIndex = CameraFollow.Instance.AddFollowItem(gameObject);
        Player.OnDie += OnPlayerDie;
        Player.OnReborn += OnPlayerReborn;

    }


    public override void OnPlayerDie()
    {
        CameraFollow.Instance.RemoveFollowItem(m_cameraFollowIndex);
    }

    void OnPlayerReborn()
    {
        m_cameraFollowIndex = CameraFollow.Instance.AddFollowItem(gameObject);
    }
}
