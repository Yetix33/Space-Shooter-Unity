using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	private float scorePoints;
	public int score = 100;


	public float speed = 10f;
	public float health = 1f;

	private BoundsCheck boundCheck;

	void Awake(){
		//get BoundsCheck 
		boundCheck = GetComponent<BoundsCheck> ();
	}



	public Vector3 pos{
		get{ 
			return(this.transform.position);
		}
		set{ 
			this.transform.position = value;
		}

	}

	public void setScore() {
		scorePoints = health;
	}

	// Update is called once per frame
	public void Update () {
		Move();

		//Destroy object if OnScreen is false from boundCheck
		if (boundCheck != null && !boundCheck.isOnScreen) {
				Destroy (gameObject);
		}
	}

	public virtual void Move(){
		//Override default Move 
	}

	void OnCollisionEnter (Collision coll) {
		GameObject otherGO = coll.gameObject;

		switch (otherGO.tag) {
		case "ProjectileHero":
			Projectile p = otherGO.GetComponent<Projectile> ();

			if (!boundCheck.isOnScreen) {
				Destroy (otherGO);
				break;
			}

			health -= Main.GetWeaponDefinition (p.type).damageOnHit;
			if (health <= 0) {
				Main.S.shipDestroyed( this ); 
				Destroy (this.gameObject);
				Main.TOTAL_POINTS = Main.TOTAL_POINTS + (int) scorePoints;
	

			}

			Destroy (otherGO);
			break;
		default:
			print ("Enemy hit by non-ProjectileHero: " + otherGO.name);
			break;
		}
	}



}
