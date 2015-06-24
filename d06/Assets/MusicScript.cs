using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public static MusicScript instance;
	public AudioSource music;
	public AudioSource musicPanic;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	void Start () {
		music.Play ();
	}

	public void Panic() {
		music.Stop ();
		musicPanic.Play ();
	}

	public void UpPitch() {
		music.pitch += 0.05f;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
