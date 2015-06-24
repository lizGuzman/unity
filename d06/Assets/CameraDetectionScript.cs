using UnityEngine;
using System.Collections;

public class CameraDetectionScript : MonoBehaviour {
	
	// Use this for initialization
	public GameObject  myFps;
	private detectHandler character;
	
	// Use this for initialization
	void Start () {
		character = myFps.GetComponent<detectHandler> ();
	}
	
	void OnTriggerEnter(Collider other) {
		if (!character.isProtected && !character.isDetect && other.gameObject.tag == "Player") {
			character.isDetect = true;
			character.detect += 75;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			character.isDetect = false;
		}
	}
	
	void Update ()
	{
	}

}