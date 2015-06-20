using UnityEngine;
using System.Collections;

public class FloatingSurface : MonoBehaviour {

	private float speed;
	private Vector2 limits;
	private Vector3 direction;

	void Start () {
		speed = Random.Range (2, 5);
		direction = Vector2.up;
		limits = new Vector2 (-1f, 12f);
	}
	
	void FixedUpdate () {
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
