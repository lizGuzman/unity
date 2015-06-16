using UnityEngine;
using System.Collections;

public class PlayerScript_ex00 : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space))
			transform.Translate (Vector3.up * Time.deltaTime * 100);
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (Vector3.left * Time.deltaTime * 3);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.Translate (Vector2.right * Time.deltaTime * 3);
	}
}
