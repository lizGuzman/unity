using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffect : MonoBehaviour {

	[SerializeField]private Font font;

	private Text text;
	private bool show = true;
	private bool isReset = false;
	private int h = Screen.height;
	private int w = Screen.width;

	void Start () {
		text = transform.GetComponent<Text> ();
		StartCoroutine ("WaitToShow");
		if (!PlayerPrefs.HasKey ("Life"))
			PlayerProfile.ResetProfile ();
	}

	IEnumerator WaitToShow() {
		while (true) {
			yield return new WaitForSeconds (1);
			show = !show;
			text.enabled = show;
		}
	}

	IEnumerator ShowForWhile() {
		isReset = true;
		yield return new WaitForSeconds (1.5f);
		isReset = false;
	}

	void OnGUI() {
		GUIStyle style = new GUIStyle(GUI.skin.button);
		style.fontSize = (int)(Screen.dpi / 5);
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.red;
		if (GUI.Button (new Rect ((w / 35), (h / 10), (w / 10), (h / 20)), "RESET PROFILE", style)) {
			StartCoroutine ("ShowForWhile");
			PlayerProfile.ResetProfile();
		}

		if (isReset) {
			GUIStyle style2 = new GUIStyle();
			style2.font = font;
			style2.fontSize = (int)(Screen.dpi / 3);
			print(style2.fontSize);
			style2.normal.textColor = Color.black;
			GUI.Label(new Rect(((9 * w)/70) + 35, h/8.7f, 30, 30), "OK !", style2);
		}

		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return))
			Application.LoadLevel (1);
	}
}