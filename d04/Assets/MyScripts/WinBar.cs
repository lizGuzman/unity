using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class WinBar : MonoBehaviour {

	[SerializeField]private Text text;
	[SerializeField]private GameObject cam;
	private bool won = false;
	private bool wont = false;
	private int t;

	IEnumerator ShowScore() {
		won = true;
		yield return new WaitForSeconds (6f);
		text.gameObject.SetActive(true);
		print (text.enabled);
		wont = true;
		text.text += t.ToString();
	}

	void CalculateScore() {
		t = Mathf.RoundToInt(Time.timeSinceLevelLoad) * -100;
		t += RingScript.score * 100;
		if (PlayerPrefs.GetInt ("Score") < t)
			PlayerPrefs.SetInt ("Score", t);
		print (t);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !won) {
			cam.GetComponent<AudioSource>().Stop();
			transform.GetComponent<AudioSource> ().Play ();
			CalculateScore();
			PlayerPrefs.SetInt("ahmed", 1);
			StartCoroutine("ShowScore");
		}
	}

	void FixedUpdate() {
		if (won) {
			if (Input.anyKeyDown && wont)
				Application.LoadLevel(1);
			transform.Rotate (0.0f, 1f, 0.0f);
		}
	}
}