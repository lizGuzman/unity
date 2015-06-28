using UnityEngine;
using System.Collections;

public class BossScript : Enemies {

	private Animation myAnim;
	private NavMeshAgent agent;

	void Awake() {
		FollowTime = -1.0f;
		myGenre = Genre.Boss;
		agent = transform.GetComponent<NavMeshAgent> ();

		myAnim = transform.GetComponent<Animation> ();
		myAnim.wrapMode = WrapMode.Loop;
		myAnim["jump"].wrapMode = WrapMode.Clamp;
		myAnim["shoot"].wrapMode = WrapMode.Clamp;
		
		myAnim["idle"].layer = -1;
		myAnim["run"].layer = -1;
	}
	
	void Update() {
		if (target) {
			myAnim.CrossFade("run");
			agent.SetDestination(target.transform.position);
		}
		else
			myAnim.CrossFade("idle");

		if (attack)
			myAnim.CrossFadeQueued("shoot", 0.3f, QueueMode.PlayNow);
	}
}