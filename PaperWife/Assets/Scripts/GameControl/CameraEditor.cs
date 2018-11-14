using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (PlayerCamera))]
public class CameraEditor : Editor {
	PlayerCamera cameraController;
	private void OnEnable () {
		cameraController = (PlayerCamera) target;

	}
	// Use this for initialization
	private void OnSceneGUI() {
		Handles.color = new Color (0.0f, 0.0f, 0.0f);
		Vector3[] edge = {
			new Vector3 (cameraController.EdgeLeft, 0.3f, cameraController.EdgeUp),
			new Vector3 (cameraController.EdgeRight, 0.3f, cameraController.EdgeUp),
			new Vector3 (cameraController.EdgeRight, 0.3f, cameraController.EdgeUp),
			new Vector3 (cameraController.EdgeRight, 0.3f, cameraController.EdgeBottom),
			new Vector3 (cameraController.EdgeRight, 0.3f, cameraController.EdgeBottom),
			new Vector3 (cameraController.EdgeLeft, 0.3f, cameraController.EdgeBottom),
			new Vector3 (cameraController.EdgeLeft, 0.3f, cameraController.EdgeBottom),
			new Vector3 (cameraController.EdgeLeft, 0.3f, cameraController.EdgeUp)
		};
		Handles.DrawLines(edge);
		Handles.DrawWireDisc(cameraController.Player.position, new Vector3(0, 1, 0), cameraController.NoFollowCircledR);
		Handles.DrawSolidDisc(cameraController.FollowPosition, new Vector3(0, 1, 0), 0.3f);
	}

}