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

	public MapEntity current;
	public CameraController _cc;
	
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

	}

	public void SetPlayable(MapEntity given) {
		current = given;
	}

	
}
