using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossAppearance : MonoBehaviour {
	public Text msg;

	// Use this for initialization
	float delay = 3f;

	void Start () {
		Invoke ("Unload", delay);
	}

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
	
	public void Unload(){
		SceneManager.UnloadSceneAsync("_Level");

	}
}
