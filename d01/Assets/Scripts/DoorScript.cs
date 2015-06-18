using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	[SerializeField]private Transform door;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "Thomas") {
			door.Translate(Vector2.right * Time.deltaTime * 100);
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.name == "Thomas") {
			door.Translate(Vector3.left * Time.deltaTime * 100);
		}
	}
}
