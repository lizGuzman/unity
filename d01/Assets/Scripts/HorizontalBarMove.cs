using UnityEngine;
using System.Collections;

public class HorizontalBarMove : MonoBehaviour {

	private Vector3 direction;
	private Vector2 limits;
	private float speed;

	void Awake() {
		if (Application.loadedLevel == 2) {
			speed = 1f;
			limits = new Vector2 (-7f, -4f);
		} else {
			speed = 3f;
			limits = new Vector2 (11f, 21.5f);
		}
	}

	void Start() {
		direction = Vector2.right;
	}

	void Update() {
		transform.Translate (direction * Time.deltaTime * speed);
		Vector2 temp = transform.localPosition;
		temp.x = Mathf.Clamp (transform.localPosition.x, limits.x, limits.y);
		transform.localPosition = temp;
		if (temp.x >= limits.y)
			direction = Vector3.left;
		else if (temp.x <= limits.x)
			direction = Vector3.right;
	}
}
