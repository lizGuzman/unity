using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]private Transform BlueOut;
	[SerializeField]private Transform RedOut;
	[SerializeField]private Transform AllOut;

	private static bool done = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "Claire")
			other.transform.position = BlueOut.position;
		if (other.gameObject.name == "Thomas" && !done)
			other.transform.position = RedOut.position;
		if (transform.name == "Both") {
			done = true;
			other.transform.position = AllOut.position;
		}
	}
}
