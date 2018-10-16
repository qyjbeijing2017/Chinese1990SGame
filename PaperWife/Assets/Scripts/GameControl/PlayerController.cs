using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationAxes {
	MouseXAndY = 0,
	MouseX = 1,
	MouseY = 2,
}
public class PlayerController : MonoBehaviour {
	[SerializeField] private float m_speed = 5f;
	[SerializeField] private Vector3 m_movement;
	[SerializeField] private Rigidbody m_playerRigidbody;
	[SerializeField] private bool m_isJump;
	[SerializeField] private bool m_isOnGround;
	[SerializeField] private float m_jumpForce = 5f;
	public GameObject m_player;

	public float m_sensitivityX = 8f;
	public float m_sensitivityY = 8f;
	[SerializeField] private float m_minimumX = -360f;
	[SerializeField] private float m_maximumX = 360f;
	[SerializeField] private float m_minimumY = -60f;
	[SerializeField] private float m_maximumY = 60f;
	[SerializeField] private RotationAxes m_axes = RotationAxes.MouseXAndY;
	[SerializeField] private float m_rotationY = 0F;
	public GameObject  m_playerCamera;
	// Use this for initialization
	private void Awake () {
		m_playerRigidbody = m_player.GetComponent<Rigidbody> ();

	}

	void Start () {

	}

	// Update is called once per frame
	private void FixedUpdate () {
		//PlayerFollowCamera();
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);

		m_isJump = Input.GetKeyDown (KeyCode.Space);
		if (m_isJump) {
			Jump ();
		}

		
		
		float m_RotationX = m_player.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * m_sensitivityX * Time.fixedTime;

		m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
		m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);

		m_playerCamera.transform.localEulerAngles = new Vector3 (-m_rotationY, 0, 0);
		m_player.transform.localEulerAngles = new Vector3 (0, m_RotationX, 0);

	}
	void Move (float h, float v) {
		m_movement = m_player.transform.forward * v + m_player.transform.right * h;
		m_movement = m_movement.normalized * m_speed * Time.deltaTime;
		m_playerRigidbody.MovePosition (m_player.transform.position + m_movement);
	}
	void Jump () {
		if (m_isOnGround) {
			m_playerRigidbody.velocity = new Vector3 (m_playerRigidbody.velocity.x, 0, m_playerRigidbody.velocity.z);
			m_playerRigidbody.AddForce (Vector3.up * m_jumpForce, ForceMode.Impulse);
			m_isOnGround = false;
		}

	}
	private void OnCollisionEnter (Collision m_Ground) {
		m_isOnGround = true;
	}

}