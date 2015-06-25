using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollower : CarControl {

	#region Attributes
	[SerializeField]private Transform way;

	public Transform isSeen = null;

	private List<Transform> wayPoints = new List<Transform>();
	private Transform target;
	private NavMeshAgent agent;
	private int current = 0;
	private int previous = -1;
	#endregion

	#region MonoBehaviour
	void Start() {
		GetWayPoints ();
		agent = GetComponentInChildren<NavMeshAgent> ();
		target = wayPoints [0];
	}

	void FixedUpdate() {
		if (target != null) {
			agent.SetDestination (target.position);
			Control(agent.desiredVelocity.x, agent.desiredVelocity.z, false);
		}
		else
			Control(0, 0, false);
	}

	void Update() {
		if (isSeen != null) {
			shutWayPointsDown();
			previous = -2;
			target = isSeen;
			if (Shoot [1] && MissleShot ())
				Fire (Weapon.Missile);
			Fire (Weapon.Mitrailleuse);
		} else if (previous < -1) {
			WakeWayPointUp();
			previous = 0;
			isSeen = null;
			current = 0;
			target = wayPoints[0];
			return;
		}
	}
	#endregion

	#region Missile
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
	#endregion

	#region Movement
	void WakeWayPointUp() {
		transform.GetComponent<NavMeshAgent> ().stoppingDistance = 0;
		foreach (var obj in wayPoints)
			obj.gameObject.SetActive (true);
	}

	void shutWayPointsDown() {
		transform.GetComponent<NavMeshAgent> ().stoppingDistance = 20;
		foreach (var obj in wayPoints)
			obj.gameObject.SetActive (false);
	}

	private void GetWayPoints() {
		foreach (Transform child in way)
			wayPoints.Add (child);
	}

	public void ParcourWayPoints(string name) {
		if (!isSeen) {
			if (name == previous.ToString())
				return;
			previous = current;
			if (current + 1 >= wayPoints.Count)
				current = 0;
			else
				current++;
			target = wayPoints [current];
		}
	}
	#endregion
}