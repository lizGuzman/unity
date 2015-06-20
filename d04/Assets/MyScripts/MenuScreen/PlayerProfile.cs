using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerProfile : MonoBehaviour {

	[SerializeField]private Text LifeText;
	[SerializeField]private Text GoldText;
	[SerializeField]private Text ScoreText;
	[SerializeField]private Text LevelText;

	[SerializeField]private Image select;
	[SerializeField]private Image level2;
	[SerializeField]private Sprite sp;
	[SerializeField]private GameObject[] Levels;
	
	private int h = Screen.height;
	private int w = Screen.width;

	private int pos = 0;

	public static void ResetProfile() {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt ("Life", 0);
		PlayerPrefs.SetInt ("Gold", 0);
		PlayerPrefs.SetInt ("Score", 0);
	}

	void Start() {
		if (PlayerPrefs.HasKey ("ahmed"))
			level2.sprite = sp;

		select.rectTransform.position = Levels[0].GetComponent<RectTransform> ().position;
		LevelText.text = Levels[0].name;

		if (!PlayerPrefs.HasKey ("Life"))
			ResetProfile ();
		LifeText.text = PlayerPrefs.GetInt ("Life").ToString();
		GoldText.text = PlayerPrefs.GetInt ("Gold").ToString();
		ScoreText.text = "BEST SCORE : " + PlayerPrefs.GetInt ("Score").ToString() + " PTS";
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (pos > 0)
				pos--;
			select.rectTransform.position = Levels[pos].GetComponent<RectTransform> ().position;
			LevelText.text = Levels[pos].name;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (pos < 11)
				pos++;
			select.rectTransform.position = Levels[pos].GetComponent<RectTransform> ().position;
			LevelText.text = Levels[pos].name;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (pos - 4 >= 0)
				pos -= 4;
			select.rectTransform.position = Levels[pos].GetComponent<RectTransform> ().position;
			LevelText.text = Levels[pos].name;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if (pos + 4 <= 11)
				pos += 4;
			select.rectTransform.position = Levels[pos].GetComponent<RectTransform> ().position;
			LevelText.text = Levels[pos].name;
		}

		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
			int load = Application.loadedLevel + pos + 1;
			if (load <= Application.levelCount)
				Application.LoadLevel(load);
		}
	}
}
