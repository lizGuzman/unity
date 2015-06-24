using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

	[SerializeField]private AIFollower ai;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Tank") {
			transform.GetComponentInParent<rotateCanon>().dourNayek = false;
//			transform.parent.LookAt(other.transform.position);
			ai.isSeen = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Tank") {
			transform.GetComponentInParent<rotateCanon>().dourNayek = false;
//			transform.parent.LookAt (other.transform.position);
			ai.isSeen = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Tank") {
			transform.GetComponentInParent<rotateCanon> ().dourNayek = true;
			ai.isSeen = false;
		}
	}
}