using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor
{
    public CameraFollow m_cameraFollow;
    private void OnEnable()
    {
        if (m_cameraFollow == null)
        {
            m_cameraFollow = (CameraFollow)target;
        }

    }



    void DrawEdge()
    {
        Vector3[] lines = { new Vector3(m_cameraFollow.LeftEdge, m_cameraFollow.TopEdge, 0),
                            new Vector3(m_cameraFollow.RightEdge, m_cameraFollow.TopEdge, 0),
                            new Vector3(m_cameraFollow.RightEdge, m_cameraFollow.TopEdge, 0),
                            new Vector3(m_cameraFollow.RightEdge, m_cameraFollow.ButtonEdge, 0),
                            new Vector3(m_cameraFollow.RightEdge, m_cameraFollow.ButtonEdge, 0),
                            new Vector3(m_cameraFollow.LeftEdge, m_cameraFollow.ButtonEdge, 0),
                            new Vector3(m_cameraFollow.LeftEdge, m_cameraFollow.ButtonEdge, 0),
                            new Vector3(m_cameraFollow.LeftEdge, m_cameraFollow.TopEdge, 0)};
        Handles.DrawLines(lines);
    }

    void DrawScreen(Vector3 position, float size, float wh = 16f / 9f)
    {
        float leftEdge = position.x - size * wh;
        float rightEdge = position.x + size * wh;
        float topEdge = position.y + size;
        float buttonEdge = position.y - size;
        Vector3[] lines = { new Vector3(leftEdge, topEdge, 0),
                            new Vector3(rightEdge, topEdge, 0),
                            new Vector3(rightEdge, topEdge, 0),
                            new Vector3(rightEdge, buttonEdge, 0),
                            new Vector3(rightEdge, buttonEdge, 0),
                            new Vector3(leftEdge, buttonEdge, 0),
                            new Vector3(leftEdge, buttonEdge, 0),
                            new Vector3(leftEdge, topEdge, 0)};
        Handles.DrawLines(lines);
    }

    static void DrawCross(Vector3 position, float length)
    {
        Handles.DrawLine(position + new Vector3(0, length, 0), position + new Vector3(0, -length, 0));
        Handles.DrawLine(position + new Vector3(length, 0, 0), position + new Vector3(-length, 0, 0));
    }

    static void DrawCircle(Vector3 position, float radius, int lineNum)
    {
        float angle = 360 / lineNum;
        List<Vector3> vec3li = new List<Vector3>();
        Vector3 olddir = new Vector3(0, 1, 0) * radius;
        vec3li.Add(olddir);
        for (int i = 0; i < lineNum; i++)
        {
            vec3li.Add(Quaternion.AngleAxis(angle, Vector3.forward) * vec3li[i]);
        }

        for (int i = 0; i < vec3li.Count; i++)
        {
            vec3li[i] += position;
        }
        for (int i = 1; i < vec3li.Count; i++)
        {
            Handles.DrawLine(vec3li[i], vec3li[i - 1]);
        }

    }



    private void OnSceneGUI()
    {

        if (m_cameraFollow != null)
        {
            Handles.color = Color.green;
            m_cameraFollow = (CameraFollow)target;
            DrawEdge();
            DrawCross(m_cameraFollow.PlayerCenter, 1.0f);
            DrawCircle(m_cameraFollow.PlayerCenter, m_cameraFollow.StartFollowDis, 30);
            DrawScreen(m_cameraFollow.PlayerCenter, m_cameraFollow.TargetSize);
            Handles.color = Color.red;
            DrawScreen(Camera.main.transform.position, m_cameraFollow.SizeMax);
            DrawScreen(Camera.main.transform.position, m_cameraFollow.SizeMin);


            m_cameraFollow.CameraScaleFollow();
            m_cameraFollow.CameraScaleLimit();
            m_cameraFollow.CameraPositionFollow();
            m_cameraFollow.CameraPositionLimit();

        }
    }



}
