using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerCamera))]
public class CameraEditor : Editor
{
    PlayerCamera m_cameraController;
    private void OnEnable()
    {
        m_cameraController = (PlayerCamera)target;
    }
    // Use this for initialization
    private void OnSceneGUI()
    {
        Handles.color = new Color(0.0f, 0.0f, 0.0f);
        Vector3[] edge = {
            new Vector3 (m_cameraController.EdgeLeft, 0.3f, m_cameraController.EdgeUp),
            new Vector3 (m_cameraController.EdgeRight, 0.3f, m_cameraController.EdgeUp),
            new Vector3 (m_cameraController.EdgeRight, 0.3f, m_cameraController.EdgeUp),
            new Vector3 (m_cameraController.EdgeRight, 0.3f, m_cameraController.EdgeBottom),
            new Vector3 (m_cameraController.EdgeRight, 0.3f, m_cameraController.EdgeBottom),
            new Vector3 (m_cameraController.EdgeLeft, 0.3f, m_cameraController.EdgeBottom),
            new Vector3 (m_cameraController.EdgeLeft, 0.3f, m_cameraController.EdgeBottom),
            new Vector3 (m_cameraController.EdgeLeft, 0.3f, m_cameraController.EdgeUp)
        };
        Handles.DrawLines(edge);
        if (m_cameraController.Player != null)
        {
            Handles.DrawWireDisc(m_cameraController.Player.position, new Vector3(0, 1, 0), m_cameraController.NoFollowCircledR);
        }
        Handles.DrawSolidDisc(m_cameraController.FollowPosition, new Vector3(0, 1, 0), 0.3f);
    }

}