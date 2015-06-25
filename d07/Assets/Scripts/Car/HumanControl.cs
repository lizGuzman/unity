using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HumanControl : CarControl {

	[SerializeField]private Transform tank;
	[SerializeField]private GameObject TurboLight;
	[SerializeField]private TextMesh ammoText;
	[SerializeField]private TextMesh lifeText;

	private float boost = 5.0f;
	private float AdditiannalSpeed = 1.0f;

	public float maxSpeed = 0.1f;
	private float speed = 0.0f;
	private float accel = 0.002f;

	void Start() {
		StartCoroutine (SpeedControl());
	}

	void Update() {
		AdditiannalSpeed = 1.0f;
		if (Input.GetKey (KeyCode.LeftShift) && boost >= 0.5f) {
			TurboLight.SetActive (true);
			boost -= Time.deltaTime;
			AdditiannalSpeed *= 2;
		} else if (boost < 5.0f) {
			TurboLight.SetActive(false);
			boost += Time.deltaTime;
		}

		EngineSound ();
		ammoText.text = missileAmmo.ToString ();
		lifeText.text = life.ToString ();
	}

	void FixedUpdate () {
		
		Ray center = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		if (Input.GetMouseButton (0))
			Fire (Weapon.Mitrailleuse, center);
		else if (Input.GetMouseButtonDown (1)) {
			Fire (Weapon.Missile, center);
		}
		
		if (Input.GetKey (KeyCode.W)) {
			if (speed < maxSpeed && speed >= 0)
				speed += accel;
			else if (speed < 0)
				speed *= 0.98f;
			accel = 0;
		} else if (Input.GetKey (KeyCode.S)) {
			if (speed > -maxSpeed && speed <= 0) {
				speed -= accel;
				print(accel + "" + speed);
			}
			else if (speed > 0)
				speed *= 0.98f;
			accel = 0;
		} else {
			speed *= 0.99f;
		}

		if (Mathf.Abs(speed) <= 0.001f)
			speed = 0;

		if (Input.GetKey(KeyCode.A)) {
			Quaternion rotation = GetComponent<Rigidbody>().rotation;
			Vector3 vector = rotation.eulerAngles;
			vector += new Vector3(0, -1, 0);
			GetComponent<Rigidbody>().rotation = Quaternion.Euler(vector);
		} else if (Input.GetKey(KeyCode.D)) {
			Quaternion rotation = GetComponent<Rigidbody>().rotation;
			Vector3 vector = rotation.eulerAngles;
			vector += new Vector3(0, 1, 0);
			GetComponent<Rigidbody>().rotation = Quaternion.Euler(vector);
		}

		transform.Translate (new Vector3 (0, 0, -(speed * AdditiannalSpeed)), Space.Self);
	}

	IEnumerator SpeedControl() {
		while (true) {
			yield return new WaitForSeconds (0.2f);
			accel = 0.002f * (Mathf.Abs(speed) + 1);
		}
	}
}