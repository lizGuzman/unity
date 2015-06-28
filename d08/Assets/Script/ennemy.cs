using UnityEngine;
using System.Collections;

public class ennemy : MonoBehaviour {

	public Transform destination;
	public GameObject target;
	public GameObject model;
	private NavMeshAgent agent;

	public int HP = 100;
	public int armor = 10;
	public int strengh = 10;
	public int agility = 10;
	public int constitution = 10;
	public int minDamage = 5;
	public int maxDamage = 9;
	public int level = 1;
	public int exp = 0;
	public bool deadD = false;

	IEnumerator dead()
	{
		
		GetComponent<Animator>().SetBool("dead",true);
		yield return new WaitForSeconds(1);
		transform.Translate (0, 0.1f, 0);
		yield return new WaitForSeconds(1);
		transform.Translate (0, 0.1f, 0);
		Destroy (gameObject);
		// MESSAGE DE GAME OVER /////////
	}

	void OnTriggerEnter(Collider other) {	
		//Debug.Log (other.tag);
		if (other.tag == "Player") {
			destination = other.gameObject.transform;
			transform.LookAt(destination.position);
			target = other.gameObject;

		}
	}

	void Start () 
	{

		//DEBUG
		level = model.GetComponent<runMaya> ().level;
		strengh = 4 + ((level - 1) * 15 / 100);
		constitution = 10 + ((level - 1) * 15 / 100);
		agility = 10 + ((level - 1) * 15 / 100);
		HP = constitution;
		///////////
		agent = gameObject.GetComponent<NavMeshAgent>();
		exp += 20 * level;
		minDamage = strengh / 2;
		maxDamage = minDamage + 4;
	}
	void Update () {
//		level = model.GetComponent<runMaya> ().level;
//		strengh = 10 + ((level - 1) * 15 / 100);
//		constitution = 10 + ((level - 1) * 15 / 100);
//		agility = 10 + ((level - 1) * 15 / 100);
//		minDamage = strengh / 2;
//		maxDamage = minDamage + 4;
		if (HP < 0 && deadD == false) {
			deadD = true;
			target.GetComponent<mayaAttack>().kill = true;
			StartCoroutine(dead());

		}
		//if (HP < 100)
		//	Debug.Log ("hp:" + HP);
		if (destination && GetComponent<Animator>().GetBool("attack") == false) {
			agent.SetDestination (destination.position);
			GetComponent<Animator>().SetBool("walk",true);
		}
		if (GetComponent<Animator>().GetBool("attack") == true)
			GetComponent<Animator>().SetBool("walk",false);
		
		//GetComponent<Animator> ().SetBool ("runMaya", true);

	}
}
