using UnityEngine;
using System.Collections;

public class PlayerScript_ex01 : MonoBehaviour {

	[SerializeField] private Transform deathBar;

	private int Player_id = -1;
	private float[] jump = new float[] {50, 80, 120};
	private float[] speed = new float[] {2f, 3f, 4f};

	public int ID {
		get {return Player_id;}
	}

	void Awake() {
		switch (transform.name) {
			case "Claire" :
				Player_id = 0;
				break;
			case "Thomas" :
				Player_id = 1;
				break;
			case "John" :
				Player_id = 2;
				break;
		}
	}

	void Update() {
		if (transform.position.y < deathBar.position.y) {
			Application.LoadLevel(Application.loadedLevel);

		}

//		transform.rotation.Set (0, 0, 0, 0);

		if (Input.GetKeyDown (KeyCode.Space))
			transform.Translate (Vector2.up * Time.deltaTime * jump[Player_id]);

		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Translate (Vector3.left * Time.deltaTime * speed[Player_id]);

		if (Input.GetKey (KeyCode.RightArrow))
			transform.Translate (Vector2.right * Time.deltaTime * speed[Player_id]);
	}
}
