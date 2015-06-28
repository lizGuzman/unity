using UnityEngine;
using System.Collections;

public class ArmyScript : Enemies {

	private enum AnimateState
	{
		idle,
		aim,
		jump,
		reload,
		walk,
		fire,
		run
	};
	
	private NavMeshAgent agent;
	private Animator animator;
	private AnimateState CharacterAnimationState = AnimateState.idle;
	private Transform OldTarget;

	private float ReloadZeit = 3.5f;

	public Transform getTarget {
		get { return OldTarget; }
		set { OldTarget = value; }
	}
	
	void Awake() {
		myGenre = Genre.Army;
		agent = transform.GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator>();
	}

	void LateUpdate() {
		if (target) {
			CharacterAnimationState = AnimateState.walk;
			agent.SetDestination (target.transform.position);
		}

		if (target.tag == "check") {
			if (agent.remainingDistance <= float.Epsilon) {
				AllOff ();
				CharacterAnimationState = AnimateState.aim;
			}
		}
			
		if (attack) {
			ReloadZeit -= Time.deltaTime;
			CharacterAnimationState = AnimateState.fire;
		} else
			ReloadZeit = 3.5f;

		if (ReloadZeit < 0.0f) {
			ReloadZeit = 3.5f;
			CharacterAnimationState = AnimateState.reload;
		}
//		print (Time.deltaTime);
		PlayAnimation ();
	}
	
	void AllOff() {
		print ("idle");
		animator.SetBool ("Aim", false);
		animator.SetBool ("Jump", false);
		animator.SetBool ("Reload", false);
		animator.SetBool ("Walk", false);
		animator.SetBool ("Fire", false);
		animator.SetBool ("Run", false);
	}
	
	private void PlayAnimation () {
		AllOff ();
		switch (CharacterAnimationState) {
		case AnimateState.aim : animator.SetBool ("Aim", true);
			break;
		case AnimateState.jump : animator.SetBool ("Jump", true);
			break;
		case AnimateState.reload : animator.SetBool ("Reload", true);
			break;
		case AnimateState.walk : animator.SetBool ("Walk", true);
			break;
		case AnimateState.fire : animator.SetBool ("Fire", true);
			break;
		case AnimateState.run : animator.SetBool ("Run", true);
			break;
		default: AllOff();
			break;
		}		
	}
}