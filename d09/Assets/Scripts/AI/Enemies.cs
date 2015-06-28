using UnityEngine;
using System.Collections;

public abstract class Enemies : MonoBehaviour {

	#region Attributes
	public delegate void Death(Enemies me);
	public event Death OnDeathHandler;
	public enum Genre
	{
		Zombie,
		Army,
		Boss
	};

	protected int life = 100;
	protected Genre myGenre;

	public Transform target;
	public int damage = 10;
	public float FollowTime;
	public bool attack = false;
	#endregion

	public Genre Type {
		get { return myGenre; }
	}

	void Start() {
		SetOldTarget ();
	}

	protected void TakeDamage(int gun) {
		life -= damage;
		if (life <= 0.0f)
			OnDeathHandler (transform.GetComponent<Enemies>());
	}

	public void SetOldTarget() {
		switch (myGenre) {
		case Genre.Zombie : target = transform.GetComponent<ZombieScript>().getTarget;
			break;
		case Genre.Army : target = transform.GetComponent<ArmyScript>().getTarget;
			break;
		case Genre.Boss : target = null;
			break;
		}
	}
}