using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HumanControl : CarControl {
	
	void FixedUpdate () {
		Control(Input.GetAxis("Vertical"), Input.GetAxis ("Horizontal"), Input.GetButton("Vertical"));
		Ray center = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		if (Input.GetMouseButton (0))
			Fire (Weapon.Mitrailleuse, center);
		else if (Input.GetMouseButtonDown (1)) {
			Fire (Weapon.Missile, center);
		}
	}

	void Update() {
		EngineSound ();
	}
}
