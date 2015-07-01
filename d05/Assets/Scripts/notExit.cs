using UnityEngine;
using System.Collections;

public class notExit : MonoBehaviour {

	void OnTriggerExit(Collider col){
		print (col.tag);
	 if (col.tag == "MainCamera") {
			col.transform.position = new Vector3(140, 14,316);
		}
	}
}
