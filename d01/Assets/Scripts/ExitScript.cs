using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	private int Exit_id = -1;
	private bool inTrigger = false;

	public bool IN {
		get {return inTrigger;}
	}

	void Awake() {
		switch (transform.name) {
		case "Claire Exit" :
			Exit_id = 0;
			break;
		case "Thomas Exit" :
			Exit_id = 1;
			break;
		case "John Exit" :
			Exit_id = 2;
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (Exit_id == collider.gameObject.GetComponent<PlayerScript_ex01> ().ID)
			inTrigger = true;
		else
			print (collider.name + " in " + transform.name);
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (Exit_id == collider.gameObject.GetComponent<PlayerScript_ex01> ().ID)
			inTrigger = false;
	}
}
