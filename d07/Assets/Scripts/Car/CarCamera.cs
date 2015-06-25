using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour {

	#region Attributes
	[SerializeField]private Transform car;
	[SerializeField]private Texture2D viseur;

	public float distance = 6.4f;
	public float height = 1.4f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float zoomRatio = 0.5f;
	public float defaultFOV = 60.0f;

	private Vector3 rotationVector;
	private int h = Screen.height;
	private int w = Screen.width;
	#endregion

//	void Start() {
//		Cursor.lockState = CursorLockMode.Locked;
//	}

	void FixedUpdate() {
		Vector3 localVelocity = car.InverseTransformDirection(car.transform.parent.GetComponent<Rigidbody>().velocity);
		if (localVelocity.z < -0.5f)
			rotationVector.y = car.eulerAngles.y + 180;
		else
			rotationVector.y = car.eulerAngles.y;// + 180f;
		float acc = car.transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
		GetComponent<Camera>().fieldOfView = defaultFOV + acc * zoomRatio;
	}
	
	void LateUpdate() {
		float wantedAngle = rotationVector.y;
		float wantedHeight = car.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;

		myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
		var currentRotation = Quaternion.Euler(0, myAngle, 0);

		Vector3 transformTmpPos = car.position;
		transformTmpPos -= currentRotation * Vector3.forward * distance;
		transformTmpPos.y = myHeight;

		transform.position = transformTmpPos;
		transform.LookAt(car);
	}

	void OnGUI() {
		GUI.DrawTexture (new Rect (w/2, h/2, 50, 50), viseur);
	}
}