using UnityEngine;
using System.Collections;

public class WayPointChecked : MonoBehaviour {

	[SerializeField]private AIFollower ai;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Tank")
			ai.ParcourWayPoints (transform.name);
	}
}