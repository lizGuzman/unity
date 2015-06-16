using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed;
	public bool moveClub = false;

	[SerializeField] private GameObject Hole;

	private int score = -20;
	private float change = -0.01f;
	private bool done = false;

	private void moveBall() {
		moveClub = false;
		transform.Translate (new Vector2(0, speed * Time.deltaTime));
		Vector2 temp = transform.position;
		temp.y = Mathf.Clamp (transform.position.y, -4.7f, 4.7f);
		transform.position = temp;

		if (Mathf.Abs (temp.y) >= 4.7f)
		{
			speed *= -1;
			change *= -1;
		}

		speed += change;
	}

	void Update () {
		if (speed > 0.01f || speed < -0.01f)
		{
			if (!done)
			{
				score += 5;
				done = true;
				if (speed < 0)
					change = 0.01f;
			}
			moveBall ();
		}
		else
		{
			done = false;
			change = -0.01f;
			moveClub = true;
		}

		if ((transform.position.y < Hole.transform.position.y + 0.15f) && (transform.position.y > Hole.transform.position.y - 0.15f) && Mathf.Abs (speed) < 0.15f)
		{
			Debug.Log("Score: " + score);
			Destroy (gameObject);
		}
	}
}
