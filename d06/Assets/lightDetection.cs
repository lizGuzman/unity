using UnityEngine;
using System.Collections;

public class lightDetection : MonoBehaviour {

	// Use this for initialization
	public AudioSource sound;
	public GameObject  myFps;
	private detectHandler character;
	
	// Use this for initialization
	void Start () {
		character = myFps.GetComponent<detectHandler> ();
		StartCoroutine (lightEffect());
	}
	
	void OnTriggerEnter(Collider other) {
		if (!character.isProtected && other.gameObject.tag == "Player") {
			character.isDetect = true;
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

	IEnumerator lightEffect() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));
			if (GetComponent<Light>().enabled) {
				sound.Stop();
				GetComponent<Light>().enabled = false;
			} else {
				sound.Play();
				GetComponent<Light>().enabled = true;
			}
		}
	}
}
