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
	//Deny access (make it into a property)
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
		//fireDelegate += TempFire;
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


		//lets da ship fire poof poof

		/*if (Input.GetKeyDown (KeyCode.Space)) {
		
			gun.Fire();

		}*/
		if(Input.GetKey(KeyCode.Alpha1)) {
			Weapon.upgradeWeapon(WeaponType.none);
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			//print("two bitch");
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
			//Destroy (go);
		} else if (go.tag == "BossEnemy") {
			shieldLevel--;
		}else if(go.tag == "PowerUp"){
			AbsorbPowerUp (go);
		}else if(go.tag == "ProjectileEnemy"){
			shieldLevel--;
			Destroy (go);
		}
	}

	public void AbsorbPowerUp(GameObject go){
		PowerUp pu = go.GetComponent<PowerUp>();
		print (pu.type);
		switch (pu.type) {
		case "shield":
			print ("shield");
			shieldLevel++;	
			break;
		case "speed":
			print ("speed");
			speed += 12;
			break;

		case "nuke":
			print ("nuke");
			Main.S.DestroyAll ();
			break;
		}
		pu.AbsorbedBy (this.gameObject);

	}


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