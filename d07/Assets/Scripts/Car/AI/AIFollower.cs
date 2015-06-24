using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollower : CarControl {

	public bool isSeen = false;
	public bool follow = false;

	[SerializeField]private Transform target;
	
	private NavMeshAgent agent;

	void Start() {
		agent = GetComponentInChildren<NavMeshAgent> ();
	}

	void FixedUpdate() {
		if (target != null && follow) {
			agent.SetDestination (target.position);
			Control(agent.desiredVelocity.x, agent.desiredVelocity.z, false);
		}
		else
			Control(0, 0, false);
	}
	
	private bool MissleShot() {
		RaycastHit hit;
		Ray center = new Ray (spawnPoint [1].transform.position, -spawnPoint [1].transform.forward);
		if (Physics.Raycast (center, out hit)) {
			CarControl car = hit.transform.GetComponent<CarControl> ();
			if (car != null)
				return true;
		}
		return false;
	}

	void Update() {
		if (isSeen) {
			if (Shoot[1] && MissleShot())
				Fire(Weapon.Missile);
			Fire(Weapon.Mitrailleuse);
		}
	}
}
