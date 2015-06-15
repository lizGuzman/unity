using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	private float speed;
	private bool click = false;

	public GameObject LineDown;
	public KeyCode key;

	void Start () {
		speed = Random.Range (0.4f, 1.5f);
	}
	
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y <= LineDown.transform.position.y || (click = Input.GetKeyDown(key)))
		{
			if (click)
				Debug.Log("Precision: " + (transform.position.y - LineDown.transform.position.y));
			Destroy(gameObject);
		}
	}
}
