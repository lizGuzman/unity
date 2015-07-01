using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trou : MonoBehaviour {

	public GameObject ball;
	public GameObject textScore;
	//private int labelScore;
	private int[] forceFactor = new int[] {3, 4, 3};
	private string[] nameHole = new string[] {"trou1","trou2","trou3"};
	private static int[] saveScore = new int[3];
	private static int[] saveLabel = new int[3];
	private string[] scoreTexts = new string[]{"Ace", "Albatross", "Eagle", "Birdie", "Par", "Bogey", "Double Bogey", "Triple Bogey","Noob"};

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {
			ball ballScript = ball.GetComponent<ball>();
			if (gameObject.name == nameHole[ballScript.hole]) {

				int score = forceFactor[ballScript.hole] - ballScript.hits;
				//if (ballScript.hits > forceFactor[ballScript.hole] + 3)
					//Debug.Log ("+" + -score);
				//else
					//Debug.Log (scoreTexts[4 - (forceFactor[ballScript.hole] - ballScript.hits)]);
				ballScript.score += score;


				ballScript.scoreDisplay = true;
				saveScore[ballScript.hole] = -score;

				if (ballScript.hits == 1) {
					textScore.GetComponent<Text>().text = "\nYOUR SCORE IS: "+(-score)+"\n"+scoreTexts[0];
					saveLabel[ballScript.hole] = 0;
				}
				else if (ballScript.hits > forceFactor[ballScript.hole] + 3) {
					textScore.GetComponent<Text>().text = "\nYOUR SCORE IS: "+(-score);
					saveLabel[ballScript.hole] = 8;
				}
				else {
					textScore.GetComponent<Text>().text = "\nYOUR SCORE IS: "+(-score)+"\n"+scoreTexts[4 - (forceFactor[ballScript.hole] - ballScript.hits)];
					saveLabel[ballScript.hole] = 4 - (forceFactor[ballScript.hole] - ballScript.hits);
				}
				textScore.SetActive(true);
				if (ballScript.hole == 2)
				{
					textScore.GetComponent<Text>().text += "\n\n1 HOLE: SCORE="+saveScore[0] + "  TITLE="+scoreTexts[saveLabel[0]];
					textScore.GetComponent<Text>().text += "\n\n2 HOLE: SCORE="+saveScore[1] + "  TITLE="+scoreTexts[saveLabel[1]];
					textScore.GetComponent<Text>().text += "\n\n3 HOLE: SCORE="+saveScore[2] + "  TITLE="+scoreTexts[saveLabel[2]];
				}

				ballScript.hits = 0;
				ballScript.hole += 1;
			}
			//REGARDER LE SCORE SI UNE BALLE EST DANS LE TROU
		}
	}
}
