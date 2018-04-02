using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss : Enemy {

	private bool left = true;
	private TextMesh textObject;
	private GameObject body;
	private static int count;

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

	void Update(){
		base.Update ();
		textObject.text = health.ToString ();
	}


	void Start() {
		health = 10;
		base.setScore ();
		textObject = GameObject.Find ("HP").GetComponent<TextMesh> ();
		body= GameObject.Find ("Body");

		float ranNum = Random.Range (2, 7);
		Invoke ("Attack", ranNum);

	}
		
		
	public void Flashfx(){
		if (count < 4) {
			body.GetComponent<MeshRenderer> ().material.color = Color.blue;
			print ("flash: run");
			count++;
			Invoke ("Red", 0.2f);
		} 
		return;
	}

	public void Red(){
		body.GetComponent<MeshRenderer>().material.color = Color.red;
		print ("flash: run");

		Invoke ("Flashfx", 0.2f);

	}


	public void Attack(){
		print ("ATTACK RUN!");
		Flashfx ();
		//yield return WaitForSeconds(3f);
		int attack = Random.Range (0, 4);

		switch (attack) {
		case 1:
			{
				print ("First: 1");



				break;
			}
		case 2:
			{
				print ("First: 2");
				break;
			}
		case 3:
			{
				print ("First: 3");
				break;
			}
		}
		body.GetComponent<MeshRenderer>().material.color = Color.white;
		count = 0;
		float ranNum = Random.Range (2, 7);
		Invoke ("Attack", ranNum);

	}
}
