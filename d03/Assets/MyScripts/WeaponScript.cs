using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour {

	#region Attributes
	[SerializeField]private GameObject me;
	[SerializeField]private Text text;

	private towerScript tower;
	private Image img;
	#endregion

	void Awake() {
		tower = me.GetComponent<towerScript>();
		text.text = tower.energy.ToString () + "\n" + tower.damage.ToString () + "\n" + tower.fireRate.ToString () + "\n" + tower.range.ToString ();
		img = transform.GetComponent<Image> ();
	}

	void Update() {
		if (gameManager.instance.playerEnergy < tower.energy) {
			if (img.color == Color.white)
				img.color = Color.red;
		} else {
			if (img.color != Color.white)
				img.color = Color.white;
		}
	}
}