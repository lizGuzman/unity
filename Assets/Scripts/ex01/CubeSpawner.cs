using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

	#region Attributes
	[SerializeField] private GameObject[] KeyLine;
	[SerializeField] private GameObject[] keys;
	[SerializeField] private GameObject LineDown;

	private bool[] isKey = new bool[]{false, false, false};
	private KeyCode[] Key_Code = new KeyCode[]{KeyCode.A, KeyCode.S, KeyCode.D};
	private GameObject[] Boxes = new GameObject[]{null, null, null};
	#endregion

	private GameObject CreateBox (GameObject box, Transform line, int choose) {
		GameObject obj = Instantiate (box, new Vector3(line.position.x, transform.position.y, 0), box.transform.rotation) as GameObject;
		Cube cube = obj.AddComponent<Cube> ();
		cube.LineDown = LineDown;
		cube.key = Key_Code [choose];
		return obj;
	}

	void Update() {
		int choose = Random.Range (0, 3);
		if (!isKey[choose])
		{
			Boxes[choose] = CreateBox(keys[choose], KeyLine[choose].transform, choose);
			isKey[choose] = true;
		}
		for (int i=0; i<3; i++)
		{
			if (Boxes[i] == null && isKey[i])
				isKey[i] = false;
		}
	}
}
