using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
	static public Hero S;

	public float speed = 30;
	public float rollMult = 45;
	public float pitchMult = 30;
	public Weapon gun;
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

		if (Input.GetAxis ("Jump") == 1 && fireDelegate != null) {

			fireDelegate ();
		
		}


	}

		void TempFire() {
			GameObject projGO = Instantiate<GameObject>(projectilePrefab);
			projGO.transform.position = transform.position;
			Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
			//rigidB.velocity = Vector3.up * projectileSpeed;

		Projectile proj = projGO.GetComponent<Projectile> ();
		proj.type = WeaponType.blaster;
		float tSpeed = Main.GetWeaponDefinition (proj.type).velocity;
		rigidB.velocity = Vector3.up * tSpeed;
		}


		void OnTriggerEnter(Collider other){
			//print ("got it");


			Transform rooT = other.gameObject.transform.root;
			GameObject go = rooT.gameObject;

			print("Object: Name: " + go.name);
			if (go == lastTriggerGo) {
				return;
			}

			lastTriggerGo = go;

			if (go.tag == "Enemy") {
				shieldLevel--;
				Destroy (go);
		}else if(go.tag == "PowerUp"){
			print("ayyyy");
			AbsorbPowerUp (go);
		}else{
				print("Triggered by non-Enemy: "+ go.name);

			//print ("Trigger: " + other.gameObject.name);
			//shieldLevel--;
			//if (shieldLevel == 0) {
			//	Destroy (other.gameObject);
			//}

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
			speed += 5;
			break;
		case "power":
			print ("power");
			Weapon.upgradeWeapon(WeaponType.phaser);
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