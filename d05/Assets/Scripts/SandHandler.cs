using UnityEngine;
using System.Collections;

public class SandHandler : MonoBehaviour {

//	private Vector3 vel;
//	private float mag;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<ClubForce>().Malus = 0.5f;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<ClubForce>().Malus = 1.0f;
		}
	}
}