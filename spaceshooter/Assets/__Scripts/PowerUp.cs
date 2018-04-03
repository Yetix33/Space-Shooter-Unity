using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public string[] PowerList = {"speed", "shield", "nuke"};
	public string type;
	public float             lifeTime = 6f;
	public float 			fadeTime = 4f;
	public float             birthTime;
	public GameObject        cube;     

	// Use this for initialization
	void Awake () {
		int rand = Random.Range (0, 3);
		print (rand);
		type = PowerList[rand]; 
		birthTime = Time.time;
		cube = gameObject;
	}

	// Update is called once per frame
	void Update () {
		float u = (Time.time - (birthTime+lifeTime)) / fadeTime;

		if (u >= 1) {
			
			Destroy( this.gameObject );            
			return;        
		} 
		if (u > 0) {
			Color c = cube.GetComponent<MeshRenderer>().material.color;

			c.a = 1f - u;   

			cube.GetComponent<MeshRenderer>().material.color = c; 

		}
	}
	public void AbsorbedBy(GameObject target){
		Destroy( this.gameObject );
	}
	void OnCollisionEnter (Collision coll) {

	}
}