using UnityEngine;
using System.Collections;

public class ZombieScript : Enemies {

	private enum AnimateState
	{
		idle,
		walk,
		attack,
		dead
	};

	private NavMeshAgent agent;
	private Animator animator;
	private AnimateState CharacterAnimationState = AnimateState.idle;
	private Transform OldTarget;

	public Transform getTarget {
		get { return OldTarget; }
	}

	void Awake() {
		FollowTime = 15.0f;
		myGenre = Genre.Zombie;
		agent = transform.GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator>();
		OldTarget = GameObject.FindGameObjectWithTag ("Center").transform;
	}

	void Update() {
		if (target) {
			CharacterAnimationState = AnimateState.walk;
			agent.SetDestination(target.transform.position);
		}
		if (attack)
			CharacterAnimationState = AnimateState.attack;
		PlayAnimation ();
	}

	void AllOff() {
		animator.SetBool ("idle", false);
		animator.SetBool ("walk", false);
		animator.SetBool ("attack", false);
		animator.SetBool ("dead", false);
	}

	private void PlayAnimation () {
		AllOff ();
		switch (CharacterAnimationState) {
			case AnimateState.idle : animator.SetBool ("idle", true);
				break;
			case AnimateState.walk : animator.SetBool ("walk", true);
				break;
			case AnimateState.attack : animator.SetBool ("attack", true);
				break;
			case AnimateState.dead : animator.SetBool ("dead", true);
				break;
		}		
	}
}