using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_1 : Enemy {
	System.Random rand = new System.Random ();
	int ranNum;

	void Start(){
		ranNum = rand.Next(2);
		health = 2;
		base.setScore ();
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
}
