using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainLoop : MonoBehaviour {

	[SerializeField]private MovePlayer player;

	private List<MovePlayer> Players = new List<MovePlayer>();
	private float dif = 11f;

	void Update () {
		OnMouseDown ();
	}

	void OnMouseDown() {

		dif += Time.deltaTime;

		if (dif >= 10f) {
			Players.Add (Instantiate (player) as MovePlayer);
			dif = 0f;
		}
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit;
			var ray = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (hit = Physics2D.Raycast (ray, transform.position)) {
				foreach (MovePlayer p in Players) {
					if (hit.collider.gameObject == p.gameObject)
					{
						if (!(Input.GetKey (KeyCode.LeftControl)))
						{
							foreach (MovePlayer pd in Players)
								pd.selected = false;
						}
						p.selected = true;
					}
				}
			}
		}
	}

}