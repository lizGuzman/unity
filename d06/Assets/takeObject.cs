using UnityEngine;
using System.Collections;

public class takeObject : MonoBehaviour {

	public Transform man;
	private Transform taker;
	private bool _card;

	// Use this for initialization
	void Start () {
		_card = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("t")) {
			Debug.Log("t");
			transform.parent = taker;
			MusicScript.instance.Panic();
			
		} else if(Input.GetKeyDown("g")) {
			Debug.Log("g");
			transform.parent = null;
			
		}
	}

	void OnGUI () {

		GUI.color = Color.yellow;

		if (_card && man.childCount != 2) {
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("You must press T on the key to get it!!");
			GUILayout.EndArea();
		}

	}

	void OnTriggerEnter (Collider col) {
		
		if(col.CompareTag("Player") ) {
			
			taker = col.transform;
			Debug.Log (taker);
			
		}
			_card = true;

	}    
	
	void OnTriggerExit (Collider col) {
		
		if(col.CompareTag("Player")) {
			taker = null;
		}
		_card = false;

	}
}
