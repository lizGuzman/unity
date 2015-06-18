using UnityEngine;
using System.Collections;

public class MyMouseEvent : MonoBehaviour {
	
	public delegate void	MoveTo(Vector3 dest);
	public delegate void	UnselectPlayer();

	public static event MoveTo				OnclickLeft;
	public static event UnselectPlayer		OnclickRight;
	
	bool run;

	void Update () {
		
		if (Input.GetMouseButton (0)) {
			if (OnclickLeft != null)
				OnclickLeft (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		} else if (Input.GetMouseButton (1)) {
			OnclickRight ();
			Debug.Log("Right");
		}
	}
}