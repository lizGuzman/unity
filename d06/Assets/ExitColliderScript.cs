using UnityEngine;
using System.Collections;

public class ExitColliderScript : MonoBehaviour {

	private GameObject exit;
	// Use this for initialization
	void Start () {
		exit = GameObject.Find ("prop_lift_exit");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "FPSController") {
			exit.GetComponent<ExitDoorScript>().isCollider = true;
		}
	}
}
