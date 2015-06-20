using UnityEngine;
using System.Collections;

public class MoneyRain : MonoBehaviour {

	[SerializeField]private GameObject money;
	[SerializeField]private Transform sonic;

	private bool[] ahmed = new bool[4]{false, false, false, false};
	private bool isRain = false;
	public GameObject cheat;

	IEnumerator destroyMoney() {
		yield return new WaitForSeconds (2.5f);
		foreach (Transform child in cheat.transform)
			Destroy (child.gameObject);
	}

	IEnumerator HeavyRain() {
		isRain = true;
		StartCoroutine("StartRain");
		yield return new WaitForSeconds (3f);
		isRain = false;
		StartCoroutine ("destroyMoney");
	}
	
	IEnumerator StartRain() {
		while (isRain) {
			for (int i = (int)(sonic.position.x-20); i<=(int)(sonic.position.x+20); i++) {
				GameObject rn = Instantiate (money, new Vector2 (i, 20), Quaternion.identity) as GameObject;
				rn.transform.parent = cheat.transform;
			}
			yield return new WaitForSeconds(0.2f);
		}
	}

	void Update () {
		if (CheckAhmed ())
			StartCoroutine ("HeavyRain");
	}

	bool CheckAhmed() {
		if (Input.GetKeyDown(KeyCode.A)) {
			ahmed = new bool[4]{true, false, false, false};
			return false;
		}
		
		if (Input.anyKeyDown && ahmed[0])
		{
			if (!ahmed[1])
			{
				if (Input.GetKeyDown(KeyCode.H))
					ahmed[1] = true;
				else
					ahmed = new bool[4]{false, false, false, false};
			}
			else if (!ahmed[2])
			{
				if (Input.GetKeyDown(KeyCode.M))
					ahmed[2] = true;
				else
					ahmed = new bool[4]{false, false, false, false};
			}
			else if (!ahmed[3])
			{
				if (Input.GetKeyDown(KeyCode.E))
					ahmed[3] = true;
				else
					ahmed = new bool[4]{false, false, false, false};
			}
			else if (ahmed[3])
			{
				ahmed = new bool[4]{false, false, false, false};
				if (Input.GetKeyDown(KeyCode.D))
					return true;
			}
		}
		return false;
	}
}