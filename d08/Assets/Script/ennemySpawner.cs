using UnityEngine;
using System.Collections;

public class ennemySpawner : MonoBehaviour {

	public float respawnTime = 5;
	public GameObject[] Ennemy;
	private GameObject[] ennemyInst;
	private Vector3[] positions;
	private float[] times;
	// Use this for initialization






	void Start () {
		int count = 0;
		ennemyInst = new GameObject[10];
		positions = new Vector3[10];
		times = new float[10];
		while (count < 10) {
			ennemyInst[count] = GameObject.Instantiate(Ennemy[Random.Range(0, Ennemy.Length)], new Vector3(Random.Range(40, 160), 8, Random.Range(40, 150)), Quaternion.identity) as GameObject;
			positions[count] = ennemyInst[count].transform.position;
			times[count] = 0;
			count += 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i =0; i < ennemyInst.Length; i++) {
			if (times[i] > respawnTime) {
				ennemyInst[i] = GameObject.Instantiate(Ennemy[Random.Range(0, Ennemy.Length)], positions[i], Quaternion.identity) as GameObject;
				times[i] = 0;
			} else if (!ennemyInst[i]) {
				times[i] += Time.deltaTime;
			}
		}
	}
}