using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy {

	public int number;

	// Use this for initialization
	// Update is called once per frame
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
