using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	public Transform target;
	public float walkDistance;
	public float runDistance;
	public float height;
	// Use this for initialization
	void Start () {
		//_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		transform.position = new Vector3 (target.position.x, target.position.y + height, target.position.z - walkDistance);
		transform.LookAt (target);
	}

}
