using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss : Enemy {

	private bool left = true;
	private TextMesh textObject;
	private GameObject body;
	private static int count;
	public GameObject minions;

	public GameObject launchPrefab;
	public float launchSpeed = 50;


	//random movements
	public override void Move(){
		Vector2 tempPos = pos;

		//random speed
		speed = Random.Range (1, 55);

		//select left or right direction depending on its position
		if (tempPos.x < -10) {
			left = false;
		} else if (tempPos.x >= 28) {
			left = true;
		}

		if (left) {
			tempPos.x -= speed * Time.deltaTime;

		} else {
			tempPos.x += speed * Time.deltaTime;
		}

		pos = tempPos;


	}
	//updates the hp on his chest
	new void Update(){
		base.Update ();
		textObject.text = health.ToString ();

	}

	//starts with default values and initializes components
	void Start() {
		health = 15;
		base.setScore ();
		textObject = gameObject.transform.Find("HP").gameObject.GetComponent<TextMesh> ();
		body = gameObject.transform.Find ("Body").gameObject;

		//calls attack with a slight delay
		float ranNum = Random.Range (1, 3);
		Invoke ("Attack", ranNum);

	}
		
	//flash animation: turns the colour blue and calls red (increases count so it only runs this 4 times);
	public void Flashfx(){
		if (count < 4) {
			body.GetComponent<MeshRenderer> ().material.color = Color.blue;
			count++;
			Invoke ("Red", 0.3f);
		} 
		return;
	}

	public void Red(){
		body.GetComponent<MeshRenderer>().material.color = Color.red;

		Invoke ("Flashfx", 0.3f);

	}


	public void Attack(){
		//calls flash
		Flashfx ();
		//picks a random attack
		int attack = Random.Range (1, 3);

		switch (attack) {
		case 1:
			{
				//launches a projectile
				Fire ();
				break;
			}
		case 2:
			{
				//spawns minions
				SpawnMinions ();
				break;
			}
		}
		//rests colour and count
		body.GetComponent<MeshRenderer>().material.color = Color.white;
		count = 0;
		//randomly recalls attack 
		float ranNum = Random.Range (2, 8);
		Invoke ("Attack", ranNum);

	}

	//spawn minions
	void SpawnMinions(){
		Vector3 pos = Vector3.zero;

		for (int i= 0; i<4; i++){
			pos.x = transform.position.x*i;
			pos.y = transform.position.y;
			Instantiate (minions,pos,Quaternion.identity);
		}
	}

	//launch projectile 
	void Fire ()
	{
		for (int i = 0; i < 4; i++) {
			GameObject projGO = Instantiate<GameObject> (launchPrefab);
			projGO.transform.position = transform.position * i;
			Rigidbody rigidB = projGO.GetComponent<Rigidbody> ();
			rigidB.velocity = Vector3.down * launchSpeed;
		}

	}
}
