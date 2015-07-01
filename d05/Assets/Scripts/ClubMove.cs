using UnityEngine;
using System.Collections;

public class ClubMove : MonoBehaviour {

	#region Attributes
	private Vector3 src = new Vector3 (0, 20, 0);
	private Vector3 dest = new Vector3 (0, -20, 0);
	private bool isClub = false;
	private float init = 345f;
	private float step1;
	private float step2 = 435f;
	private float speed = 100f;

	bool isInit = false;
	bool isStep1 = false;
	bool isStep2 = false;
	GameObject ball;
	int per;
	#endregion

	void OnTriggerEnter(Collider other) {
		print (other.tag);
		if (other.tag == "Ball") {
			isClub = false;
			gameObject.SetActive(false);
			ball.GetComponent<ClubForce>().kickForce = per;
		}
	}

	public void Moveclub(int per, GameObject ball) {
		this.ball = ball;
		step1 = init - per;
		isClub = true;
		this.per = per;
	}

	void Update() {
		if (isClub) {
			print("as");
			if (!isInit) {
				isInit = true;
				transform.Rotate (init, 0, 0);
			} else if (!isStep1) {
				transform.Rotate(-speed * Time.deltaTime, 0, 0);
				if (transform.rotation.eulerAngles.x < step1)
					isStep1 = true;
			} else if (!isStep2) {
				transform.Rotate(speed * Time.deltaTime, 0, 0);
				if (transform.rotation.eulerAngles.x > step2)
					isStep2 = true;
			} else {
				isClub = false;
				print("sa");
				gameObject.SetActive(false);
				ball.GetComponent<ClubForce>().kickForce = per;
			}
		}
	}
}