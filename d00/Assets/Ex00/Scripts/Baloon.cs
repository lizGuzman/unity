using UnityEngine;
using System.Collections;

public class Baloon : MonoBehaviour {

	private	float breath = 1f;

	void Update () {
		bool swollen = false;
		if (breath >= 0.2f)
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				transform.localScale *= 1.1f;
				breath -= 0.2f;
				swollen = true;
			}
		}
		if (!swollen)
		{
			if (transform.localScale.y >= 0.7f)
				transform.localScale /= 1.01f;
			if (breath < 1f)
				breath += 0.025f;
		}
		if (transform.localScale.y >= 3f) {
			Debug.Log("Baloon life time:" + Mathf.RoundToInt(Time.realtimeSinceStartup) + "s");
			Destroy (gameObject);
		}
	}
}
