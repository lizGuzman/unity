using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	[SerializeField]private Text text;

	public bool isText = false;
	private static bool started = false;

	void Start() {
		gameManager.instance.pause(true);
	}

	void Update() {
		if (isText && started) {
			text.fontSize = 20;
			if (Time.timeScale == 0)
				text.text = "PAUSED";
			else
				text.text = "SPEED : " + Time.timeScale.ToString() + "X";
		}
	}

	public void OnRetryCallBack() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnNextCallBack() {
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void OnStartCallBack() {
		Application.LoadLevel (1);
	}

	public void OnExitCallBack() {
		Application.Quit();
	}

	public void OnPauseCallBack() {
		if (started)
			gameManager.instance.changeSpeed (0);
	}

	public void OnPlayCallBack() {
		if (!started)
			started = true;

		gameManager.instance.changeSpeed (1);
	}

	public void OnSpeedCallBack() {
		if (started)
			gameManager.instance.changeSpeed (2);
	}

	public void OnMaxSpeedCallBack() {
		if (started)
			gameManager.instance.changeSpeed (4);
	}
}