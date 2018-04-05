using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public string[] PowerList = {"speed", "shield", "nuke"};
	public string type;
	public float            lifeTime = 6f;
	public float 			fadeTime = 4f;
	public float            birthTime;
	public GameObject       cube;     
	public Vector2    		rotMinMax = new Vector2(15,90); 
	public Vector3          rotPerSecond;   
	Color c;
	// Use this for initialization
	void Awake () {
		int rand = Random.Range (0, 3);
		type = PowerList[rand]; 
		birthTime = Time.time;
		cube = gameObject;
		 
<<<<<<< HEAD
		switch (type) {
		case "speed":
			c = new Color (255f, 0f, 255f);
			break;
		case "shield":
			c = new Color (0f, 255f, 255f);
			break;
		case "nuke":
			c = new Color (255f, 0f, 0f);
			break;
=======
		switch (rand) {
		case 0:
			c = new Color (255f, 0f, 255f);
			break;
		case 1:
			c = new Color (0f, 255f, 255f);
			break;
		case 2:
			c = new Color (255f, 0f, 0f);
			break;
		default:
			c = new Color (255f, 255f, 255f);
			break;
>>>>>>> 7e192b1e10f911ba3bff0ea61f6094c81f2d6993
		}
		cube.GetComponent<MeshRenderer>().material.color = c;
	
		rotPerSecond = new Vector3( Random.Range(rotMinMax.x,rotMinMax.y), Random.Range(rotMinMax.x,rotMinMax.y),  Random.Range(rotMinMax.x,rotMinMax.y) );


	}

	// Update is called once per frame
	void Update () {
		cube.transform.rotation = Quaternion.Euler( rotPerSecond*Time.time );
		float u = (Time.time - (birthTime+lifeTime)) / fadeTime;

		if (u >= 1) {
			
			Destroy( this.gameObject );            
			return;        
		} 
		if (u > 0) {
			c = cube.GetComponent<MeshRenderer>().material.color;

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