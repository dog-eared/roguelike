using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour, IAlignable {

	/* MAP ENTITY
     * Class for any entity who appears on the game board who the player
     * could reasonably interact with, or who should adhere to board rules.
     *
	 * This class holds the actual methods to move pieces on the board and
	 * check for collisions. It also contains the entity's faction -- this is
	 * so that we can iterate through the list of active pieces in proper order
     */

	[Header("Config:")]
	public string entityName = "Untitled Entity"; //Name that the user sees in GUI/ingame, rather than using the
												  //GameObject name
	public Faction faction; 	//Used to determine if/when they act in the turn order, general allegience
	public const float moveSpeed = 18f; //Speed of movements on board

	public MapAI _ai;
	public CombatData _cd;

	[Header("Turn:")]
	//public bool followingPath = false;
	public bool hasActed = false;
	public Vector2Int location; //In general we'll use this for step/distance calculations rather than transform.position
								//It'll be a little more reliable once we have characters moving around in tandem


	static private int itemLayer = 1 << 8; //We use this to ignore the lootable item layer when checking for collisions
	private	IEnumerator nextMove;


	/*
	 *	PUBLIC METHODS
	 */

	public Vector2Int GetLocation() {
		return location;
	}

	public bool HasAnimation() {
		return (nextMove != null);
	}

	public void AddWaypoint(Vector2 target) {
		if (_ai != null && _ai is PlayerPathfinding) {
			_ai.AddWaypoint(target);
			//followingPath = true;
		}
	}

	public bool GetPlayable() {
		if (hasActed == false) {
			return faction == Faction.Player;
		}

		return false;
	}

	public void Loot(Vector2Int getLocation, bool restrainDistance = true) {
		if (restrainDistance) {
			Vector2Int comparison = location - getLocation;

			if (Mathf.Abs(comparison.x) > 1 || Mathf.Abs(comparison.y) > 1) {
				Debug.Log("LOOT FAILED: Out of range.");
			}
			else {
				try {
					GameObject pickedUp = Physics2D.Raycast(getLocation, Vector3.forward, Mathf.Infinity, itemLayer)
						.transform.gameObject;
					pickedUp.GetComponent<MapItem>().Loot(this.gameObject);
				}
				catch {
					Debug.Log("LOOT FAILED: No item found.");
				}
			}
		}
	}

	public bool Move(int x, int y) {
		//Returning true on valid move, false on invalid movie

		hasActed = true;
		Vector2 target = new Vector2(location.x + x, location.y + y); //setup our direction

		if (x == 0 && y == 0) {
			//If we got a move with no x and y, interpret as a pause;
			//Pause();
			return false;
		}

		//See if there's another move already queued
		if (!(nextMove == null)) {
			//Our move target was valid, but the timing was not acceptable
			return true;
		}

		//Check space is passable
		if (!CheckPassableAt(target))
		{
			return false;
		}

		nextMove = LerpTo(target);
		StartCoroutine(nextMove);

		return true;
	}

	public bool Move(Vector2 move) {
		return Move((int)move.x, (int)move.y);
	}

	public bool Move(Vector3 move) {
		return Move((int)move.x, (int)move.y);
	}

	public void Pause() {
		hasActed = true;
	}

	public void NextStep() {
		if (_ai != null) {
			_ai.NextStep();
			hasActed = true;
		} else {
			Debug.Log("ERROR: No AI assigned to " + transform.name);
		}
	}

	/*
	 *	PRIVATE METHODS
	 */

	public void AlignToTile(Vector3 newLocation) {
		transform.position = newLocation;
		location = CleanLocation(newLocation);
	}

	public void AlignToTile() {
		//Overloaded version incase I forget to use the normal version
		AlignToTile(transform.position);
	}

	private void Awake() {
		location = CleanLocation(transform.position);
	}

	private bool CheckPassableAt(Vector2 target) {
		//If a collider found, returns false; not passable Else, returns true Debug.DrawLine(transform.position,
		//target);
		try {
			GameObject hit = Physics2D.BoxCast(target, new Vector2(.5f, .5f), 0, Vector3.forward, Mathf.Infinity, ~itemLayer).transform.gameObject;
			if (hit.GetComponent<CombatData>()) {
				_cd.Attack(hit);
				hasActed = true;
			} else {
				return false;
			}
		} catch {
			Debug.Log("nothing found");
			return true;
		}
		return false;
	}

	private Vector2Int CleanLocation(Vector3 newLocation) {
		return new Vector2Int((int)newLocation.x, (int)newLocation.y);
	}


	private IEnumerator LerpTo(Vector2 newLoc) {

		Vector2 init = transform.position; //Initial position
		float i = 0;

		location = CleanLocation((Vector3)newLoc);

		//isMoving = true;

		while (i < 1) {

			i += Time.deltaTime * moveSpeed;
			transform.position = Vector2.Lerp(init, newLoc, i);

			yield return null;
		}

		StopCoroutine(nextMove);

		nextMove = null;

		//location = CleanLocation(transform.position);
		//isMoving = false;
	}



}

	/*
	 * OTHER
	 */

public enum Faction {
	Neutral,   //Entities that act but shouldn't be targeted
	Player,    //Anyone in the player's party
	Faction1,  //Friendly, aggressive faction
	Faction2,  //Enemy, aggressive faction 1
	Faction3,  //Enemy, aggressive faction 2
	Idle   //Neutral, but does not ever act (ie destructible walls, etc)
}
