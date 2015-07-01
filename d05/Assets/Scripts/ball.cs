using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	#region Attributes
	// Use this for initialization
	public Quaternion rotation = Quaternion.identity;
	public GameObject mainCamera;
	public GameObject arrow;
	private Vector3[] ballPosition = new Vector3[]{new Vector3(400,20,70), new Vector3(400,20,70)};
	public GameObject textScore;
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	private bool dirJauge = false;
	public bool lance = true;
	public bool power = false;
	private int force2;
	public int score;
	public int hole = 0;
	public int hits = 0;
	public Vector3[] parcours;
	public Vector3[] cam;
	public Vector3[] arrowDir;
	public int[] rotateDir;
	public int orienteBall = 0;
	public Quaternion saveDir;
	public Quaternion camview;
	public bool scoreDisplay = false;

	//current progress
	public float barDisplay;	
	Vector3 position;
	Vector2 pos = new Vector2(10,50);
	Vector2 size = new Vector2(250,50);
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	private GUIStyle emptyStyle = null;
	public GameObject arrowWood;
	public GameObject arrowIron;
	public GameObject arrowWedge;
	public ClubMove clubMv;
	#endregion
	
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
	
	void Start () {
		//TODO
		//FAIRE UN TABLEAU DE COORDONNE ET ANGLE DE DEPART
		//COMPTER CHAQUE PARCOURS REUSIT 
		//transform.position = parcours [hole];
		mainCamera.transform.position = cam [hole];
		arrow.transform.position = arrowDir[hole];
		rotation.eulerAngles = new Vector3 (5, rotateDir[hole], 0);
		mainCamera.transform.rotation = rotation;
		rotation.eulerAngles = new Vector3 (31, 180 + rotateDir[hole], 3.5f);
		arrow.transform.rotation = rotation;
	}


	public Vector3 lastStop;
	private Quaternion saveR;
//	private bool first = true;
	// Update is called once per frame
	void Update () {
		//DEBUG
//		if (first == true) {
//			hole += 2;
//			scoreDisplay = true;
//			first = false;
//		}
		if (scoreDisplay == true) {
			mainCamera.transform.position = new Vector3(1200,-200,-750);
			if (Input.GetKey (KeyCode.Return)) {
				//NEXT HOLE
				if (hole == 1)
					transform.position = new Vector3(211,17,115);
				else if (hole == 2){
					//hole 2
					transform.position = new Vector3(521,12,428);
				}
				else
					Application.Quit();
				scoreDisplay = false;
				textScore.SetActive(false);
				mainCamera.transform.position = cam [hole];
				arrow.transform.position = arrowDir[hole];
				rotation.eulerAngles = new Vector3 (5, rotateDir[hole], 0);
				mainCamera.transform.rotation = rotation;
				rotation.eulerAngles = new Vector3 (31, 180 + rotateDir[hole], 3.5f);
				arrow.transform.rotation = rotation;
			}

			return;
		}
		if (power == true)
			AjustForce(0); 

		if(Input.GetKeyDown(KeyCode.Space)  && GetComponent<ClubForce>().isMoving == false)
		{
			if (lance == false)
			{
				GetComponent<ClubForce>().Water = false;
				GetComponent<ClubForce>().plusOne = false;
				position = transform.position;
				position.y += 2;

				saveR = mainCamera.transform.rotation;
				//				saveR.x = 5;
				//				mainCamera.transform.rotation = saveR;
				Vector3 save2 = saveR.eulerAngles;
				save2.x = 5;
				mainCamera.transform.rotation = Quaternion.Euler(save2);
				mainCamera.transform.position = position;
				mainCamera.transform.Translate(0,0,-10);



//				mainCamera.transform.Rotate(5,0,0);

				arrow.transform.position = position;
				saveDir = arrow.transform.rotation;
				camview = mainCamera.transform.rotation;
				arrow.transform.rotation = camview;
				arrow.transform.Translate(0,15,80);
				arrow.transform.rotation = saveDir;

				lance = true;
			}
			else if (power == true)
			{
				lance = false;
				power = false;
				lastStop = transform.position;
				//lancer ball
				hits += 1;
				//Debug.Log ("force: "+force2);
				clubMv.gameObject.SetActive(true);
				clubMv.Moveclub(force2, gameObject);

				force2 = 0;
			}
			else
			{
			force2 = 0;
			dirJauge = false;	
			power = true;
			}
		}
	}

	public void AjustForce(int adj) { 
		if (dirJauge == false)
			force2 += 5;
		else
			force2 -= 5;
		if (force2 == 100)
			dirJauge = true;
		if (force2 == 0)
			dirJauge = false;
	}

	void OnGUI () {
		InitStyles();
		//force = 25;
		arrowIron.SetActive(false);
		arrowWedge.SetActive(false);
		arrowWood.SetActive(false);
		if (lance == true) {
			arrow.SetActive(true);

			switch (GetComponent<ClubForce>().MyClub) {
			case Club.Iron: arrowIron.SetActive(true);
				break;
			case Club.Wedge : arrowWedge.SetActive(true);
				break;
			case Club.Wood :case Club.Putter: arrowWood.SetActive(true);
				break;
			}

			GUI.Box (new Rect (20, 20, 20, 400), "", emptyStyle);
			GUI.Box (new Rect (20, 420 - force2 * 4, 20, 4 * force2), "", currentStyle);
		}
		else
			arrow.SetActive(false);
	

	}
}
