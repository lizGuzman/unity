using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

	[SerializeField]private AIFollower ai;
	[SerializeField]private rotateCanon parent;

	private Vector3 lockPos;
	private Quaternion lockRot;

	void Start() {
		lockPos = transform.localPosition;
		lockRot = transform.localRotation;
	}

	void FixedUpdate() {
		transform.localRotation = lockRot;
		transform.localPosition = lockPos;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Tank") {
			parent.root = false;
			parent.transform.LookAt(2 * parent.transform.position - other.transform.position);//other.transform);
			ai.isSeen = other.transform;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Tank") {
			if (other.gameObject.activeSelf == false) {
				parent.root = true;
				ai.isSeen = null;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Tank") {
			parent.root = true;
			ai.isSeen = null;
		}
	}
}