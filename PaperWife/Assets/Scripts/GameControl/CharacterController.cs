// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// [AddComponentMenu("Control Script/FPS Input")]
// public class CharacterController : MonoBehaviour {
// 	/*  [SerializeField]private float m_Speed = 6.0f;
// 	[SerializeField]private float m_Gravity = 0;
// 	private CharacterController m_CharacterController;
// 	private void Start() {
// 		m_CharacterController = GetComponent<CharacterController>();
// 	}
// 	private void FixedUpdate() {
// 		float h = Input.GetAxis("Horizontal") * m_Speed;
// 		float v = Input.GetAxis("Vertical") * m_Speed;
// 		Vector3 m_Movement = new Vector3(h , 0 , v);
// 		m_Movement = Vector3.ClampMagnitude(m_Movement, m_Speed);
// 		m_Movement.y = m_Gravity;
// 		m_Movement *= Time.deltaTime;
// 		m_Movement = transform.TransformDirection(m_Movement);
// 		CharacterController.Move(m_Movement);
// 	}*/
// 	void FixedUpdate()
//     {
//         float h = Input.GetAxisRaw("Horizontal");//获取水平方向移动
//         float v = Input.GetAxisRaw("Vertical");//获取垂直方向移动
//         Move(h, v);//角色移动
//         Turning();//角色转向
//     }
//    void Move(float h,float v)
//     {
//         movement.Set(h, 0f, v); //vector3向量
//         movement = movement.normalized * speed * Time.deltaTime; //movement.normalized使向量单位化，结果等于每帧角色移动的距离
//         playerRigidbody.MovePosition(transform.position + movement);//设置刚体移动位置
//     }
//     void Turning()
//     {
//         Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//定义射线检测鼠标位置
//         RaycastHit floorHit; //光线投射碰撞
//         if (Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
//             光线投射，计算在指定层上是否存在碰撞，返回bool
//         {
//             Vector3 playerToMouse = floorHit.point - transform.position;//计算差值
//             playerToMouse.y = transform.position.y;//不改变y轴方向
//             Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
//             四元数用于表示旋转，创建一个旋转，沿着forward（z轴）并且头部沿着upwards（y轴）的约束注视
//             playerRigidbody.MoveRotation(newRotation);
//         }
//     }


	
// }
