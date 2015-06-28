using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class runMaya : MonoBehaviour {
	private Transform myTransform;				// this transform
	private Vector3 destinationPosition;		// The destination Point
	private float destinationDistance;			// The distance between myTransform and destinationPosition
	
	public float moveSpeed;						// The Speed the character will move
	private CharacterController characterController;

	public GameObject menuAble;
	private NavMeshAgent agent;
	public bool menu = false;
	//STAT VARIABLE
	public int HP = 50;
	public int armor = 10;
	public int strengh = 10;
	public int agility = 10;
	public int constitution = 10;
	public int minDamage = 5;
	public int maxDamage = 9;
	public int level = 1;
	public int exp = 0;
	public int[] nextLevel = new int[] {40, 250,380, 550};
	public int money = 0;
	public int skillPoint = 0;
	public string nameHero = "MAYA";
	private Text[] panelText;
	public bool restart = false;
	bool go = false;


	//GUI VARIABLE
	public Texture2D emptyTex;
	public Texture2D fullTex;
	private GUIStyle currentStyle = null;
	private GUIStyle emptyStyle = null;
	
	//////////
	/////// STAT
	////////



		public GameObject target;
		private static Texture2D mWhiteTex;
		private static GUIStyle  mStyle;
		
		
		//----------------------------------------
		// A appeller dans un callback unity OnGUI()
		//----------------------------------------
		public static void DrawGUI(int x, int y, int count, int total, string prefix, int width)
		{
			if (mWhiteTex==null) mWhiteTex = Resources.Load("Textures/white") as Texture2D; //Texture 8*8 (ou autre) toute blanche à placer dans Resources/Textures
			if (mStyle==null)
			{
				mStyle = new GUIStyle();
				mStyle.alignment = TextAnchor.MiddleCenter;
				mStyle.font = Resources.Load("Font/FontProgressBar") as Font; //Font Arial size 13 à placer dans Resources/Font
				mStyle.fontStyle = FontStyle.Bold;
			}
			GUI.color = Color.black;
			GUI.DrawTexture(new Rect(x, y, width+2, 18), mWhiteTex);
			GUI.color = new Color(0.8f,0,0);
			GUI.DrawTexture(new Rect(x+1,y+1,width,16), mWhiteTex);
			GUI.color = Color.white;
			float val = 1;
			if (total!=0) val = ((count)/(float)total);
			GUI.DrawTexture(new Rect(x+1, y+1, val*width, 16), mWhiteTex);
			GUI.color = Color.black;
			GUI.Label(new Rect(x, y, width+2, 18), prefix+" "+((int)(val*100))+"% ("+count+"/"+total+")", mStyle);
			GUI.color = Color.white;
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


	//AMELIORER LE VISUEL
	void OnGUI () {
		InitStyles();

		DrawGUI (460, Screen.height - 60,exp, nextLevel[level - 1], "exp", 40);
		DrawGUI (4 * constitution * 5 + 60, Screen.height - 30, (int)HP, 5 * constitution, "health", 40);
	
		//LVL
		GUI.TextField (new Rect(50, Screen.height - 110, 60, 40), "LEVEL " +level.ToString(),mStyle);
		//HEAL
		GUI.Box (new Rect (20, Screen.height - 30, 4 * 5 * constitution, 20), "", emptyStyle);
		GUI.Box (new Rect (20, Screen.height - 30, 4 * (int)HP, 20), "", currentStyle);
		//GUI.TextField (new Rect(4 * HP + 30, Screen.height - 30, 40, 20), HP.ToString());

		//EXP
		GUI.Box (new Rect (20, Screen.height - 60, 400, 20), "", emptyStyle);
		GUI.Box (new Rect (20, Screen.height - 60, (100 * exp) / (nextLevel[level - 1]) * 4, 20), "", currentStyle);
		//GUI.TextField (new Rect(430, Screen.height - 60, 40, 20), ((100 * exp) / (nextLevel[level - 1])).ToString()+" %");
	}
	
	/// <summary>
	/// //////////////RUN
	/// </summary>
	
	
	
	void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}
	
	void Start () {
		myTransform = transform;							// sets myTransform to this GameObject.transform
		myTransform.position.Set(transform.position.x, 0, transform.position.z);
		destinationPosition = myTransform.position;			// prevents myTransform reset
		agent = transform.GetComponent<NavMeshAgent>();
		panelText =  menuAble.GetComponentsInChildren<Text> ();
		panelText [0].text = nameHero;
		menuAble.SetActive(false);
	}
	
	void checkSolposition()
	{
		
	}

	void LateUpdate() {

	}

	IEnumerator dead()
	{

		GetComponent<Animator>().SetBool("deadMaya",true);
		yield return new WaitForSeconds(2);
		//Destroy (gameObject);
		// MESSAGE DE GAME OVER /////////
	}

	public void updateAgiSkill() {
		if (skillPoint > 0){
			agility += 1;
			skillPoint -= 1;
		}
	}

	public void updateConSkill() {
		if (skillPoint > 0) {
			constitution += 1;
			skillPoint -= 1;
		}
	}

	public void updateStrSkill() {
		if (skillPoint > 0) {
			strengh += 1;
			skillPoint -= 1;
		}
	}


	void Update () {
		minDamage = strengh / 2;
		maxDamage = minDamage + 4;

		//Text[] toto =  menuAble.GetComponentsInChildren<Text> ();
		if (menu) {
			panelText [2].text = minDamage.ToString () + "/" + maxDamage.ToString ();
			panelText [4].text = level.ToString ();
			panelText [6].text = exp.ToString ();
			panelText [8].text = nextLevel [level - 1].ToString ();
			panelText [15].text = agility.ToString ();
			panelText [16].text = strengh.ToString ();
			panelText [17].text = constitution.ToString ();
			panelText [19].text = armor.ToString ();
			panelText [23].text = skillPoint.ToString (); //A VOIR
			panelText [20].text = money.ToString ();
			Button[] buttonMenu = menuAble.GetComponentsInChildren<Button> ();
			if (skillPoint <= 0) {

				buttonMenu [0].interactable = false;
				buttonMenu [1].interactable = false;
				buttonMenu [2].interactable = false;

			} else {
				buttonMenu [0].interactable = true;
				buttonMenu [1].interactable = true;
				buttonMenu [2].interactable = true;

			}
		}


		if (Input.GetKeyDown (KeyCode.Tab) && menu == true) {
			menu = false;
			menuAble.SetActive(false);
			agent.enabled = true;
		}
		else if (Input.GetKeyDown (KeyCode.Tab) && menu == false) {
			menu = true;
			menuAble.SetActive(true);
			agent.enabled = false;
		}



		if (HP <= 0) {
			HP = 0;
			Debug.Log("fail game");
			//target.GetComponent<attack>().killEn = true;
			restart = true;
			StartCoroutine(dead());  //DEBUG

		}

		if (exp >= nextLevel [level - 1]) {
			exp = exp - nextLevel [level - 1];
			level += 1;
			skillPoint += 5;
			HP = 5 * constitution;
			Debug.Log ("level up");
			////MENU DE LEVEL UP 
		}
//		if (GetComponent<Animator>().GetBool("attackMaya") == true)
//			GetComponent<Animator>().SetBool("runMaya",false);
//		if (!characterController.isGrounded)
//		{
//			characterController.SimpleMove(gameObject.transform.up * Time.deltaTime);
//		}
		////

		//destinationPosition.Set(destinationPosition.x, transform.position.y, destinationPosition.z);

		// keep track of the distance between this gameObject and destinationPosition
		destinationDistance = agent.remainingDistance;//Vector3.Distance(destinationPosition, myTransform.position);
//		Debug.Log (destinationDistance + " " + myTransform.position + " " +destinationPosition);
		//Debug.Log (destinationDistance);
		if (destinationDistance < 2) {		// To prevent shakin behavior when near destination
			moveSpeed = 0;
			go = false;
			GetComponent<Animator> ().SetBool ("runMaya", false);
			agent.SetDestination (transform.position);
		}
		else if (destinationDistance > 2) {			// To Reset Speed to default
			moveSpeed = 3;
			GetComponent<Animator> ().SetBool ("runMaya", true);
		}
		
		
		// Moves the Player if the Left Mouse Button was clicked
		if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0) {
			go = true;
			//Debug.Log("test");
			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			//GetComponent<Animator> ().SetBool ("runMaya", true);
			if (playerPlane.Raycast(ray, out hitdist)) {
				Vector3 targetPoint = ray.GetPoint(hitdist);
//				Debug.Log (targetPoint);
				destinationPosition = ray.GetPoint(hitdist);
				//Quaternion targetRotation = Quaternion.LookRotation(targetPoint - myTransform.position);
				//myTransform.rotation = targetRotation;
			}
		}
		if (go)
			agent.SetDestination (destinationPosition);
		// To prevent code from running if not needed
		//		if(destinationDistance > .5f){
		//			transform.position = Vector3.MoveTowards(transform.position, destinationPosition, moveSpeed * Time.deltaTime);
		//		}
	}
}
