﻿using System.Collections;
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
	public float moveSpeed = 10f; //Speed of movements on board

	public EnemyAI _ai;
	public CombatData _cd;
	
	[Header("Turn:")]
	public bool canAct = true;
	public bool hasActed = false;
	public Vector2Int location; //In general we'll use this for step/distance calculations rather than transform.position
								//It'll be a little more reliable once we have characters moving around in tandem

	static private int itemLayer = 1 << 8; //We use this to ignore the lootable item layer when checking for collisions

	IEnumerator nextMove;
	
	//bool isMoving = false;
	
	private void Awake() {
		location = CleanLocation(transform.position);
	}

	
	/*
	 *	PUBLIC METHODS 
	 */

	public void Move(int x, int y) {

		Vector2 target = new Vector2(location.x + x, location.y + y); //setup our direction
		
		//See if there's another move already queued
		if (!(nextMove == null)) {
			return;
		}
		
		//Check space is passable
		if (!CheckPassableAt(target)) {
			return;
		}
		
		nextMove = LerpTo(target);
		StartCoroutine(nextMove);
		hasActed = true;

	}

	public void Move(Vector2 move) {
		Move((int)move.x, (int)move.y);
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

	public void Pause() {
		hasActed = true;
	}


	public Vector2Int GetLocation() {
		return location;
	}

	public bool GetPlayable() {
		return faction == Faction.Player;
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

	private bool CheckPassableAt(Vector2 target) {
		//If a collider found, returns false; not passable
		//Else, returns true
		if (Physics2D.Raycast(target, Vector3.forward, Mathf.Infinity, ~itemLayer)) {
			GameObject hit = Physics2D.Raycast(target, Vector3.forward, Mathf.Infinity).transform.gameObject;
			if (hit.GetComponent<CombatData>()) {
				_cd.Attack(hit);
				hasActed = true;
			}
			return false;
		}

		Debug.Log("nothing found");
		return true;
	}

	
	public void AlignToTile(Vector3 newLocation) {
		transform.position = newLocation;
		location = CleanLocation(newLocation);	
	}
	
	public void AlignToTile() {
		//Overloaded version incase I forget to use the normal version
		AlignToTile(transform.position);
	}
	
	private Vector2Int CleanLocation(Vector3 newLocation) {
		return new Vector2Int((int)newLocation.x, (int)newLocation.y);
	}
	
	private IEnumerator LerpTo(Vector2 newLoc) {
       
		Vector2 init = transform.position; //Initial position
		float i = 0;

		//isMoving = true;

		while (i < 1) {

			i += Time.deltaTime * moveSpeed;
			transform.position = Vector2.Lerp(init, newLoc, i);

			yield return null;
		}

		StopCoroutine(nextMove);
		nextMove = null;

		location = CleanLocation(transform.position);
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