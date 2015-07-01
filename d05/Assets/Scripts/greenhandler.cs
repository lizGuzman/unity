using UnityEngine;
using System.Collections;

public class greenhandler : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<ClubForce>().Green = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<ClubForce>().Green = false;
		}
	}
}
