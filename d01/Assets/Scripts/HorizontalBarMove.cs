using UnityEngine;
using System.Collections;

public class HorizontalBarMove : MonoBehaviour {

	private Vector3 direction;

	void Start() {
		direction = Vector2.right;
	}

	void Update() {
		transform.Translate (direction * Time.deltaTime);
		Vector2 temp = transform.localPosition;
		temp.x = Mathf.Clamp (transform.localPosition.x, -7f, -4f);
		transform.localPosition = temp;
		if (temp.x >= -4)
			direction = Vector3.left;
		else if (temp.x <= -7)
			direction = Vector3.right;
	}
}
