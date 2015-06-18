using UnityEngine;
using System.Collections;

public class VerticalBarMove : MonoBehaviour {

	private Vector3 direction;
	private Vector2 limits;
	private float speed;

	void Awake() {
		speed = 3f;
		limits = new Vector2 (14.5f, 18f);
	}

	void Start() {
		direction = Vector2.up;
	}

	void Update() {
		transform.Translate (direction * Time.deltaTime * speed);
		Vector2 temp = transform.localPosition;
		temp.y = Mathf.Clamp (transform.localPosition.y, limits.x, limits.y);
		transform.localPosition = temp;
		if (temp.y >= limits.y)
			direction = Vector3.down;
		else if (temp.y <= limits.x)
			direction = Vector3.up;
	}
}
