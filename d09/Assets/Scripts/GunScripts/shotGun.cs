using UnityEngine;
using System.Collections;

public class shotGun : MonoBehaviour {
	
	private bool canShot;
	// Use this for initialization
	void Start () {
		canShot = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (canShot)
				StartCoroutine(launchParticle());
			canShot = false;
		}
		
	}
	
	IEnumerator launchRay(Vector3 start, Vector3 target, RaycastHit hit) {
		Vector3 startPosition = start;
		Vector3 dir = (target - start).normalized;
		Vector3 endPosition = start;
		
		GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
		GetComponent<LineRenderer> ().SetColors (Color.clear, Color.yellow);
		while (Vector3.Distance(startPosition, target) > 3) {
			if (Vector3.Distance(endPosition, target) > 1)
				endPosition = startPosition + 3 * dir;
			GetComponent<LineRenderer> ().SetPosition(0, startPosition);
			GetComponent<LineRenderer> ().SetPosition(1, endPosition);
			yield return new WaitForSeconds(0.005f);
			startPosition += 5 * dir;
		}
		//GetComponent<ParticleSystem> ().transform.position = hit.point;
		//GetComponent<ParticleSystem> ().Play();
	}
	
	IEnumerator impact()
	{
		transform.parent.Rotate(new Vector3(0.0f, 10.0f,0.0f));
		yield return new WaitForSeconds (0.05f);
		transform.parent.Rotate(new Vector3(0.0f, -10.0f,0.0f));
	}
	
	IEnumerator launchParticle() {
		RaycastHit[] hits;
		//Vector3 direction = new Vector3(transform.parent.parent.parent.parent.transform.rotation.eulerAngles.y, transform.parent.parent.parent.transform.rotation.eulerAngles.x, 0);
		//Debug.Log ("Direction: " + direction);
		Debug.Log (transform.parent.parent.parent.transform.position + " " + transform.parent.parent.parent.transform.forward);
		Debug.DrawRay (transform.parent.parent.parent.transform.position, transform.parent.parent.parent.transform.forward, Color.green, 50);
		hits = Physics.RaycastAll(transform.parent.parent.parent.transform.position, transform.parent.parent.parent.transform.forward, Mathf.Infinity);
		//hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0)), 1000.0f);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.tag != "Player"/* && hit.collider.gameObject.transform.position != new Vector3(0, 0, 0)*/) {
				StartCoroutine(impact());
				StartCoroutine(launchRay(transform.parent.FindChild("Cannon Point").position, hit.point, hit));
				GetComponent<ParticleSystem> ().transform.position = hit.point;
				GetComponent<ParticleSystem> ().Play();
				//GetComponent<LineRenderer> ().SetPosition(0, transform.parent.FindChild("Cannon Point").position);
				//GetComponent<LineRenderer> ().SetPosition(1, hit.point);
				Debug.Log(hit.collider.tag + " " + hit.collider.transform.position);
				break ;
				//if (hit.collider.tag!= "Player")
			}
		}
		yield return new WaitForSeconds (0.3f);
		canShot = true;
	}
}