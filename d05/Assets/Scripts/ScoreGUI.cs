using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	#region Attributes
	[SerializeField]private Font font;

	private int shot = 1;
	private int hole = 1;
	private int[] par = new int[] {3, 4, 3};
	public static Club club = Club.Wood;

	private float h = Screen.height;
	private float w = Screen.width;

	private GUIStyle style = new GUIStyle ();
	#endregion

	void Start() {
		style.font = font;
		style.fontSize = 50;
		style.normal.textColor = Color.white;
	}

	void OnGUI() {
		if (GetComponent<ball> ().scoreDisplay == false) {
			shot = GetComponent<ball> ().hits;
			hole = GetComponent<ball> ().hole + 1;
			GUI.Label (new Rect (w - (w / 7), h - (h / 5), 100, 100), "SHOT " + shot, style);
			GUI.Label (new Rect ((w / 2) - 50, (h / 15), 100, 100), "club : " + club, style);
			GUI.Label (new Rect ((w / 15), h - (h / 5), 100, 100), "POWER", style);
			GUI.Label (new Rect ((w / 15), (h / 5), 100, 100), string.Format ("HOLE {0}\n\n(PAR {1})", hole, par [hole - 1]), style);
		}
	}
}