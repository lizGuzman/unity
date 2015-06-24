using UnityEngine;
using System.Collections;

public class detectHandler : MonoBehaviour {

	public Transform card;
	public bool looser;
	public bool winner;
//	private bool _key;
	private bool _win;
	private bool _card;
	public AudioSource mySound;
	public bool isDetect;
	public bool isRunning;
	private bool _isGuiStart = true;
	private bool soundplaying;
	private bool _Door1;
	public int detectMax = 100; 
	public int SanteActu = 100; 
	public float LargeurBarreSante;
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	private bool dirJauge = false;
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	private GUIStyle emptyStyle = null;
	public float detect;
	public bool isProtected = false;
	
	void Start()
	{
		_win = false;
		_card = false;
		Debug.Log ("start");
		winner = false;
		_Door1 = false;
		looser = false;
		isRunning = false;
		soundplaying = false;
		isDetect = false;
		detect = 0;
		StartCoroutine (guiStart ());
		
	}
	
	IEnumerator guiStart()
	{
		yield return new WaitForSeconds (5);
		_isGuiStart = false;
	}
	
	IEnumerator door1Start()
	{
		yield return new WaitForSeconds (5);
		_Door1 = false;
	}
	
	private void InitStyles()
	{
		if( currentStyle == null && emptyStyle == null)
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 0.5f ) );
			emptyStyle = new GUIStyle( GUI.skin.box );
			emptyStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 0.5f ) );
		}
	}
	
	void OnColliderEnter(Collision collider)
	{
		if (collider.gameObject.tag == "paper") {
			winner = true;
		}
		
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "end") {
			_win = true;
			if (Input.GetKey (KeyCode.Return))
				winner = true;
		}
//		if (collider.gameObject.tag == "card" && card.childCount != 2) {
//			_card = true;
//		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "door1") {
			_Door1 = true;
		}

	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "door1") {
			_Door1 = false;
		}
		if (collider.gameObject.tag == "end") {
			_win = false;
		}
//		if (collider.gameObject.tag == "card") {
//			_card = false;
//		}
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
	
	void Update () {
		if (detect >= 100) {
			looser = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
		}
		isRunning = Input.GetKey (KeyCode.LeftShift);
		if ((isDetect || isRunning) && !looser && !winner)
			StartCoroutine (Detection ());
		else if (!looser && !winner)
			StartCoroutine (Recover ());
		if (detect >= 75 && !soundplaying)
		{
			mySound.Play();
			soundplaying = true;
		}
		if (detect < 75 && soundplaying)
		{
			mySound.Stop();
			soundplaying = false;
		}
	}

	IEnumerator Detection()
	{
		while ((isDetect | isRunning )&& detect <= 100) {
			if (detect < 75)
				detect += 0.6f;
			else
				detect += 0.1f;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	IEnumerator Recover()
	{
		while (!isDetect && !isRunning && detect > 0) {
			detect -= 0.1f;
			yield return new WaitForSeconds(0.5f);
		}
		
	}
	
	void OnGUI () {
		InitStyles ();
		GUI.Box (new Rect (20, 120, 20, 300), "", emptyStyle);
		GUI.Box (new Rect (20, 420 - detect * 4, 20, 4 * detect), "", currentStyle);
		
		GUI.color = Color.yellow;

		if (_win && !winner) {
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("You must press enter to win!!");
			GUILayout.EndArea();
		}
//		if (_card) {
//			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
//			GUILayout.Label("You must press T on the key to get it!!");
//			GUILayout.EndArea();
//		}

		if (_Door1) {
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("You must press E with the key!!");
			GUILayout.EndArea();
		}
		if (_isGuiStart) {
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("You must find the key and escape!!");
			GUILayout.EndArea();
		}
		if (looser) {
			
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("GAME OVER");
			if (GUILayout.Button ("RETRY")) {
				Application.LoadLevel(Application.loadedLevel);
			}
			if (GUILayout.Button ("QUIT")) {
				Application.Quit();
			}
			GUILayout.EndArea();
		} else if (winner) {
			
			GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
			GUILayout.Label("YOU WIN");
			if (GUILayout.Button ("RETRY")) {
				Application.LoadLevel(Application.loadedLevel);
			}
			if (GUILayout.Button ("QUIT")) {
				Application.Quit();
			}
			GUILayout.EndArea();
		} 
	}
	
	void OnParticleCollision(GameObject other) {
		if (!isProtected) {
			isProtected = true;
			StartCoroutine(StopProtected());
		}
	}
	
	IEnumerator StopProtected () {
		yield return new WaitForSeconds (0.2f);
		isProtected = false;
	}
	
}

/*using UnityEngine;
using System.Collections;

public class detectHandler : MonoBehaviour {
	
	public AudioSource mySound;
	public bool isDetect;
	public bool isRunning;
	private bool soundplaying;
	public int detectMax = 100; 
	public int SanteActu = 100; 
	public float LargeurBarreSante;
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	private bool dirJauge = false;
	
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	private GUIStyle emptyStyle = null;
	public float detect;
	public bool isProtected = false;

	void start()
	{
		isRunning = false;
		soundplaying = false;
		isDetect = false;
		detect = 0;
	}
	
	private void InitStyles()
	{
		if( currentStyle == null && emptyStyle == null)
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 0.5f ) );
			emptyStyle = new GUIStyle( GUI.skin.box );
			emptyStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0f, 0f, 0.5f ) );
		}
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
	
	void Update () {
		isRunning = Input.GetKey (KeyCode.LeftShift);
		if (isDetect || isRunning)
			StartCoroutine (Detection ());
		else
			StartCoroutine (Recover ());
		if (detect >= 75 && !soundplaying)
		{
			mySound.Play();
			soundplaying = true;
		}
		if (detect < 75 && soundplaying)
		{
			mySound.Stop();
			soundplaying = false;
		}
	}
	
	IEnumerator Detection()
	{
		while ((isDetect | isRunning )&& detect <= 100) {
			detect += 0.6f;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	IEnumerator Recover()
	{
		while (!isDetect && !isRunning && detect > 0) {
			detect -= 0.1f;
			yield return new WaitForSeconds(0.5f);
		}
		
	}
	
	void OnGUI () {
		InitStyles ();
		GUI.Box (new Rect (20, 120, 20, 300), "", emptyStyle);
		GUI.Box (new Rect (20, 420 - detect * 4, 20, 4 * detect), "", currentStyle);
		
		
	}
	

	
}*/