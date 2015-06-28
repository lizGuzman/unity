using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour {
	public bool killEn = false;
	private GameObject target;
	private int hitChance;
	public bool onTrigger = false;
	public float animTime = 0.5f;


	IEnumerator attac(Collider other)
	{
		target = other.gameObject;
		transform.parent.GetComponent<Animator>().SetBool("attack",true);
		transform.parent.GetComponent<Animator>().SetBool("walk",false);
		
		yield return new WaitForSeconds(2);
		while (other.GetComponent<runMaya>().HP > 0 && onTrigger) {
			hitChance = 75 + transform.parent.GetComponent<ennemy> ().agility - other.GetComponent<runMaya> ().agility;
			//Debug.Log ("HIT "+ hitChance);
			if (Random.Range (0, 100) < hitChance) {
				other.GetComponent<runMaya> ().HP -= (int)((float)(((float)(1 - ((float)target.GetComponent<runMaya> ().armor / 200.0)) * (Random.Range (transform.parent.GetComponent<ennemy> ().minDamage, transform.parent.GetComponent<ennemy> ().maxDamage)))));
			}
//			Debug.Log ("vie :" + other.GetComponent<runMaya> ().HP);

			yield return new WaitForSeconds (animTime);
		}
	}




	void OnTriggerEnter(Collider other) {
		//Debug.Log (other.tag);
		if (other.tag == "Player") {
			StartCoroutine(attac(other));  //DEBUG	
			onTrigger = true;
		}
	}

	void OnTriggerExit(Collider other) {
		//Debug.Log (other.tag);
		if (other.tag == "Player") {
			transform.parent.GetComponent<Animator>().SetBool("attack",false);
			transform.parent.GetComponent<Animator>().SetBool("walk",false);
			onTrigger = false;
			
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (killEn == true) {
			killEn = false;
			transform.parent.GetComponent<Animator>().SetBool("attack", false);
		}
	}
}
