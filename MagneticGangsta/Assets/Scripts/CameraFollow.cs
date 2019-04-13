using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;

public class CameraFollow : MonoSingleton<CameraFollow>
{

    [Header("上边缘"), Range(0, 100)] public float TopEdge;
    [Header("下边缘"), Range(0, -100)] public float ButtonEdge;
    [Header("左边缘"), Range(0, -100)] public float LeftEdge;
    [Header("右边缘"), Range(0, 100)] public float RightEdge;

    [Space(20), Header("玩家最大距离屏占比"), Range(0, 1)] public float Dis2Size;
    [Header("摄像机最小尺寸"), Range(0, 10)] public float SizeMin;
    [Header("摄像机最大尺寸"), Range(3, 100)] public float SizeMax;
    [Header("摄像机缩放速度"), Range(0, 10)] public float ScaleLowSpeed;
    [Header("摄像机拉伸速度"), Range(0, 100)] public float ScaleUpSpeed;

    [Space(20), Header("摄像机跟随速度"), Range(0, 10)] public float FollowSpeed;
    [Header("开始跟随中心点最大差值"), Range(0, 10)] public float StartFollowDis;

    const int m_maxFollowItem = 1200;

    const float wh = 16.0f / 9.0f;

    public float CameraLeftEdge
    {
        get
        {
            float a = Camera.main.transform.position.x - Camera.main.orthographicSize * wh;
            return a;
        }
    }
    public float CameraRightEdge
    {
        get
        {
            float a = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
            return a;
        }
    }
    public float CameraTopEdge
    {
        get
        {
            float a = Camera.main.transform.position.y + Camera.main.orthographicSize;
            return a;
        }
    }
    public float CameraButtonEdge
    {
        get
        {
            float a = Camera.main.transform.position.y - Camera.main.orthographicSize;
            return a;
        }
    }

    public float PlayerLeft
    {
        get
        {
            if (FallowItem == null)
            {
                return 0.0f;
            }
            float a = FallowItem[0].transform.position.x;
            for (int i = 0; i < FallowItem.Count; i++)
            {
                if (FallowItem[i].transform.position.x <= a)
                {
                    a = FallowItem[i].transform.position.x;
                }
            }
            return a;
        }
    }
    public float PlayerRight
    {
        get
        {
            if (FallowItem == null)
            {
                return 0.0f;
            }
            float a = FallowItem[0].transform.position.x;
            for (int i = 0; i < FallowItem.Count; i++)
            {
                if (FallowItem[i].transform.position.x >= a)
                {
                    a = FallowItem[i].transform.position.x;
                }
            }
            return a;
        }
    }
    public float PlayerTop
    {
        get
        {
            if (FallowItem == null)
            {
                return 0.0f;
            }
            float a = FallowItem[0].transform.position.y;
            for (int i = 0; i < FallowItem.Count; i++)
            {
                if (FallowItem[i].transform.position.y >= a)
                {
                    a = FallowItem[i].transform.position.y;
                }
            }
            return a;
        }
    }
    public float PlayerButton
    {

        get
        {
            if (FallowItem == null)
            {
                return 0.0f;
            }
            float a = FallowItem[0].transform.position.y;
            for (int i = 0; i < FallowItem.Count; i++)
            {
                if (FallowItem[i].transform.position.y <= a)
                {
                    a = FallowItem[i].transform.position.y;
                }
            }
            return a;
        }
    }

    private Dictionary<int, GameObject> m_followItem = new Dictionary<int, GameObject>();
    public List<GameObject> FallowItem
    {
        get
        {
            List<GameObject> followItem = new List<GameObject>();
            var enumerator = m_followItem.GetEnumerator();
            while (enumerator.MoveNext())
            {
                followItem.Add(enumerator.Current.Value);                
            }
            return followItem;
        }
    }


    public Vector2 PlayerCenter
    {
        get
        {
            if (FallowItem == null || FallowItem .Count == 0)
            {
                return Vector2.zero;
            }
            List<GameObject> players = FallowItem;
            float top = FallowItem[0].transform.position.y;
            float button = FallowItem[0].transform.position.y;
            float left = FallowItem[0].transform.position.x;
            float right = FallowItem[0].transform.position.x;
            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].transform.position.y >= top)
                {
                    top = players[i].transform.position.y;
                }
                if (players[i].transform.position.y <= button)
                {
                    button = players[i].transform.position.y;
                }
                if (players[i].transform.position.x <= left)
                {
                    left = players[i].transform.position.x;
                }
                if (players[i].transform.position.x >= right)
                {
                    right = players[i].transform.position.x;
                }
            }

            Vector2 center = Vector2.zero;
            center.x = (right - left) / 2 + left;
            center.y = (top - button) / 2 + button;
            return center;
        }
    }

    public void CameraPositionLimit()
    {
        if (CameraRightEdge > RightEdge)
        {
            Camera.main.transform.position -= new Vector3(CameraRightEdge - RightEdge, 0, 0);
        }
        if (CameraLeftEdge < LeftEdge)
        {
            Camera.main.transform.position -= new Vector3(CameraLeftEdge - LeftEdge, 0, 0);
        }
        if (CameraButtonEdge < ButtonEdge)
        {
            Camera.main.transform.position -= new Vector3(0, CameraButtonEdge - ButtonEdge, 0);
        }
        if (CameraTopEdge > TopEdge)
        {
            Camera.main.transform.position -= new Vector3(0, CameraTopEdge - TopEdge, 0);
        }
    }
    public void CameraPositionFollow()
    {
        Vector2 cameraPosition = Camera.main.transform.position;
        if ((PlayerCenter - cameraPosition).magnitude > StartFollowDis)
        {
            Camera.main.transform.position += (Vector3)(PlayerCenter - cameraPosition).normalized * FollowSpeed * Time.deltaTime;
        }
    }

    public void CameraScaleLimit()
    {
        if (Camera.main.orthographicSize < SizeMin)
        {
            Camera.main.orthographicSize = SizeMin;
        }
        if (Camera.main.orthographicSize > SizeMax)
        {
            Camera.main.orthographicSize = SizeMax;
        }
    }
    public float TargetSize
    {
        get
        {
            if (FallowItem == null || FallowItem.Count == 0)
            {
                return 0.0f;
            }
            List<GameObject> players = FallowItem;
            float top = FallowItem[0].transform.position.y;
            float button = FallowItem[0].transform.position.y;
            float left = FallowItem[0].transform.position.x;
            float right = FallowItem[0].transform.position.x;
            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].transform.position.y >= top)
                {
                    top = players[i].transform.position.y;
                }
                if (players[i].transform.position.y <= button)
                {
                    button = players[i].transform.position.y;
                }
                if (players[i].transform.position.x <= left)
                {
                    left = players[i].transform.position.x;
                }
                if (players[i].transform.position.x >= right)
                {
                    right = players[i].transform.position.x;
                }
            }

            float targetX = (right - left) / 2;
            float targetY = (top - button) / 2;
            float targetSize = Mathf.Max(targetX / wh, targetY) / Dis2Size;
            return targetSize;

        }
    }
    public void CameraScaleFollow()
    {
        if (Camera.main.orthographicSize > TargetSize)
        {
            Camera.main.orthographicSize -= ScaleLowSpeed * Time.deltaTime;
            if (Camera.main.orthographicSize < TargetSize)
            {
                Camera.main.orthographicSize = TargetSize;
            }

            return;
        }
        if (Camera.main.orthographicSize < TargetSize)
        {
            Camera.main.orthographicSize += ScaleUpSpeed * Time.deltaTime;
            if (Camera.main.orthographicSize > TargetSize)
            {
                Camera.main.orthographicSize = TargetSize;
            }
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CameraScaleFollow();
        CameraScaleLimit();
        CameraPositionFollow();
        CameraPositionLimit();
    }



    public int AddFollowItem(GameObject obj)
    {
        int index = Random.Range(0, m_maxFollowItem);
        while (m_followItem.ContainsKey(index))
        {
            index = Random.Range(0, m_maxFollowItem);
        }
        m_followItem.Add(index, obj);
        return index;
    }

    public bool RemoveFollowItem(int index)
    {
        if (m_followItem.ContainsKey(index))
        {
            m_followItem.Remove(index);
            return true;
        }

        return false;
    }

}
