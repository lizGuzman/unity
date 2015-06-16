using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	[SerializeField] private Transform pipe;
	private int score = 0;
	private bool dead = false;
	private bool show = true;

	private bool isDead () {
		if (transform.position.x < (pipe.position.x + 0.7f) && transform.position.x > (pipe.position.x - 0.7f)) {
			if (transform.position.y > 3.149f || transform.position.y < 0.128f)
				return true;
		}
		if (transform.position.y <= -1.87f || transform.position.y >= 5.9f)
			return true;
		return false;
	}

	void ShowScore() {
		if (pipe.position.x <= -1.25f && pipe.position.x >= -1.28f && !dead)
			score += 5;
		if (dead && show)
		{
			Debug.Log ("Score : " + score);
			Debug.Log ("Time : " + Mathf.RoundToInt(Time.realtimeSinceStartup) + "s");
			show = false;
		}
	}

	void Update () {
		ShowScore ();
		if (isDead ()) {
			pipe.gameObject.GetComponent<Pipe> ().stop = true;
			dead = true;
		}
		else {
			transform.Translate (Vector3.down * Time.deltaTime * 2);
			if (Input.GetKeyDown (KeyCode.Space))
				transform.Translate (Vector3.up * 2);
		}
	}
}