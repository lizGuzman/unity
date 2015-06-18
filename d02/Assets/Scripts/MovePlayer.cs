using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MovePlayer : MonoBehaviour {

	#region Attributes
	Vector3					destination;
	private Animator animator;
	protected AnimateState CharacterAnimationState = AnimateState.idle;
	protected enum AnimateState
	{
		walk_front,
		walk_back,
		walk_right,
		walk_backRight,
		walk_frontRight,
		walk_left,
		walk_backLeft,
		walk_frontLeft,
		idle
	}

	private float precision = 0.3f;
	public bool selected = false;
	#endregion

	#region Events
	void	OnEnable() {
		MyMouseEvent.OnclickLeft += MoveTo;
		MyMouseEvent.OnclickRight += UnselectPlayer;
	}
	
	void	OnDisable() {
		MyMouseEvent.OnclickLeft += MoveTo;
		MyMouseEvent.OnclickRight += UnselectPlayer;
	}

	public void	UnselectPlayer() {selected = false;}

	void	MoveTo( Vector3 dest)
	{
		if (!selected)
			return;
		GetComponent<AudioSource>().Play();
		destination = dest;
		destination.z = transform.position.z;
		CharacterAnimationState = ChooseState (destination);
	}
	#endregion

	#region MonoBehaviour
	void Start () {
		selected = false;
		animator = GetComponent<Animator>();
		destination = transform.position;
	}
	
	void Update () {

		if (!selected && Application.loadedLevel == 0)
			selected = true;

		transform.position = Vector3.MoveTowards(transform.position, destination, 3.75f * Time.deltaTime);
		if (Vector3.Distance (transform.position, destination) < 0.1f) {
			CharacterAnimationState = AnimateState.idle;
			GetComponent<AudioSource>().Stop();
		}
		PlayAnimation ();
	}
	#endregion

	#region Animation
	AnimateState ChooseState(Vector3 target) {
		if ((target.x - transform.position.x) > precision)
		{
			if ((target.y - transform.position.y) > precision)
				return AnimateState.walk_frontRight;
			else if ((transform.position.y - target.y) > precision)
				return AnimateState.walk_backRight;
			else
				return AnimateState.walk_right;
		}
		else if ((transform.position.x - target.x) > precision)
		{
			if ((target.y - transform.position.y) > precision)
				return AnimateState.walk_frontLeft;
			else if ((transform.position.y - target.y) > precision)
				return AnimateState.walk_backLeft;
			else
				return AnimateState.walk_left;
		}
		else
		{
			if (transform.position.y < target.y)
				return AnimateState.walk_front;
			else
				return AnimateState.walk_back;
		}
	}

	protected void PlayAnimation () {
		switch (CharacterAnimationState) {
		case AnimateState.idle : animator.SetTrigger ("walknot");
			break;
		case AnimateState.walk_front : animator.SetTrigger ("walkfront");
			break;
		case AnimateState.walk_back : animator.SetTrigger ("walkback");
			break;
		case AnimateState.walk_right : animator.SetTrigger ("walkright");
			break;
		case AnimateState.walk_frontRight : animator.SetTrigger ("walkfrontright");
			break;
		case AnimateState.walk_backRight : animator.SetTrigger ("walkbackright");
			break;
		case AnimateState.walk_left : animator.SetTrigger ("walkleft");
			break;
		case AnimateState.walk_frontLeft : animator.SetTrigger ("walkfrontleft");
			break;
		case AnimateState.walk_backLeft : animator.SetTrigger ("walkbackleft");
			break;
		}		
	}
	#endregion
}
