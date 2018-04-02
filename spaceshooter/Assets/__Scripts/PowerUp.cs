using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public string[] PowerList = {"speed", "power", "shield"};
	public string type;
	public float             lifeTime = 6f;
	public float 			fadeTime = 4f;
	public float             birthTime;
	public GameObject        cube;     

	// Use this for initialization
	void Awake () {
		type = PowerList[Random.Range(0, 3)]; 
		//cube = gameObject;
		birthTime = Time.time;
		cube = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float u = (Time.time - (birthTime+lifeTime)) / fadeTime;
		print ("what is" + u);
		if (u >= 1) {
			print ("time to die: " + u);
			Destroy( this.gameObject );            
			return;        
		} 
		if (u > 0) {
			print ("bitch FAADE: " + u);
			Color c = cube.GetComponent<MeshRenderer>().material.color;
			print (c);
			c.a = 1f - u;   
			print ("color change");
			cube.GetComponent<MeshRenderer>().material.color = c; 

		}
	}
	public void AbsorbedBy(GameObject target){
		Destroy( this.gameObject );
	}
	void OnCollisionEnter (Collision coll) {
		print ("ok");

}
}