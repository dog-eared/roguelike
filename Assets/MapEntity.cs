using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour {

	/* MAP ENTITY
     * Class for any entity who appears on the game board who the player
     * could reasonably interact with, or who should adhere to board rules.
     *
	 * This class holds the actual methods to move pieces on the board and
	 * check for collisions. It also contains the entity's faction -- this is
	 * so that we can iterate through the list of active pieces in proper order
     */


	public string entityName = "Untitled Entity"; //Name that the user sees in GUI/ingame, rather than using the 
												  //GameObject name
	public Faction faction; 	//Used to determine if/when they act in the turn order, general allegience

	public Vector2Int location; //In general we'll use this for step/distance calculations rather than transform.position
								//It'll be a little more reliable once we have characters moving around in tandem
	
	public float moveSpeed = 10f; //Speed of movements on board
		
	static private GameObject currentMap;
	static private GridLayout gl;

	IEnumerator nextMove;

	bool isMoving = false; 


	private void Awake() {
		
		
		
		GetCurrentMap();
		AlignToTile();
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
		
	}
	
	/*
	 *	PRIVATE METHODS 
	 */
	
	private void GetCurrentMap() {
		if (currentMap == null) {
			currentMap = GameObject.FindGameObjectWithTag("WorldMap");
			gl = currentMap.GetComponent<GridLayout>();
			Debug.Log("Got currentMap");
		}
	}

	private bool CheckPassableAt(Vector2 target) {
		//If a collider found, returns false; not passable
		//Else, returns true
		if (Physics2D.Raycast(target, Vector3.forward, Mathf.Infinity)) {
			Debug.Log("Tag is " + Physics2D.Raycast(target, Vector3.forward, Mathf.Infinity).transform.tag);
			return false;
		}

		Debug.Log("nothing found");
		return true;
	}

	private void AlignToTile() {
		//Used to force entities back into the correct tile
		
		Vector3Int cellPosition = gl.WorldToCell(transform.position);
		transform.position = gl.CellToWorld(cellPosition);

		UpdateLocation();
	}

	private void UpdateLocation() {
		location = new Vector2Int((int)transform.position.x, (int)transform.position.y);
	}
	
	private IEnumerator LerpTo(Vector2 newLoc) {
       
		Vector2 init = transform.position; //Initial position
		float i = 0;

		isMoving = true;

		while (i < 1) {

			i += Time.deltaTime * moveSpeed;
			transform.position = Vector2.Lerp(init, newLoc, i);

			yield return null;
		}

		StopCoroutine(nextMove);
		nextMove = null;

		AlignToTile();
		isMoving = false;
	}



}

	/*
	 * OTHER
	 */ 

public enum Faction {
	Neutral,  //Entities that act but shouldn't be targeted
	Player,   //Anyone in the player's party
	Faction1, //Friendly, aggressive faction
	Faction2, //Enemy, aggressive faction 1
	Faction3  //Enemy, aggressive faction 2
}
