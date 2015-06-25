using UnityEngine;
using System.Collections;

public class rotateCanon : MonoBehaviour {
		
	private float center;
	private float forceRotate = 50f;
	private Quaternion rotation = Quaternion.identity;

	public bool isHuman = false;
	public bool root = true;
	public Transform spawn;
	public GameObject tank; 
	
	void Rotate () {
		transform.RotateAround (tank.transform.position, Vector3.up, forceRotate * Time.deltaTime);
		Quaternion tmp = tank.transform.rotation;
		Quaternion tmp2 = transform.rotation;
		Vector3 temp = tmp.eulerAngles;
		Vector3 temp2 = tmp2.eulerAngles;
		temp2.x = temp.x;
		temp2.z = temp2.z;
		rotation.eulerAngles = temp2;
		transform.rotation = rotation;
	}
	
	void Update () {
		if (isHuman) {
			center = Screen.width / 2;
			forceRotate = (Input.mousePosition.x - center) / 8;
		}

		transform.position = tank.transform.position;

		if (isHuman || root)
			Rotate ();
	}
}