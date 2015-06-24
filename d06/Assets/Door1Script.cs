using UnityEngine;
using System.Collections;

public class Door1Script : MonoBehaviour {

	public AudioSource sound;
	private bool isOpen;

	// Use this for initialization
	void Start () {
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		if (!isOpen && collider.gameObject.name == "FPSController") {
			JointMotor motor = new JointMotor();
			motor.targetVelocity = -20;
			motor.force = 1000;
			GetComponent<HingeJoint> ().motor = motor;
			GetComponent<HingeJoint> ().useMotor = true;
			sound.Play ();
			StartCoroutine(StopSound());
		}
	}

	IEnumerator StopSound() {
		float time = 0;
		while (transform.rotation.eulerAngles.y < 329) {
			yield return new WaitForSeconds (0.1f);
			time += Time.deltaTime;
			sound.volume -= 0.05f;
		}
		isOpen = true;
		sound.Stop ();
		MusicScript.instance.UpPitch();
	}
}
