using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxes {
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2,
}
public class PlayerCamera : MonoBehaviour {

    public float m_sensitivityX = 15f;
    public float m_sensitivityY = -15f;
    [SerializeField] private float m_minimumX = -360f;
    [SerializeField] private float m_maximumX = 360f;
    [SerializeField] private float m_minimumY = -60f;
    [SerializeField] private float m_maximumY = 60f;
    [SerializeField] private RotationAxes m_axes = RotationAxes.MouseXAndY;
    [SerializeField] private float m_rotationY = 0F;
    [SerializeField] Transform m_player;
    private void FixedUpdate () {

        float m_RotationX = m_player.localEulerAngles.y + Input.GetAxis ("Mouse X") * m_sensitivityX * Time.fixedTime;

        m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
        m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);

        transform.localEulerAngles = new Vector3 (-m_rotationY, 0, 0);
        m_player.localEulerAngles = new Vector3 (0, m_RotationX, 0);

    }

}