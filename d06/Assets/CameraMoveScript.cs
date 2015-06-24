using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {
	
	bool moveRight;
	bool moveLeft;
	// Use this for initialization
	void Start () {
		moveRight = false;
	}
	
	void Update ()
	{
		if (transform.rotation.eulerAngles.y < 130 && transform.rotation.eulerAngles.y > 120 && moveRight)
			moveRight = false;
		else if (transform.rotation.eulerAngles.y > 250 && transform.rotation.eulerAngles.y < 260 && !moveRight)
			moveRight = true;
		
		if (moveRight)
			transform.Rotate(Vector3.up * - Time.deltaTime * 10);
		else
			transform.Rotate(Vector3.up * Time.deltaTime * 10);
		
//		Debug.Log (moveRight + " " + transform.rotation.eulerAngles.y);
	}
}
