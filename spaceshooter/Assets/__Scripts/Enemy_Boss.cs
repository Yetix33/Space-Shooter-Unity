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


	public override void Move(){
		Vector2 tempPos = pos;

		speed = Random.Range (1, 55);


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

	new void Update(){
		base.Update ();
		textObject.text = health.ToString ();

	}


	void Start() {
		health = 15;
		base.setScore ();
		textObject = gameObject.transform.Find("HP").gameObject.GetComponent<TextMesh> ();
		body = gameObject.transform.Find ("Body").gameObject;

		float ranNum = Random.Range (1, 3);
		Invoke ("Attack", ranNum);

	}
		
		
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
		print ("flash: run");

		Invoke ("Flashfx", 0.3f);

	}


	public void Attack(){
		print ("ATTACK RUN!");
		Flashfx ();
		int attack = Random.Range (1, 3);

		switch (attack) {
		case 1:
			{
				print ("First: 1");
				Fire ();
				break;
			}
		case 2:
			{
				print ("First: 2");
				SpawnMinions ();
				break;
			}
		}
		body.GetComponent<MeshRenderer>().material.color = Color.white;
		count = 0;
		float ranNum = Random.Range (2, 8);
		Invoke ("Attack", ranNum);

	}

	void SpawnMinions(){
		Vector3 pos = Vector3.zero;

		for (int i= 0; i<4; i++){
			pos.x = transform.position.x*i;
			pos.y = transform.position.y;
			Instantiate (minions,pos,Quaternion.identity);
		}
	}

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
