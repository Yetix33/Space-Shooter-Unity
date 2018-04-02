using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public string[] PowerList = {"speed", "power", "shield"};
	public string type;
	//public GameObject        cube;      
	// Use this for initialization
	void Awake () {
		type = PowerList[Random.Range(0, 3)]; 
		//cube = transform.Find("Cube").gameObject; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AbsorbedBy(GameObject target){
		Destroy( this.gameObject );
	}
	void OnCollisionEnter (Collision coll) {
		print ("ok");

}
}