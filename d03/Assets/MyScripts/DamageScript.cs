using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		print(other.name);
		int damage = other.GetComponent<ennemyScript>().ennemyDamage;
		gameManager.instance.damagePlayer(damage);
		Destroy (other.gameObject);
	}
}
