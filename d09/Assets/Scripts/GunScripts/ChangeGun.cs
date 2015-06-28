using UnityEngine;
using System.Collections;

public class ChangeGun : MonoBehaviour {
	
	private bool isgun1 = true;
	private bool isgun2 = false;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			if (!isgun1)
			{
				isgun1 = true;
				isgun2 = false;
				this.transform.GetChild(0).gameObject.SetActive(true);
				this.transform.GetChild(1).gameObject.SetActive(false);
				// change gun
			}
		}
		else if (Input.GetKeyDown ("2")) {
			if (!isgun2)
			{
				isgun2 = true;
				isgun1 = false;
				this.transform.GetChild(0).gameObject.SetActive(false);
				this.transform.GetChild(1).gameObject.SetActive(true);
				// change gun
			}
		}
	}
}
