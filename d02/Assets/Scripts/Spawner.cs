using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	[SerializeField]private GameObject player;
	[SerializeField]private Transform spawnPoint;
	public bool isHuman = false;
	
	private List<GameObject> Players = new List<GameObject>();
	private float dif = 11f;
	
	void Update () {
		dif += Time.deltaTime;
		
		if (dif >= 10f) {
			Players.Add (Instantiate (player, spawnPoint.position, player.transform.rotation) as GameObject);
			dif = 0f;
		}

		if (isHuman) {
			OnMouseDown ();
		}
	}
	
	void OnMouseDown() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit;
			var ray = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (hit = Physics2D.Raycast (ray, transform.position)) {
				foreach (var p in Players) {
					if (hit.collider.gameObject == p.gameObject)
					{
						if (!(Input.GetKey (KeyCode.LeftControl)))
						{
							foreach (var pd in Players)
								pd.GetComponent<MovePlayer>().selected = false;
						}
						p.GetComponent<MovePlayer>().selected = true;
					}
				}
			}
		}
	}
}
