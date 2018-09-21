using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

	/* PLAYER INPUT MANAGER
	 *
	 * Captures player input and sends commands to the proper
	 * place.
	 */


	public GameObject _player;
	public bool isMoving;
	public bool isPlayerTurn = true;

	public bool touchControls; //If false, mouse is main control

	public MapEntity current;
	public CameraController _cc;

	private const float mouseStepTolerance = 0.78f; //Used to determine how far from the player's icon to click to trigger a movement
	private const float mouseDiagonalTolerance = 3.7f; //Used to determine how loosely a diagonal angle will be be interpreted as a diagonal move

	public GameObject _scene;
	public EffectManager _effects;


	public Vector3 mousePos;
	public Vector3Int lastMousePos = Vector3Int.zero; //Used to minimize number of times we update effect manager


	void Awake () {
		GetSceneController();

		//Make sure the camera is doing what it ought to.
		SetPlayable(current);
	}

	void Update () {

		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2Int moveDir = new Vector2Int(0, 0);

		/*
		 * UNIVERSAL CONTROLS
		 *
		 */

		if (current != null) {

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

			if (Input.GetKeyDown("g") || Input.GetKeyDown("l")) {
				current.Loot(current.location);
			}


			/*
			 * TOUCH CONTROLS
			 *
			 */

			if (touchControls) {
				if (Input.GetMouseButtonDown(0)) {
					Vector2 point = Vector2Int.RoundToInt(new Vector2(mousePos.x, mousePos.y));

					current.AddWaypoint(point);
				}

			/*
			 * PC CONTROLS
			 *
			 */

			} else {

				if (mousePos != lastMousePos) {
					if (Input.GetButton("Step")) {
						//Limit to just one
						_effects.MouseHighlight(current.transform.position + GetOneStep(GetMouseXY()));
					} else {
						_effects.MouseHighlight(Vector3Int.RoundToInt(mousePos));
					}
				}

				if (Input.GetMouseButtonDown(0)) {
					current.Move(GetMouseXY());
				}

			}
		}

	}



	public void SetPlayable(MapEntity given) {
		current = given;
		CameraController.SetTarget(current.transform.gameObject);
	}


	Vector3Int GetOneStep(Vector2 input) {
		Vector2 step = GetMouseXY();

		return new Vector3Int((int)step.x, (int)step.y, -10);
	}


	Vector2 GetMouseXY() {

		Vector3 pos = mousePos - current.transform.position; //Getting the difference

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



	private void GetSceneController() {
		_scene = GameObject.Find("SceneController");

		if (_effects == null) {
			_effects = _scene.GetComponent<EffectManager>();
		}
	}



}
