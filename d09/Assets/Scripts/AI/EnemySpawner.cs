using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : Enemies {

	#region Attributes
	[SerializeField]private Transform SpawnerParent;
	[SerializeField]private Transform[] CheckZone;
	[SerializeField]private GameObject Zombie;
	[SerializeField]private GameObject Army;
	[SerializeField]private GameObject Boss;

	private List<Transform> SpawnPoints = new List<Transform>();

	private int BossMaxNb;
	private int ZombieMaxNb;
	private int ArmyMaxNb;

	private float ZombieWait = 0.15f;
	private float ArmyWait = 1.3f;

	private int Wave = 0;

	private List<List<GameObject>> Zombies = new List<List<GameObject>>();
	private List<List<GameObject>> Marins = new List<List<GameObject>>();
	private List<GameObject> Bosses = new List<GameObject>();
	#endregion

	#region Initialization
	void SetSpawnPoints() {
		foreach (Transform spawn in SpawnerParent)
			SpawnPoints.Add (spawn);
	}

	void SetMaxEnemies(int BossNb, int MarinsNb, int ZombieNb) {
		BossMaxNb = BossNb;//SpawnPoints.Count;
		ArmyMaxNb = MarinsNb;//2;
		ZombieMaxNb = ZombieNb;//5;
		if (Bosses.Count == 0) {
			foreach(var span in SpawnPoints) {
				Zombies.Add(new List<GameObject>());
				Marins.Add(new List<GameObject>());
			}
		}
	}

	void Start() {
		SetSpawnPoints ();
		StartCoroutine ("WaveZeit");
	}
	#endregion

	#region DeathEvent
	void	OnEnable() {
		base.OnDeathHandler += Death;
	}
	
	void	OnDisable() {
		base.OnDeathHandler -= Death;
	}

	void Delete(List<List<GameObject>> liste, Enemies entity) {
		foreach (var lst in liste) {
			if (lst.Contains(entity.gameObject)) {
				lst.Remove(entity.gameObject);
				Destroy(entity.gameObject);
				break;
			}
		}
		Destroy(entity.gameObject);
	}

	private void KillBoss(Enemies entity) {
		int id = Bosses.IndexOf (entity.gameObject as GameObject);
		if (id > 0) {
			Bosses.Remove (entity.gameObject);
			Destroy(entity.gameObject);
			Zombies.Remove(Zombies[id]);
			Marins.Remove(Marins[id]);
		}
	}

	public void	Death(Enemies entity) {
		switch (entity.Type) {
		case Genre.Zombie: Delete(Zombies, entity);
			break;
		case Genre.Army: Delete(Marins, entity);
			break;
		case Genre.Boss: KillBoss(entity);
			break;
		}
	}
	#endregion

	#region ArmyCreation
	void CreateBosses() {
		while (Bosses.Count < BossMaxNb) {
			GameObject boss = Instantiate (Boss, SpawnPoints [Bosses.Count].position, Quaternion.identity) as GameObject;
			boss.name = "Boss";
			boss.transform.parent = SpawnPoints[Bosses.Count];
			Bosses.Add(boss);
			Zombies.Add(new List<GameObject>());
			Marins.Add(new List<GameObject>());
		}
	}

	IEnumerator AddZombie (int id, int nb) {
		while (nb > 0) {
			GameObject zombie = Instantiate(Zombie, SpawnPoints[id].position, Quaternion.identity) as GameObject;
			zombie.transform.parent = SpawnPoints[id];
			zombie.name = "Zombie";
			Zombies[id].Add(zombie);
			yield return new WaitForSeconds(ZombieWait);
			nb--;
		}
	}

	IEnumerator AddArmy (int id, int nb) {
		while (nb > 0) {
			GameObject pilot = Instantiate(Army, SpawnPoints[id].position, Quaternion.identity) as GameObject;
			pilot.transform.parent = SpawnPoints[id];
			pilot.name = "Army";
			pilot.GetComponent<ArmyScript>().getTarget = CheckZone[id].GetChild(nb - 1);
			Marins[id].Add(pilot);
			yield return new WaitForSeconds(ArmyWait);
			nb--;
		}
	}

	void CreateArmy() {
		for (int i = 0; i < Marins.Count; i++) {
			int armyToCreate =  ArmyMaxNb - Marins[i].Count;
			if (armyToCreate > 0) StartCoroutine(AddArmy(i, armyToCreate));
		}
	}

	void CreateZombies() {
		for (int i = 0; i < Zombies.Count; i++) {
			int zombieToCreate =  ZombieMaxNb - Zombies[i].Count;
			if (zombieToCreate > 0) StartCoroutine(AddZombie(i, zombieToCreate));
		}
	}
	#endregion

	#region Waves
	void SetWave() {
		print ("here ==> " + Wave);
		switch (Wave) {
		case 0: WaveCreation(0, 0, 1);
			break;
		case 1: WaveCreation(0, 0, 3);
			break;
		case 2: WaveCreation(0, 0, 5);
			break;
		case 3: WaveCreation(0, 2, 5);
			break;
		default : WaveCreation(SpawnPoints.Count, 2, 5);
			break;
		}
	}

	void WaveCreation(int a, int b, int c) {
		SetMaxEnemies (a, b, c);
		CreateBosses ();
		CreateZombies ();
		CreateArmy ();
	}

	void DestroyWave() {
		Zombies.ForEach (temp => temp.ForEach(tmp => Destroy(tmp)));
		Zombies.Clear ();
		Marins.ForEach (temp => temp.ForEach(tmp => Destroy(tmp)));
		Marins.Clear ();
		Bosses.ForEach (tmp => Destroy(tmp));
		Bosses.Clear ();
	}

	IEnumerator WaitForNewWave() {
		yield return new WaitForSeconds (5.0f);
		Wave++;
		StartCoroutine ("WaveZeit");
	}

	IEnumerator WaveZeit() {
		SetWave ();
		yield return new WaitForSeconds (25.0f);
		DestroyWave ();
		StartCoroutine ("WaitForNewWave");
	}
	#endregion
}