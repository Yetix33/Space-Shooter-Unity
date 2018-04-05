using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {

	
	// If the player presses enter: reloads game at mainscreen!
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene ("_MainScreen");
		}
	}
}
