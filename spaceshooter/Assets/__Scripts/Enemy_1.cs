using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_1 : Enemy {

	static public Transform PROJECTILE_ANCHOR;

	System.Random rand = new System.Random ();

	int ranNum;

	public GameObject enemyProjectilePrefab;
	public float launchSpeed = 50;

	void Start(){
		ranNum = rand.Next(2);

		health = 2;
		base.setScore ();

		Invoke ("Attack", 1.0f);


	}


	public override void Move(){
		Vector2 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		if (ranNum == 1)
			tempPos.x += speed/2 * Time.deltaTime;
		else
			tempPos.x -= speed /2* Time.deltaTime;

		pos = tempPos;
	}


	public void Attack(){

		Fire ();
		Invoke ("Attack", 3.0f);

	}


	void Fire ()
	{	

		Projectile p;

		Vector3 vel = Vector3.down * launchSpeed;

		int fire = Random.Range (1, 3);
		switch (fire) {

		case 1:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			break;

		case 2:
			p = MakeProjectile ();
			p.rigid.velocity = vel;
			p = MakeProjectile ();
			p.transform.rotation = Quaternion.AngleAxis (10, Vector3.back);
			p.rigid.velocity = p.transform.rotation * vel;
			p = MakeProjectile ();
			p.transform.rotation = Quaternion.AngleAxis (-10, Vector3.back);
			p.rigid.velocity = p.transform.rotation * vel;
			break;
		}
			
	}

	public Projectile MakeProjectile() {
		GameObject projGO = Instantiate<GameObject> (enemyProjectilePrefab);
		projGO.transform.position = transform.position;
		Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
		projGO.transform.SetParent (PROJECTILE_ANCHOR, true);
		Projectile p = projGO.GetComponent<Projectile> ();
		return(p);
	}

}
