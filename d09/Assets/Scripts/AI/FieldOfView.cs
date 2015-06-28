using UnityEngine;
using System.Collections;

public class FieldOfView : MonoBehaviour {

	private Enemies enemy;

	void Awake() {
		enemy = transform.GetComponentInParent<Enemies>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.root.tag == "Player") {
			enemy.target = other.transform;
			enemy.transform.LookAt(other.transform);
			enemy.attack = true;
		}
	}

	IEnumerator StopFollowing() {
		yield return new WaitForSeconds (enemy.FollowTime);
		enemy.SetOldTarget();
	}

	void OnTriggerExit(Collider other) {
		if (other.transform.root.tag== "Player") {
			enemy.attack = false;
			if (enemy.FollowTime > 0)
				StartCoroutine("StopFollowing");
		}
	}
}