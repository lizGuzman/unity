using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

	public bool stop = false;

	private float speed = 2;

	void Update () {
		if (!stop) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
			Vector2 temp = transform.position;
			if (temp.x < -6.5f) {
				temp.x = 7;
				transform.position = temp;
				speed += 0.5f;
			}
		}
	}
}
