using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {

	#region Attributes
	[SerializeField]private Transform	_physicalBoard;
	[SerializeField]private	GameObject	Canon;
	[SerializeField]private Texture range;
	
	// array for storing if the 3 mouse buttons are dragging
	private bool		isDragActive;
	
	// for remembering if a button was down in previous frame
	//private bool		downInPreviousFrame;
	
	private bool		ClickEnd;
	private GameObject	draggedObject = null;
	#endregion

	#region Events
	public void OnMouseRelease() {
		if (isDragActive)
		{
			gameManager.instance.playerEnergy -= draggedObject.GetComponent<towerScript>().energy;
			isDragActive = false;
			OnDraggingEnd();
		}
	}

	public void OnDragCallBack() {
		if (gameManager.instance.playerEnergy < Canon.GetComponent<towerScript> ().energy)
			return;
		isDragActive = true;
		OnDraggingStart();
	}

	void OnGUI() {
		if (isDragActive) {
			Vector2 rg = Camera.main.WorldToScreenPoint(draggedObject.transform.position);
			GUI.DrawTexture (new Rect(rg.x, Screen.height - rg.y, 300, 300), range);
		}
	}

	void FixedUpdate() {
		if (isDragActive)
			OnDragging();
	}
	#endregion

	private GameObject getClosestObject () {

		GameObject closestObject = null;
		foreach (Transform obj in _physicalBoard)
		{
			if (obj.childCount > 0)
				continue;
			if(!closestObject)
				closestObject = obj.gameObject;
			if(Vector3.Distance(draggedObject.transform.position, obj.position) <= Vector3.Distance(draggedObject.transform.position, closestObject.transform.position))
				closestObject = obj.gameObject;
		}
		return closestObject;
	}

	#region DragAndDrop
	/// <summary>
	/// Raises the dragging start event.
	/// </summary>
	public virtual void OnDraggingStart()
	{
		if (draggedObject != null)
			draggedObject = null;
		Vector2 offset = Camera.main.WorldToScreenPoint(Input.mousePosition);
		draggedObject = Instantiate (Canon, offset, Canon.transform.rotation) as GameObject;
	}
	
	/// <summary>
	/// Raises the dragging event.
	/// </summary>
	public virtual void OnDragging()
	{
		Vector2 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		draggedObject.transform.position = curPosition;
	}
	
	/// <summary>
	/// Raises the dragging end event.
	/// </summary>
	public virtual void OnDraggingEnd()
	{
		GameObject empty = getClosestObject ();
		if (empty != null) {
			GameObject obj = Instantiate(draggedObject, empty.transform.position, Quaternion.identity) as GameObject;
			obj.transform.parent = empty.transform;
		}
		Destroy(draggedObject);
		draggedObject = null;
		return;
	}
	#endregion
}