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
    [Header("摄像机最小尺寸"), Range(0, 8)] public float SizeMin;
    [Header("摄像机最大尺寸"), Range(8, 15)] public float SizeMax;
    [Header("摄像机缩放速度"), Range(0, 5)] public float ScaleLowSpeed;
    [Header("摄像机拉伸速度"), Range(0, 100)] public float ScaleUpSpeed;

    [Space(20), Header("摄像机跟随速度"), Range(0, 10)] public float FollowSpeed;
    [Header("开始跟随中心点最大差值"), Range(0, 10)] public float StartFollowDis;

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
            if (Players == null)
            {
                return 0.0f;
            }
            float a = Players[0].transform.position.x;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].transform.position.x <= a)
                {
                    a = Players[i].transform.position.x;
                }
            }
            return a;
        }
    }
    public float PlayerRight
    {
        get
        {
            if (Players == null)
            {
                return 0.0f;
            }
            float a = Players[0].transform.position.x;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].transform.position.x >= a)
                {
                    a = Players[i].transform.position.x;
                }
            }
            return a;
        }
    }
    public float PlayerTop
    {
        get
        {
            if (Players == null)
            {
                return 0.0f;
            }
            float a = Players[0].transform.position.y;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].transform.position.y >= a)
                {
                    a = Players[i].transform.position.y;
                }
            }
            return a;
        }
    }
    public float PlayerButton
    {

        get
        {
            if (Players == null)
            {
                return 0.0f;
            }
            float a = Players[0].transform.position.y;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].transform.position.y <= a)
                {
                    a = Players[i].transform.position.y;
                }
            }
            return a;
        }
    }

    private List<MPlayerController> m_players;
    public List<MPlayerController> Players
    {
        get
        {
            if (m_players != null)
            {
                for (int i = 0; i < m_players.Count; i++)
                {
                    if (null == m_players[i])
                    {
                        m_players.RemoveAt(i);
                    }
                }
                return m_players;
            }

            MPlayerController[] players = FindObjectsOfType<MPlayerController>();
            if (players == null)
            {
                return null;
            }
            if (players.Length == 0)
            {
                return null;
            }

            m_players = new List<MPlayerController>();
            for (int i = 0; i < players.Length; i++)
            {
                m_players.Add(players[i]);
            }
            return m_players;

        }
    }


    public Vector2 PlayerCenter
    {
        get
        {
            if (Players == null)
            {
                return Vector2.zero;
            }
            List<MPlayerController> players = Players;
            float top = Players[0].transform.position.y;
            float button = Players[0].transform.position.y;
            float left = Players[0].transform.position.x;
            float right = Players[0].transform.position.x;
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
            if (Players == null)
            {
                return 0.0f;
            }
            List<MPlayerController> players = Players;
            float top = Players[0].transform.position.y;
            float button = Players[0].transform.position.y;
            float left = Players[0].transform.position.x;
            float right = Players[0].transform.position.x;
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
}
