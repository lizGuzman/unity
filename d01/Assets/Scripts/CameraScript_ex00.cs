using UnityEngine;
using System.Collections;

public class CameraScript_ex00 : MonoBehaviour {

	[SerializeField]private Transform[] targets = new Transform[3];

	private Transform target;
//	private float distance = 10.0f;
	private float height = 5.0f;
	private float heightDamping = 2.0f;
	private float rotationDamping = 3.0f;
	
	//	// Place the script in the Camera-Control group in the component menu
	//	@script AddComponentMenu("Camera-Control/Smooth Follow")
	
	void Start() {
		target = targets [0];
		target.GetComponent<PlayerScript_ex00> ().enabled = true;
	}

	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)
			return;
		
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
//		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
//		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		Vector3 temp = transform.position;
		temp.y = currentHeight;
		temp.z = -10.0f;
		transform.position = temp;

		// Always look at the target
		transform.LookAt (target);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Alpha1) && target != targets [0])
		{
			target.GetComponent<PlayerScript_ex00>().enabled = false;
			targets [0].GetComponent<PlayerScript_ex00> ().enabled = true;
			target = targets[0];
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && target != targets[1])
		{
			target.GetComponent<PlayerScript_ex00>().enabled = false;
			targets [1].GetComponent<PlayerScript_ex00> ().enabled = true;
			target = targets[1];
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) && target != targets[2])
		{
			target.GetComponent<PlayerScript_ex00>().enabled = false;
			targets [2].GetComponent<PlayerScript_ex00> ().enabled = true;
			target = targets[2];
		}
		if (Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown (KeyCode.Backspace))
			Application.LoadLevel (Application.loadedLevel);
	}
}
