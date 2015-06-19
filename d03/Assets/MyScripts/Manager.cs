using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	
	[SerializeField]private Text energy;
	[SerializeField]private Text hp;
	[SerializeField]private Text Score;
	[SerializeField]private Text Grade;
	[SerializeField]private Text Score2;
	[SerializeField]private Text Grade2;
	[SerializeField]private Texture2D cursorTexture;
	[SerializeField]private GameObject show;
	[SerializeField]private GameObject show2;
	[SerializeField]private Transform[] spawner;

	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;
	private bool over = false;
	private int gradeChanger = 50000;
		
	void Update () {
		energy.text = gameManager.instance.playerEnergy.ToString ();
		hp.text = gameManager.instance.playerHp.ToString ();
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		ShowGrade_Score(gameManager.instance.score);

		if (gameManager.instance.lastWave) {
			int x = 0;
			foreach (var sp in spawner)
				x = sp.childCount;
			if (x == 0) {
				Time.timeScale = 0;
				show2.SetActive (true);
			}
		}
	}

	void ShowGrade_Score(int sc) {
		Score.text = sc.ToString ();
		if (sc < gradeChanger)
			Grade.text = "F";
		else if (sc < gradeChanger * 2)
			Grade.text = "D";
		else if (sc < gradeChanger * 4)
			Grade.text = "C";
		else if (sc < gradeChanger * 8)
			Grade.text = "B";
		else if (sc < gradeChanger * 16)
			Grade.text = "A";
		else
			Grade.text = "*****";
		Score2.text = Score.text;
		Grade2.text = Grade.text;
	}

	void OnEnable() {
		gameManager.over += GameOver;
	}

	void OnDisable() {
		gameManager.over -= GameOver;
	}

	void GameOver() {
		show.SetActive (true);
	}
}