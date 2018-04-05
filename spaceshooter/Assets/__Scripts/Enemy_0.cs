using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy {

	public int number;

	//moves straight down
	public override void Move(){
		Vector2 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}


	void Start() {
		number = Random.Range (0, 2);
		health = 4;
		base.setScore ();
	}

}
