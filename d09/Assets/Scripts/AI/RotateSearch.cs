using UnityEngine;
using System.Collections;

public class RotateSearch : MonoBehaviour {

	bool moveRight;
	public bool rotate = false;
	// Use this for initialization
	void Start () {
		moveRight = false;
	}
	
	void FixedUpdate ()
	{
		if (rotate) {
			if (transform.rotation.eulerAngles.y < 270 && transform.rotation.eulerAngles.y > -90 && moveRight)
				moveRight = false;
			else if (transform.rotation.eulerAngles.y > 360 && transform.rotation.eulerAngles.y < 0 && !moveRight)
				moveRight = true;
			
			if (moveRight)
				transform.Rotate (Vector3.up * - Time.deltaTime * 10);
			else
				transform.Rotate (Vector3.up * Time.deltaTime * 10);
		}
	}
}