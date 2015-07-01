using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Club {
	Putter = 0,
	Iron,
	Wedge,
	Wood
};

public class ClubForce : MonoBehaviour {

	#region Attributes
	[SerializeField]private Font font;
	[SerializeField]private Texture2D aTexture;
	public GameObject watertext;
	private bool green = false;
	private bool water = false;
	private int myClub = (int)Club.Iron;
	private float malus = 1.0f;
	public bool plusOne = false;

//	private float bounceFactor = 0.9f; // Determines how the ball will be bouncing after landing. The value is [0..1]
	private float[] forceFactor = new float[] {3.0f, 6.0f, 8.0f, 11.0f};
	private string[] nameClub = new string[] {"Putter","Fer","Wedge","Bois"};
	private float[] bellKick = new float[] {0.5f, 8.0f, 15.0f, 0.5f};
	private Vector2[] rangeLimit = new Vector2[] {new Vector2(-0.5f, 0.5f),new Vector2(-3.0f, 3.0f), new Vector2(-5.0f, 5.0f) ,new Vector2(-5.0f, 5.0f)};
//	public float forceFactor = 10f;

	private float kickStart; // Keeps time, when you press button
	public float kickForce; // Keeps time interval between button press and release 
	private Vector3 prevVelocity; // Keeps rigidbody velocity, calculated in FixedUpdate()
	private Vector3 lastPosition;	
	public bool isMoving;

	#endregion

	#region Properties
	public float Force {
		get { return kickForce; }
		set { kickForce = value; }
	}

	public Club MyClub {
		get { return (Club)myClub ; }
		set { myClub = (int)value; }
	}

	public float Malus {
		get { return malus ; }
		set {
			malus = value;
			if (malus < 1.0f) {
				Vector3 currentVelocity = GetComponent<Rigidbody> ().velocity;
				Vector3 oppositeForce = -currentVelocity;
				GetComponent<Rigidbody> ().AddRelativeForce (oppositeForce.x, oppositeForce.y, oppositeForce.z);
			}
		}
	}

	public bool Green {
		get { return green ; }
		set { green = value;}
	}

	public bool Water {
		get { return water ; }
		set { water = value;}
	}
	#endregion
	
	void Start() {
		lastPosition = transform.position;
		isMoving = false;
	}
	
	void Update() {
		if (malus != 1.0f) {
			myClub = 2;
		} else if (green == true) {
			myClub = 0;
		} else if (myClub == 0) {
			myClub = 1;
		}
		else {
			if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
				myClub = (myClub + 1 > 3) ? 1 : myClub + 1;
				//Debug.Log (myClub);
			} else if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
				myClub = (myClub - 1 < 1) ? 3 : myClub - 1;
				//Debug.Log (myClub);
			}
		}
		if (transform.position != lastPosition)
			isMoving = true;
		else {



			if (GetComponent<Rigidbody>().velocity.x < 0.1 && GetComponent<Rigidbody>().velocity.x > -0.1)
			{
				isMoving = false;
//				GetComponent<Rigidbody>().maxAngularVelocity =
//				Vector3 currentVelocity = GetComponent<Rigidbody> ().velocity;
//				Vector3 oppositeForce = -currentVelocity;
//				GetComponent<Rigidbody> ().AddRelativeForce (oppositeForce.x, oppositeForce.y, oppositeForce.z);
			
			}

		}
		
		lastPosition = transform.position;
		ScoreGUI.club = (Club)myClub;
	}

	void OnGUI () {
//		if (GetComponent<ball>().lance == true) {
//		//Debug.Log (Club [myClub]);
//			GUI.Box (new Rect (40, 800, 200, 30), nameClub[myClub]);
//		}
//		GUI.DrawTexture (new Rect (Screen.width/2, Screen.height/2, 60, 60), aTexture);
		if (water == true || transform.position.y < -1000) {
			GUIStyle style = new GUIStyle ();
			style.font = font;
			style.fontSize = 40;
			style.normal.textColor = Color.red;
			GetComponent<ball>().score += 1;
			GUI.Label(new Rect(Screen.width/2, Screen.height/2, 200, 50), "FAIL WATER", style);
			if (!plusOne) {
				GetComponent<ball>().lastStop.y += 0.1f;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				transform.position =GetComponent<ball>().lastStop;
				plusOne = true;
			}
			print(transform.position);
		}
	}
	
	void FixedUpdate () {
		
		if(kickForce != 0)
		{
			kickForce /= 10.0f;
			float test = forceFactor[myClub] * Mathf.Clamp(kickForce, -1.0f, 10.0f) * malus;
			float angle = (Camera.main.transform.rotation.eulerAngles.y + Random.Range(rangeLimit[myClub].x,rangeLimit[myClub].y)) * Mathf.Deg2Rad;
			GetComponent<Rigidbody>().AddForce(new Vector3(test * Mathf.Sin(angle),
			                                               bellKick[myClub],
			                                               test * Mathf.Cos(angle)), 
			                                   ForceMode.VelocityChange);
			kickForce = 0;
		}
		prevVelocity = GetComponent<Rigidbody>().velocity;        
	}
}
