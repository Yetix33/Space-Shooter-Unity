using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {
	public float radius = 1f;
	public float camWidth;
	public float camHeight;
	[HideInInspector]

	public bool	offRight, offLeft, offUp, offDown;

	//Bool values for Boundary Checks
	public bool keepOnScreen = true;
	public bool isOnScreen = true;

	void Awake(){
		camHeight = Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
	}


	//Drawn Boundaries on screen
	void OnDrawGizmos(){
		if (!Application.isPlaying)
			return;
		Vector2 bound = new Vector2 (camWidth * 2, camHeight * 2);
		Gizmos.DrawWireCube (Vector2.zero, bound);
	}




	void LateUpdate(){
		Vector3 pos = transform.position;
		isOnScreen = true;
		offRight = offLeft = offUp = offDown = false;

		//Checks all bounds...If it is offScreen sets isOnScreen to false
		if (pos.x > camWidth - radius) {
			pos.x = camWidth - radius;
			offRight = true;
		}

		if (pos.x < -camWidth + radius) {
			pos.x = -camWidth + radius;
			offLeft = true;
		}

		if (pos.y > camHeight - radius) {
			pos.y = camHeight - radius;
			offUp = true;
		}

		if (pos.y < -camHeight + radius) {
			pos.y = -camHeight + radius;
			offDown = true;
		}

		//if the object is to be kept on screen (hero) it sets transform.pos to the updated pos and sets isOnScreen to true

		isOnScreen = !(offRight || offLeft || offUp || offDown);

		if (keepOnScreen && !isOnScreen) {
			transform.position = pos;
			isOnScreen = true;
			offRight = offLeft = offUp = offDown = false;
		}
	}
}

