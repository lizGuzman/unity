using UnityEngine;
using System.Collections;

public class mayaAttack : MonoBehaviour {
	private int hitChance;
	private GameObject target;
	private int reward;
	public bool kill = false;
	public bool onTrigger = false;
	public float animTime = 0.5f;

	// ARRETER ATTACK SI ENNEMY TROP LOIN

	IEnumerator attac(Collider other)
	{
		transform.parent.GetComponent<Animator>().SetBool("attackMaya",true);
		transform.parent.GetComponent<Animator>().SetBool("runMaya",false);
		yield return new WaitForSeconds(2);
		/// hit = 75 + AGI - Target.AGI
		///  Random entre minDamage et maxDamage
		target = other.gameObject;
		reward = target.GetComponent<ennemy>().exp;
		while (other.GetComponent<ennemy>().HP > 0 && onTrigger) {
			hitChance = 75 + transform.parent.GetComponent<runMaya> ().agility - other.GetComponent<ennemy> ().agility;
			if (Random.Range (0, 100) < hitChance) {
				other.GetComponent<ennemy> ().HP -= (int)((float)(((float)(1 - ((float)target.GetComponent<ennemy> ().armor / 200.0)) * (Random.Range (transform.parent.GetComponent<runMaya> ().minDamage, transform.parent.GetComponent<runMaya> ().maxDamage)))));
			}
			//Debug.Log (other.GetComponent<ennemy>().HP);
			yield return new WaitForSeconds(animTime);
		}


	}


	void OnTriggerEnter(Collider other) {

		if (other.tag == "ennemy") {
			StartCoroutine(attac(other));  //DEBUG
			onTrigger = true;
		}
	}

	void OnTriggerExit(Collider other) {
		//Debug.Log (other.tag);
		if (other.tag == "ennemy") {
			transform.parent.GetComponent<Animator>().SetBool("attackMaya",false);
			transform.parent.GetComponent<Animator>().SetBool("runMaya",false);
			onTrigger = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (kill == true) {
			kill = false;
//			Debug.Log ("test reward:"+ reward);
			transform.parent.GetComponent<runMaya>().exp += reward;
			transform.parent.GetComponent<Animator>().SetBool("attackMaya",false);
		}

	}
}
