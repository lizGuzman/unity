using UnityEngine;
using System.Collections;

public class restart : MonoBehaviour {

	public GameObject maya;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		if (maya.GetComponent<runMaya> ().restart == true) {
			GUI.Box(new Rect(0, Screen.height/4, Screen.width, Screen.height/3), "YOU ARE DEAD");
			if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 3, Screen.width / 2, Screen.height / 10), "TRY AGAIN ?"))
				Application.LoadLevel (0);
			else if (GUI.Button (new Rect (Screen.width / 4, ((Screen.height / 3) + (Screen.height / 10)), Screen.width / 2, Screen.height / 10), "QUIT"))
				Application.Quit ();
		}
	}
}
