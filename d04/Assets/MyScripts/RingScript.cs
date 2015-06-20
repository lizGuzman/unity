using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RingScript : MonoBehaviour {

	public static int score = 0;

	IEnumerator WaitToDestroy() {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			score++;
			print("col");
			GetComponent<AudioSource>().Play();
			other.gameObject.GetComponent<Sonic>().rings++;
			PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+1);
			StartCoroutine("WaitToDestroy");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			score++;
			GetComponent<AudioSource>().Play();
			other.GetComponent<Sonic>().rings++;
			PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+1);
			StartCoroutine("WaitToDestroy");
		}
	}
}