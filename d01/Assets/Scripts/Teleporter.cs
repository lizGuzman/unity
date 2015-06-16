using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	[SerializeField]private Transform Out;

	void OnTriggerEnter2D(Collider2D other) {
		other.transform.position = Out.position;
	}
}
