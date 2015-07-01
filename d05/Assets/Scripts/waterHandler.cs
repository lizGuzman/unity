using UnityEngine;
using System.Collections;

public class waterHandler : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			other.GetComponent<ClubForce>().Water = true;
		}
	}
	
//	void OnTriggerExit(Collider other) {
//		if (other.tag == "Ball") {
//			other.GetComponent<ClubForce>().Water = false;
//		}
//	}
}
