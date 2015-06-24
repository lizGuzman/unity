using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class CarControl : MonoBehaviour {

	#region Attributes
	public enum Weapon {
		Mitrailleuse = 0,
		Missile
	}
	
	[SerializeField]private GameObject[] particles;
	[SerializeField]protected Transform[] spawnPoint;
	[SerializeField]private Transform COM;

	[SerializeField]private WheelCollider WheelFL;
	[SerializeField]private WheelCollider WheelFR;
	[SerializeField]private WheelCollider WheelRL;
	[SerializeField]private WheelCollider WheelRR;

	[SerializeField]private Transform WheelFLTrans;
	[SerializeField]private Transform WheelFRTrans;
	[SerializeField]private Transform WheelRLTrans;//Rear Left Wheel
	[SerializeField]private Transform WheelRRTrans;//Rear Right Wheel

	public float currentSpeed;
	public float topSpeed = 150f;
	public float maxTorque = 50f;
	public int[] gearRatio;

	private float lowestSteerAtSpeed = 50f;
	private float lowSpeedSteerAngle = 10f;
	private float highSpeedSteerAngle = 1f;
	private float decelerationSpeed = 30f;
	private float maxReverseSpeed = 50f;
	private float maxBrakeTorque = 100f;

	protected bool[] Shoot = new bool[] {true, true};

	protected int life = 100;
	protected int missileAmmo = 8;
	private float[] waitToFire = new float[] {0.3f, 2.5f};
	#endregion

	#region MonoBehaviour
	void Start () {
		GetComponent<Rigidbody>().centerOfMass = COM.localPosition;
	}

	void FixedUpdate() {
		WheelFLTrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		WheelFRTrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		WheelRLTrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);//Rear Left Wheel transform
		WheelRRTrans.Rotate(WheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);//Rear Right Wheel

		Vector3 calc = WheelFLTrans.localEulerAngles;
		calc.y = (2*WheelFL.steerAngle) - WheelFLTrans.localEulerAngles.z;
		WheelFLTrans.localEulerAngles = calc;

		calc = WheelFRTrans.localEulerAngles;
		calc.y = (2*WheelFR.steerAngle) - WheelFRTrans.localEulerAngles.z;
		WheelFRTrans.localEulerAngles = calc;

		WheelPosition();
	}

	void LateUpdate() {
		if (life <= 0)
			Destroy (gameObject);
		if (missileAmmo <= 0)
			Shoot [(int)Weapon.Missile] = false;
	}
	#endregion

	#region Control
	protected void Control(float vertical, float horizontal, bool isBraking) {
		currentSpeed = 2 * Mathf.PI * WheelRL.radius * WheelRL.rpm * 6/100;// 60 / 1000
		currentSpeed = Mathf.Abs(Mathf.Round(currentSpeed));

		if (currentSpeed < topSpeed && currentSpeed > -maxReverseSpeed) {
			WheelRR.motorTorque = -maxTorque * vertical;
			WheelRL.motorTorque = -maxTorque * vertical;
		}
		else {
			WheelRR.motorTorque = 0;
			WheelRL.motorTorque = 0;
		}

		if (isBraking == false) {
			WheelRR.brakeTorque = decelerationSpeed;
			WheelRL.brakeTorque = decelerationSpeed;
		}
		else {
			WheelRL.brakeTorque = 0;
			WheelRR.brakeTorque = 0;
		}
		
		float speedFactor = GetComponent<Rigidbody>().velocity.magnitude / lowestSteerAtSpeed;
		float currentSpeedAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
		currentSpeedAngle *= horizontal;

		WheelFL.steerAngle = currentSpeedAngle;
		WheelFR.steerAngle = currentSpeedAngle;
	}
	
	void WheelPosition() {
		RaycastHit hit;
		Vector3 wheelPos;

		//Front Left Wheel
		if (Physics.Raycast (WheelFL.transform.position, -WheelFL.transform.forward, out hit, WheelFL.radius + WheelFL.suspensionDistance))
			wheelPos = hit.point + WheelFL.transform.forward * WheelFL.radius;
		else
			wheelPos = WheelFL.transform.position - WheelFL.transform.forward * WheelFL.suspensionDistance;
		WheelFLTrans.position = wheelPos;

		//Front Right Wheel
		if (Physics.Raycast(WheelFR.transform.position, -WheelFR.transform.forward, out hit, WheelFR.radius + WheelFR.suspensionDistance))
			wheelPos = hit.point + WheelFR.transform.forward * WheelFR.radius;
		else
			wheelPos = WheelFR.transform.position - WheelFR.transform.forward * WheelFR.suspensionDistance;
		WheelFRTrans.position = wheelPos;

		//Rear Left Wheel
		if (Physics.Raycast(WheelRL.transform.position, -WheelRL.transform.forward, out hit, WheelRL.radius + WheelRL.suspensionDistance))
			wheelPos = hit.point + WheelRL.transform.forward * WheelRL.radius;
		else
			wheelPos = WheelRL.transform.position - WheelRL.transform.forward * WheelRL.suspensionDistance;
		WheelRLTrans.position = wheelPos;

//		Rear Right Wheel
		if (Physics.Raycast(WheelRR.transform.position, -WheelRR.transform.forward, out hit, WheelRR.radius + WheelRR.suspensionDistance))
			wheelPos = hit.point + WheelRR.transform.forward * WheelRR.radius;
		else
			wheelPos = WheelRR.transform.position - WheelRR.transform.forward * WheelRR.suspensionDistance;
		WheelRRTrans.position = wheelPos;
	}
	#endregion

	#region Fire
	public void takeDamage(Weapon gun) {
		switch (gun) {
		case Weapon.Missile : life -= 50;
			break;
		case Weapon.Mitrailleuse : life -= 10;
			break;
		}
	}

	IEnumerator ParticlesEffect(int par) {
		GameObject show = Instantiate (particles [par], spawnPoint[par].position, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (1.0f);
		Destroy (show);
	}

	IEnumerator GiveOrder(int gun) {
		Shoot[gun] = false;
		yield return new WaitForSeconds(waitToFire[gun]);
		Shoot[gun] = true;
	}

	protected void Fire (Weapon gun, Ray center) {
		FireSound ();
		StartCoroutine (ParticlesEffect((int)gun));
		RaycastHit hit;
		if (Physics.Raycast (center, out hit)) {
			CarControl car = hit.transform.GetComponent<CarControl> ();
			if (car != null)
				car.takeDamage (gun);
		}
	}

	protected void Fire (Weapon gun) {
		if (Shoot[(int) gun]) {
			if (gun == Weapon.Missile)
				missileAmmo--;

			FireSound ();
			StartCoroutine (ParticlesEffect ((int)gun));
			RaycastHit hit;
			Ray center = new Ray (spawnPoint [1].transform.position, -spawnPoint [1].transform.forward);
			if (Physics.Raycast (center, out hit)) {
				CarControl car = hit.transform.GetComponent<CarControl> ();
				if (car != null)
					car.takeDamage (gun);
			}
			StartCoroutine(GiveOrder((int)gun));
		}
	}
	#endregion

	#region Sound
	protected void EngineSound() {
		int i = 0;
		for (; i < gearRatio.Length; i++) {
			if (gearRatio[i] > currentSpeed)
				break;
		}
		float gearMinValue;
		float gearMaxValue = gearRatio[i];
		if (i == 0)
			gearMinValue = 0.0f;
		else
			gearMinValue = gearRatio[i - 1];

		float enginePitch = (currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue);
		if (currentSpeed < 5 && currentSpeed > -5)
			GetComponent<AudioSource>().pitch = 0.5f;
		else
			GetComponent<AudioSource>().pitch = enginePitch + 1f;
	}

	protected void FireSound() {
		GetComponents<AudioSource> () [1].Play ();
	}
	#endregion
}