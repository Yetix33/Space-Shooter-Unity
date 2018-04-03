﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//uses enump of diff weapon types in the game, not all are coded yet


[System.Serializable]
public class WeaponDefinition {
	public WeaponType 	type = WeaponType.none;
	public string 		letter;
	public Color 		color = Color.white;
	public GameObject 	projectilePrefab;
	public Color 		projectileColor = Color.white;
	public float 		damageOnHit = 1;
	public float 		continuousDamage = 0;
	public float 		delayBetweenShots = 0;
	public float 		velocity = 20;

}

public class Weapon : MonoBehaviour {

	static public Transform PROJECTILE_ANCHOR;

	[Header("Set Dynamically")] [SerializeField]

	private static WeaponType 			_type = WeaponType.none;
	private static WeaponDefinition 	def;
	private GameObject			collar;
	public float 				lastShotTime;
	private Renderer 			collarRend;


	// Use this for initialization
	void Start () {
		def = Main.GetWeaponDefinition (_type);
		collar = transform.Find("Collar").gameObject;
		collarRend = collar.GetComponent<Renderer> ();
		lastShotTime = 0;
		SetType (_type);

		if (PROJECTILE_ANCHOR == null) {

			GameObject go = new GameObject ("_ProjectileAnchor");
			PROJECTILE_ANCHOR = go.transform;
		
		}
		GameObject rootGO = transform.root.gameObject;
		if (rootGO.GetComponent<Hero> () != null) {

			rootGO.GetComponent<Hero> ().fireDelegate += Fire;
		
		}

		
	}

	public WeaponType type {
		get { 
			return(_type); 
		} set {
			SetType (value);
		}
	}

	public void SetType(WeaponType wt) {
		_type = wt;
		if (type == WeaponType.none) {
			this.gameObject.SetActive (false);
			return;
		} else {
			this.gameObject.SetActive (true);
		}

		def = Main.GetWeaponDefinition (_type);
		collarRend.material.color = def.color;
		lastShotTime = 0;
	}

	public void Fire() {
		//if (!gameObject.activeInHierarchy) return;

		if (Time.time - lastShotTime < def.delayBetweenShots) {
			
			return;
		}

		//lastShotTime = Time.time;
		Projectile p;

		Vector3 vel = Vector3.up * def.velocity;

		if (transform.up.y < 0) {
			vel.y = -vel.y;
		}

		switch (type) {
		case WeaponType.none:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			break;
		case WeaponType.blaster:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			p = MakeProjectile ();
			p.transform.rotation = Quaternion.AngleAxis (10, Vector3.back);
			p.rigid.velocity = p.transform.rotation * vel;
			p = MakeProjectile ();
			p.transform.rotation = Quaternion.AngleAxis (-10, Vector3.back);
			p.rigid.velocity = p.transform.rotation * vel;
			break;
		case WeaponType.phaser:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			break;
		}

	}

	public Projectile MakeProjectile() {
		GameObject go = Instantiate<GameObject> (def.projectilePrefab);
		if (transform.parent.gameObject.tag == "Hero") {
			go.tag = "ProjectileHero";
			go.layer = LayerMask.NameToLayer ("ProjectileHero");
			//enemies can get weapons (later addition)de
		} else {
			go.tag = "ProjectileEnemy";
			go.layer = LayerMask.NameToLayer ("ProjectileEnemy");
		}

		go.transform.position = transform.position;
		Rigidbody rigidB = go.GetComponent<Rigidbody>();
		go.transform.SetParent (PROJECTILE_ANCHOR, true);
		Projectile p = go.GetComponent<Projectile> ();
		p.type = type;
		lastShotTime = Time.time;
		return(p);

	}

	public static void upgradeWeapon(WeaponType t){
		_type = t;

		def = Main.GetWeaponDefinition (_type);
//		collarRend.material.color = def.color;
	}

	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKey(KeyCode.Alpha1)) {
			type = WeaponType.simple;
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			type = WeaponType.blaster;
		}*/


	}
}
