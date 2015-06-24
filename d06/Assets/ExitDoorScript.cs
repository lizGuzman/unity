using UnityEngine;
using System.Collections;

public class ExitDoorScript : MonoBehaviour {

	public AudioSource Sound;
	public AudioSource doorSound;
	public AudioSource hitSound;
	public float delta;
	private GameObject leftDoor;
	private GameObject rightDoor;
	private bool isClose;
	public bool isCollider = false;
	public GameObject door;

	// Use this for initialization
	void Start () {
		isClose = false;
		leftDoor = GameObject.Find ("door_exit_inner_left_001");
		rightDoor = GameObject.Find ("door_exit_inner_right_001");
		StartCoroutine(OpenDoors ());
	}

	// Update is called once per frame
	void Update () {
		if (!isClose && isCollider) {
			StartCoroutine(CloseDoors());
		}
	}

	IEnumerator OpenDoors() {
		doorSound.Play ();
		float tmpDelta = 0;
		while (tmpDelta < delta) {
			yield return new WaitForSeconds(0.1f);
			leftDoor.transform.position -= new Vector3(0, 0, 0.05f);
			rightDoor.transform.position += new Vector3(0, 0, 0.05f);
			leftDoor.transform.localScale -= new Vector3(0, 0, 0.05f);
			rightDoor.transform.localScale -= new Vector3(0, 0, 0.05f);
			tmpDelta += 0.1f;
		}
		doorSound.Stop ();
	}

	IEnumerator Move() {
		Sound.Play ();
		while (transform.position.y > 1.65) {
			yield return new WaitForSeconds(0.05f);
			if (transform.position.y > 3.65) {
				transform.position -= new Vector3(0, 0.01f, 0);
			} else {
				if (Sound.isPlaying)
					Sound.Stop ();
				transform.position -= new Vector3(0, 0.5f, 0);
			}
		}
		hitSound.Play ();
		StartCoroutine (OpenDoors());
		door.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
	}

	IEnumerator CloseDoors() {
		doorSound.Play ();
		float tmpDelta = 0;
		isClose = true;
		while (tmpDelta < delta) {
			yield return new WaitForSeconds(0.1f);
			leftDoor.transform.position += new Vector3(0, 0, 0.05f);
			rightDoor.transform.position -= new Vector3(0, 0, 0.05f);
			leftDoor.transform.localScale += new Vector3(0, 0, 0.05f);
			rightDoor.transform.localScale += new Vector3(0, 0, 0.05f);
			tmpDelta += 0.1f;
		}
		doorSound.Stop ();
		StartCoroutine (Move());
	}
}
