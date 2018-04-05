using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
	static public Hero S;

	public float speed = 30;
	public float rollMult = 45;
	public float pitchMult = 30;
	private Weapon gun;
	public float gameRestartDelay = 2f;
	public GameObject projectilePrefab;
	public float projectileSpeed = 40;

	[Header("Set Dynamically")]
	[SerializeField]

	private float _shieldLevel = 1;

	private GameObject lastTriggerGo = null;

	public delegate void WeaponFireDelegate ();

	public WeaponFireDelegate fireDelegate;


	// Use this for initialization
	void Awake () {
		gun = GetComponent<Weapon> ();
		if (S == null) {
			S = this;
		} else {
			Debug.LogError("Hero.Awake() - attempt to assign two heros" );
		}
	}


	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector3 pos = transform.position;
		pos.x += x * speed * Time.deltaTime;
		pos.y += y * speed * Time.deltaTime;
		transform.position = pos;
		transform.rotation = Quaternion.Euler (y*pitchMult,x*rollMult,0);


		if(Input.GetKey(KeyCode.Alpha1)) {
			Weapon.upgradeWeapon(WeaponType.none);
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			Weapon.upgradeWeapon(WeaponType.blaster);
		}
		if (Input.GetKey (KeyCode.Alpha3)) {
			Weapon.upgradeWeapon(WeaponType.phaser);
		}


		if (Input.GetAxis ("Jump") == 1 && fireDelegate != null) {

			fireDelegate ();

		}


	}
		


	void OnTriggerEnter(Collider other){


		Transform rooT = other.gameObject.transform.root;
		GameObject go = rooT.gameObject;

		if (go == lastTriggerGo) {
			return;
		}

		lastTriggerGo = go;

		if (go.tag == "Enemy") {
			shieldLevel--;
		} else if (go.tag == "BossEnemy") {
			shieldLevel--;
		}else if(go.tag == "PowerUp"){
			AbsorbPowerUp (go);
		}else if(go.tag == "ProjectileEnemy"){
			shieldLevel--;
			Destroy (go);
		}
	}

	//absorbing powerup
	public void AbsorbPowerUp(GameObject go){
		PowerUp pu = go.GetComponent<PowerUp>();
		//depending on type absorbed:: different effects
		switch (pu.type) {
		case "shield":
			shieldLevel++;	
			break;
		case "speed":
<<<<<<< HEAD
=======
			print ("speed");
>>>>>>> 7e192b1e10f911ba3bff0ea61f6094c81f2d6993
			if (speed <70)
			speed += 10;

			break;

		case "nuke":
			Main.S.DestroyAll ();
			break;
		}
		pu.AbsorbedBy (this.gameObject);

	}

	//Shield level of hero: if its <0--> restart game
	public float shieldLevel {
		get {
			return(_shieldLevel);
		}
		set {
			_shieldLevel = Mathf.Min (value, 4);
			if (value < 0) {
				Destroy (this.gameObject);
				Main.S.DelayedRestart (gameRestartDelay);
			}
		}
	}
}