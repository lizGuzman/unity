using UnityEngine;
using System.Collections;

public class blockingCamera : MonoBehaviour {
	
	//    var target : Transform; var smoothTime = 0.3; private var velocity = Vector3.zero; private var colliding : boolean = false;
	//    
	//    function OnTriggerEnter (){ colliding = true; }
	//    
	//    function OnTriggerExit (){ colliding = false; }
	//    
	//    function Update(){ if (colliding){ transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3(0,1,0), velocity, smoothTime); } 
	//    else{ var hit : RaycastHit; if (Physics.Raycast(transform.position, -transform.forward, hit, 1.5)){ print ("ray has hit"); } 
	//    else{ transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3(0,3.5,-6), velocity, smoothTime); } } }
	//
	public bool colliding;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "Terrain")
			colliding = true;
		//        Debug.Log (other.gameObject.name);
	}
	
	void OnCollisionExit(Collision other) {
		colliding = false;
	}
	
	// Use this for initialization
	void Start () {
		colliding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y >= 52) {
			transform.Translate(0, -1f, 0);
		}
		if (colliding) {
			transform.Translate(0, 0.7f, 0); // Si collide
		}  
	}

}