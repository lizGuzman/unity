using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public float turnSpeed = 10.0f;        // Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 10.0f;        // Speed of the camera when being panned
	public float zoomSpeed = 10.0f;        // Speed of the camera going back and forth
	
	public float heightLimit = 10.0f;
	
	private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
	private bool isPanning;        // Is the camera being panned?
	private bool isRotating;    // Is the camera being rotated?
	private bool isZooming;        // Is the camera zooming?
	public Vector3 pos; 
	public GameObject ball; 
	//
	// UPDATE
	//
	
	void Update () 
	{
		
		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}
		
		// Get the middle mouse button
		if(Input.GetKey(KeyCode.A))
		{
			// Get mouse origin
			transform.Translate(-0.4F, 0, 0);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			// Get mouse origin
			transform.Translate(0.4F, 0, 0);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			// Get mouse origin
			transform.Translate(0, 0, -0.4F);
		}
		else if(Input.GetKey(KeyCode.W))
		{
			// Get mouse origin
			transform.Translate(0, 0, 0.4F);
		}
		else if(Input.GetKey(KeyCode.Q))
		{
			// Get mouse origin
			if (transform.position.y < heightLimit)
				transform.Translate(0, 0.4F, 0, Space.World);
		}
		else if(Input.GetKey(KeyCode.E))
		{
			// Get mouse origin
			transform.Translate(0, -0.4F, 0, Space.World);
		}
		else if(Input.GetKey(KeyCode.Space))
		{
			// Get mouse origin
			pos = ball.transform.position;
			pos.z += 10;
			transform.position = pos;
		}
		
		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(1)) isPanning=false;
		if (!Input.GetMouseButton(2)) isZooming=false;
		
		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		
		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
			if (move.y > heightLimit)
				move.y = heightLimit;
			transform.Translate(move, Space.Self);
		}
		
		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			if (move.y > heightLimit)
				move.y = heightLimit;
			transform.Translate(move, Space.World);
		}
	}
}
