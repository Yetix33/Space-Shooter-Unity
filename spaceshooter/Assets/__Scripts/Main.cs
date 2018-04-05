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

	public GameObject explosion;

	public GameObject[] enemies;
	public GameObject bossEnemy;
	public float enemySpawns = 1f;
	public float enemyDefaultSpacing = 1.5f;
	public WeaponDefinition[] weaponDefinition;

	private WeaponDefinition phaserdef = new WeaponDefinition();
	public GameObject prefabPowerUp;


	public Text currentScore;
	public Text highScore;

	public static int TOTAL_POINTS;
	public static int HIGH_SCORE;
	public static int CURR_LEVEL;

	void Start() {
		TOTAL_POINTS = 0;
		CURR_LEVEL = 0;
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
		phaserdef.damageOnHit = 3;
		phaserdef.delayBetweenShots = 1;
		phaserdef.velocity = 100; 

		WEAP_DICT.Add (WeaponType.phaser, phaserdef);

		foreach (WeaponDefinition def in weaponDefinition) {
			WEAP_DICT [def.type] = def;			
		}



	}

	void Update(){
		//Update Scoreboard
		UpdateScore ();
<<<<<<< HEAD
		//Enemy Spawnrates
		enemySpawns = Mathf.Sqrt (TOTAL_POINTS+1);
		//Update HighScore
=======
		enemySpawns = Mathf.Sqrt (TOTAL_POINTS+1);
>>>>>>> 7e192b1e10f911ba3bff0ea61f6094c81f2d6993
		if (TOTAL_POINTS >= HIGH_SCORE) {
			HIGH_SCORE = TOTAL_POINTS;
			PlayerPrefs.SetInt ("highscore", HIGH_SCORE);
		}

<<<<<<< HEAD
		//Move to Next level after 100 points
=======
>>>>>>> 7e192b1e10f911ba3bff0ea61f6094c81f2d6993
		if (TOTAL_POINTS >= 100 && CURR_LEVEL == 0) {
			CURR_LEVEL++;
			SceneManager.LoadScene ("_Level", LoadSceneMode.Additive);

		}
	}

	//Enemy spawn function

	public void Spawn(){

		//Randomly pick an enemey type from list of enemies
		int rng = Random.Range (0, enemies.Length);
	
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


		//if on current level keep spawning minions:: else destroy all minions and spawn boss
		if (CURR_LEVEL == 0) {
<<<<<<< HEAD
			//RECALL FUNCTION (keeps going)
=======
			print (enemySpawns);
>>>>>>> 7e192b1e10f911ba3bff0ea61f6094c81f2d6993
			Invoke ("Spawn", 5f / enemySpawns);
		} else {
			DestroyAll ();
			Invoke ("BossSpawn", 2f);
		}
	}

	//invoke restart of game with a delay
	public void DelayedRestart(float delay){
		Invoke ("Restart", delay);
	}

	public void Restart(){
		//reload same screen
		SceneManager.LoadScene("_MainScreen");
	}

	public void shipDestroyed(Enemy e){
		float chance = Random.Range (0, 100);
		if (chance <= 40) {
			GameObject go = Instantiate (prefabPowerUp);
			PowerUp pu = go.GetComponent<PowerUp> ();
			pu.transform.position = e.transform.position;
		}
		//if boss is destroyed: trigger explosion: game end screen after 4 seconds
		if (e.tag == "BossEnemy") {
			Instantiate (explosion, e.transform.position, e.transform.rotation);
			Invoke ("EndGame", 4.0f);
		}

	}
	//Load End Game Screen
	public void EndGame(){
		SceneManager.LoadScene ("_Finished");
	}

	//boss spawn function
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
	//Update score on screen
	public void UpdateScore() {
		currentScore.text = "Score: " + TOTAL_POINTS.ToString();
		highScore.text = "High Score: " + HIGH_SCORE.ToString ();
	}

	//Destroy all enemy game objects 
	public void DestroyAll(){
		GameObject[] gameObjects;
		gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");

		for (int i = 0; i < gameObjects.Length; i++) {
			Destroy (gameObjects [i]);
		}
		Instantiate (explosion,Vector3.zero,Quaternion.identity);
	}

}