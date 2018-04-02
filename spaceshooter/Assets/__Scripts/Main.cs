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
	public float enemySpawns = 0.5f;
	public float enemyDefaultSpacing = 1.5f;
	public WeaponDefinition[] weaponDefinition;
	private WeaponDefinition phaserdef = new WeaponDefinition();
	public Text currentScore;
	public Text highScore;
	public GameObject prefabPowerUp;
	public static int TOTAL_POINTS;
	public static int HIGH_SCORE;

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
		phaserdef.damageOnHit = 3;
		phaserdef.delayBetweenShots = 5;
		phaserdef.velocity = 40;
		WEAP_DICT.Add (WeaponType.phaser, phaserdef);
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

		Invoke ("Spawn", 1f / enemySpawns);
	}

	public void DelayedRestart(float delay){
		Invoke ("Restart", delay);
	}
		
	public void Restart(){
		SceneManager.LoadScene("_MainScreen");
	}

	public void shipDestroyed(Enemy e){
		GameObject go = Instantiate (prefabPowerUp);
		PowerUp pu = go.GetComponent<PowerUp> ();
		pu.transform.position = e.transform.position;
	
	}




	static public WeaponDefinition GetWeaponDefinition (WeaponType wt) {

		if (WEAP_DICT.ContainsKey (wt)) {
///			print("keyfound");
			return (WEAP_DICT [wt]);
			
		}
		print("keynotfound");
		return (new WeaponDefinition ());
		
	}

	public void UpdateScore() {
		currentScore.text = "Score: " + TOTAL_POINTS.ToString();
		highScore.text = "High Score: " + HIGH_SCORE.ToString ();



	}

}