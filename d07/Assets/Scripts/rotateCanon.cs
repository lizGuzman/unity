using UnityEngine;
using System.Collections;

public class rotateCanon : MonoBehaviour {

	private float center;
	private float forceRotate;
	public Transform spawn;
	private Quaternion rotation = Quaternion.identity;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		center = Screen.width / 2;
//		forceRotate = (Input.mousePosition.x - center) / 2;
//		transform.RotateAround (transform.position, Vector3.up, forceRotate * Time.deltaTime);
//		Quaternion tmp = transform.rotation;
//		Vector3 temp = tmp.eulerAngles;
//		temp.x = 0;
//		temp.z = 0;
//		tmp.eulerAngles = temp;
//		transform.rotation = tmp;


//		center = Screen.width / 2;
//		forceRotate = (Input.mousePosition.x - center) / 8;
//		transform.RotateAround (transform.position, Vector3.up, forceRotate * Time.deltaTime);
////		
//		Quaternion tmp = transform.rotation;
//		Vector3 temp = tmp.eulerAngles;
//		rotation.eulerAngles = new Vector3(0,temp.y + forceRotate,0);
//		transform.rotation = rotation;
//		GetComponent<Rigidbody>().MoveRotation(rotation);
//		tmp.eulerAngles = temp;
//		transform.rotation = Quaternion.Euler(temp);
	

	}
	
	public float speed = 10;
	public bool dourNayek = true;

	void LateUpdate() {
		if (dourNayek)
			transform.RotateAround (transform.position, Vector3.up, speed * Time.deltaTime);
	}
}