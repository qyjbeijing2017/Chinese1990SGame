using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowItem : PlayerFunctionBase
{
    public override string Name { get { return "CameraFollowItem"; } }


    int m_followItemIndex = 0;

    private void Start()
    {
        m_followItemIndex = CameraFollow.Instance.AddFollowItem(gameObject);
    }

    private void OnDestroy()
    {

        CameraFollow.Instance.RemoveFollowItem(m_followItemIndex);
    }
}
