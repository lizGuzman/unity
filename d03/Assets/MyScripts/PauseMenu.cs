using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	#region variables
	public GUISkin myskin;

	[SerializeField]private Texture resume;
	[SerializeField]private Texture quit;
	[SerializeField]private Texture leave;

	private bool paused = false;
	private Rect windowRect;
	private Rect waitRect;
	private Rect OverRect;
	private Rect rect;
	private int path = 0;
	private float now;
	
	private int h = Screen.height;
	private int w = Screen.width;
	#endregion
	
	#region Initialisation
	private void Start() {
		rect  = new Rect(10, 10, (h / 12), (h / 12));
		waitRect = new Rect(w / 2 - 30, h / 2 - 30, 100, 75);
		windowRect = new Rect(w / 2 - 100, h / 2 - 100, 200, 150);
	}
	
	private void OnGUI () {
		if (!paused) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				paused = true;
				gameManager.instance.pause (true);
			}
		}
		if (paused) {
			if (path == 0)
				windowRect = GUI.Window (0, windowRect, windowFunc, "Pause Menu");
			else if (path == 1)
				windowRect = GUI.Window (0, windowRect, windowSure, "Pause Menu");
			else
				windowWait ();
		}
	}
	#endregion
	
	#region Menus
	private void windowFunc(int id) {
		if (GUILayout.Button(resume))
		{
			now = Time.realtimeSinceStartup;
			path = 2;
		}
		if (GUILayout.Button (quit)) {
			path = 1;
		}
	}

	private void windowSure(int id) {
		if (GUILayout.Button(resume))
		{
			now = Time.realtimeSinceStartup;
			path = 2;
		}
		if (GUILayout.Button (leave)) {
			Application.Quit();
		}
	}
		
	private void windowWait() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 15;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;
		
		GUI.Label(waitRect, "Get Ready !", style);
		int yet = 3 - (int) (Time.realtimeSinceStartup - now);
		if (yet < 1)
		{
			paused = false;
			path = 0;
			gameManager.instance.pause(false);
		}
		GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 90, 50), yet + "", style);
	}
	#endregion
}