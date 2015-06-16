using UnityEngine;
using System.Collections;

public class CameraScript_ex01 : MonoBehaviour {

	[SerializeField]private Transform[] targets = new Transform[3];
	[SerializeField]private GameObject[] Exits = new GameObject[3];

	private Transform target;
	private float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private ExitScript[] Exit = new ExitScript[3];

	void Start() {
		target = targets [0];
		target.GetComponent<PlayerScript_ex01> ().enabled = true;
		for (int i=0; i<3; i++)
			Exit [i] = Exits [i].GetComponent<ExitScript> ();
	}

	void LateUpdate () 
	{
		if (target)
		{
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Alpha1) && target != targets [0])
		{
			target.GetComponent<PlayerScript_ex01>().enabled = false;
			targets [0].GetComponent<PlayerScript_ex01> ().enabled = true;
			target = targets[0];
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && target != targets[1])
		{
			target.GetComponent<PlayerScript_ex01>().enabled = false;
			targets [1].GetComponent<PlayerScript_ex01> ().enabled = true;
			target = targets[1];
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) && target != targets[2])
		{
			target.GetComponent<PlayerScript_ex01>().enabled = false;
			targets [2].GetComponent<PlayerScript_ex01> ().enabled = true;
			target = targets[2];
		}
		if (Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown (KeyCode.Backspace))
			Application.LoadLevel (Application.loadedLevel);
	}

	void FixedUpdate() {
		int done = 0;
		foreach (var ext in Exit) {
			if (ext.IN)
				done++;
		}
		if (done == 3)
			Application.LoadLevel(Application.loadedLevel + 1);
	}
}