using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossAppearance : MonoBehaviour {
	public Text msg;

	//delay
	float delay = 3f;

	//3 seconds invoke 
	void Start () {
		Invoke ("Unload", delay);
	}

	//flash 
	void Update(){
		Flashfx();
	}

	public void Flashfx(){
		msg.color = Color.red;
		Invoke ("Red", 0.3f);
	}

	public void Red(){
		msg.color = Color.blue;
		Invoke ("Flashfx", 0.9f);
	}

	//unload scene 
	public void Unload(){
		SceneManager.UnloadSceneAsync("_Level");

	}
}
