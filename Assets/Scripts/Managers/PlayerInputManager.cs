using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
	
	/* PLAYER INPUT MANAGER
	 * 
	 * Captures player input and sends commands to the proper
	 * place.
	 * 
	 * 
	 */
	
	
	public GameObject _player;
	public bool isMoving;
	public bool isPlayerTurn = true;
	
	public bool touchControls; //If false, mouse is main control

	public MapEntity current;
	public CameraController _cc;
	
	public float mouseStepTolerance = 0.8f; //Used to determine how far from the player's icon to click to trigger a movement
	public float mouseDiagonalTolerance = 1.5f; //Used to determine how loosely a diagonal angle will be be interpreted as a diagonal move
	
	void Update () {

		Vector2Int moveDir = new Vector2Int(0, 0);

		if (Input.GetAxis("Horizontal") != 0) {
			moveDir.x = (int)Input.GetAxis("Horizontal");
		}

		if (Input.GetAxis("Vertical") != 0) {
			moveDir.y = (int)Input.GetAxis("Vertical");
		}
		
		if (moveDir.x != 0 || moveDir.y != 0) {
			current.Move(moveDir.x, moveDir.y);
		}

		if (Input.GetKeyDown("space")) {
			current.Pause();
		}

		if (Input.GetKeyDown("tab")) {
			_cc.CenterOn(current.gameObject);
		}

		if (Input.GetKeyDown("g") || Input.GetKeyDown("l")) {
			current.Loot(current.location);
		}
		
		if (Input.GetMouseButtonDown(0)) {
			current.Move(MouseMove());
		}

	}

	public void SetPlayable(MapEntity given) {
		current = given;
	}
	
	
	
	Vector2 MouseMove() {
		
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - current.transform.position;
		
		Debug.Log(pos.x + " + " + pos.y + " = " + (pos.x+pos.y) + " which is greater than tolerance: " + ((pos.x + pos.y) < mouseStepTolerance));
		
		if ((Mathf.Abs(pos.x) + Mathf.Abs(pos.y)) < mouseStepTolerance) {
			
			//Checking if the position is too close to allow movement
			return Vector2.zero;
		}
		
		//Set move x and y based on relation to zero
		int moveX = (pos.x > 0) ? 1 : -1;
		int moveY = (pos.y > 0) ? 1 : -1;
			
		//Do checks to see if user is aiming in 4 cardinal directions
		//If so, limit movement to just one axis
		if (Mathf.Abs(pos.x) > (Mathf.Abs(pos.y) * mouseDiagonalTolerance) ) {
			moveY = 0;
		}
		
		if (Mathf.Abs(pos.y) > (Mathf.Abs(pos.x) * mouseDiagonalTolerance) ) {
			moveX = 0;
		}
		
		return new Vector2(moveX, moveY);

	}

	
}
