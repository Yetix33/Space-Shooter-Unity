using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum WeaponType {
	none,        //default
	simple,     //blaster
	blaster,
	phaser,
	missile,
	laser,
	shield       //used to raise shield level
}
	

public class Main : MonoBehaviour {
	//Singleton for Main Function
	static public Main S;
	static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;



	public GameObject[] enemies;
	public GameObject bossEnemy;
	public float enemySpawns = 0.5f;
	public float enemyDefaultSpacing = 1.5f;
	public WeaponDefinition[] weaponDefinition;

	public Text currentScore;
	public Text highScore;

	public static int TOTAL_POINTS;
	public static int HIGH_SCORE;
	public static int CURR_LEVEL;

	void Start() {
		TOTAL_POINTS = 0;
		HIGH_SCORE = PlayerPrefs.GetInt ("highscore", HIGH_SCORE);
		UpdateScore ();
	}

	private BoundsCheck boundCheck;

	void Awake(){
		S = this;
		boundCheck = GetComponent<BoundsCheck> ();

		//Call Spawn function (in 2 seconds)
		Invoke ("Spawn", 1f / enemySpawns);

		WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition> ();
		foreach (WeaponDefinition def in weaponDefinition) {
			WEAP_DICT [def.type] = def;			
		}
			
	}
		
	void Update(){
		UpdateScore ();

		if (TOTAL_POINTS >= HIGH_SCORE) {
			HIGH_SCORE = TOTAL_POINTS;
			PlayerPrefs.SetInt ("highscore", HIGH_SCORE);
		}

		if (TOTAL_POINTS >= 5 && CURR_LEVEL == 0) {
			CURR_LEVEL++;
			SceneManager.LoadScene ("_Level", LoadSceneMode.Additive);

		}
	}

	public void Spawn(){

		//Randomly pick an enemey type from list of enemies
		int rng = Random.Range (0, enemies.Length);
		print (rng);
		//instantiate it 
		GameObject enemy = Instantiate<GameObject> (enemies [rng]);

		float enemySpacing = enemyDefaultSpacing;
		//Random X point:
		if (enemy.GetComponent<BoundsCheck> () != null) {
			enemySpacing = Mathf.Abs (enemy.GetComponent<BoundsCheck> ().radius);
		}

		Vector3 pos = Vector3.zero;

		float xMin = -boundCheck.camWidth + enemySpacing;
		float xMax = boundCheck.camWidth - enemySpacing;

		pos.x = Random.Range (xMin, xMax);
		pos.y = boundCheck.camHeight + enemySpacing;
		enemy.transform.position = pos;

		//RECALL FUNCTION (keeps going)

		if (CURR_LEVEL == 0) {
			Invoke ("Spawn", 1f / enemySpawns);
		} else {
			print ("lol");
			DestroyAll ();
			Invoke ("BossSpawn", 2f);
		}
	}

	public void DelayedRestart(float delay){
		Invoke ("Restart", delay);
	}
		
	public void Restart(){
		SceneManager.LoadScene("_MainScreen");
	}

	public void BossSpawn(){
		Vector3 pos = Vector3.zero;

		float x = 19.6f;
		float y = 27f;

		pos.x = x;
		pos.y = y;
		Instantiate (bossEnemy,pos,Quaternion.identity);
	}

	static public WeaponDefinition GetWeaponDefinition (WeaponType wt) {

		if (WEAP_DICT.ContainsKey (wt)) {

			return (WEAP_DICT [wt]);
			
		}

		return (new WeaponDefinition ());
		
	}

	public void UpdateScore() {
		currentScore.text = "Score: " + TOTAL_POINTS.ToString();
		highScore.text = "High Score: " + HIGH_SCORE.ToString ();
	}

	public void DestroyAll(){
		GameObject[] gameObjects;
		gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");

		for (int i = 0; i < gameObjects.Length; i++) {
			Destroy (gameObjects [i]);
		}

	}

}