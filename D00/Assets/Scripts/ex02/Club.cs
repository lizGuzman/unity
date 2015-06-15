using UnityEngine;
using System.Collections;

public class Club : MonoBehaviour {

	[SerializeField] private GameObject ball;
	[SerializeField] private Transform Hole;

	private float InitialPos;
	private float speed = 0f;
	private Ball BallScript;
	private bool moved = true;
	private bool rotated = false;

	void Start() {
		InitialPos = ball.transform.position.y + 0.15f;
		transform.position.Set(-0.4f, InitialPos, 0);
		BallScript = ball.GetComponent<Ball> ();
	}

	void KeyHandler () 	{
		if (Input.GetKey (KeyCode.Space) && BallScript.moveClub) {
			transform.Translate (new Vector2 (0, -0.02f));
			speed = Mathf.Abs (InitialPos - transform.position.y);
			moved = true;
		}
		else {
			if (!isThere () && transform.position.y < InitialPos && BallScript.moveClub)
			{
				if (!rotated)
					transform.Translate (new Vector2 (0, 0.05f));
				else
					transform.Translate (new Vector2 (0, -0.05f));
			}
			else if (!isThere () && transform.position.y > InitialPos && BallScript.moveClub)
			{
				if (rotated)
					transform.Translate (new Vector2 (0, 0.05f));
				else
					transform.Translate (new Vector2 (0, -0.05f));
			}
			else if (moved && speed > 0) {
				if (rotated)
					speed *= -1;
				BallScript.speed = speed * 2;
				moved = false;
			}
		}
	}

	void RotateClub () {
		if (transform.position.y > Hole.position.y + 0.3f && !rotated) {
			rotated = true;
			transform.Rotate (180, 0, 0);
		}
		else if (transform.position.y < Hole.position.y - 0.3f && rotated) {
			rotated = false;
			transform.Rotate (180, 0, 0);
		}
	}

	void Update () {
		RotateClub ();
		
		if (BallScript != null)
		{
			if (rotated)
				InitialPos = ball.transform.position.y - 0.15f;
			else
				InitialPos = ball.transform.position.y + 0.15f;
			KeyHandler ();
		}
	}

	private bool isThere() {
		if ((transform.position.y >= InitialPos ) && (transform.position.y <= InitialPos + 0.05f))
			return true;
		if ((transform.position.y <= InitialPos ) && (transform.position.y >= InitialPos - 0.05f))
			return true;
		return false;
	}
}
