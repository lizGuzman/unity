using UnityEngine;
using System.Collections;
using System;

public class PlayerProperties : MonoBehaviour {

	[SerializeField]private Texture Ring;
	[SerializeField]private Font font;

	private Rect RingRect;
	private Rect GoldRect;
	private Rect TimeRect;
	private Rect ScoreRect;

	private float dpi;
	private int h = Screen.height;
	private int w = Screen.width;

	private GUIStyle style;

	void Start() {
		dpi = Screen.dpi;
		style = new GUIStyle ();
		style.font = font;
		style.fontSize = (int)(dpi / 1.5f);
		style.normal.textColor = Color.white;

		RingRect = new Rect (w / 35, h/7, dpi / 2, dpi / 2);
		GoldRect = new Rect ((w / 35) + dpi, h/7, dpi / 2, dpi / 2);
		TimeRect = new Rect (w / 35, h/12, dpi / 2, dpi / 2);
//		ScoreRect = new Rect (w / 35, h/30, dpi / 2, dpi / 2);
	}

	void OnGUI() {
		GUI.DrawTexture (RingRect, Ring);
		GUI.Label (GoldRect, RingScript.score.ToString(), style);

		TimeSpan t = TimeSpan.FromSeconds( Time.timeSinceLevelLoad );
		string time = string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
		GUI.Label (TimeRect, "Time " + time, style);
//		GUI.Label (ScoreRect, "Score " + RingScript.score, style);
	}
}
