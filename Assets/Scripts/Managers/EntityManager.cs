using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

	/*	ENTITY MANAGER
	*	Manages the control flow of entity actions.
	*
	*	Contains: list of entities as both gameObjects and MapEntities(entityObjects/entities), an array of bools
	*	controlling those that are inactive (destroyedList), references to the current map.
	*
	*	tl dr: Has every computer controlled entity act, then the player, then any computer entities left
	*	in the turn order.
	*/

	public MapEntity[] entities;

	private static GameObject[] entityObjects;
	private static bool[] destroyedList;

	private static EffectManager _em;
	private int actingEntity = 0;

	static private GridLayout gl;

	/* PUBLIC METHODS */

	public static void EntityDestroyed(GameObject entity) {
		//Called when an entity dies or is otherwise removed from play.
		//Sets the correct destroyed element so the entity is skipped when appropriate.
		destroyedList[System.Array.IndexOf(entityObjects, entity)] = true;
		PlaceDeathEffect(Vector3Int.FloorToInt(entity.transform.position));
	}

	/* PRIVATE METHODS */

	private void Awake() {
		//Regular setup for scene.
		SetSceneControls();
		GetAllEntities();
	}

	private void Update() {
		//Regular update. Effectively: all entities up to player-controlled act -- and if no players
		//left in the round, all remaining entities and new round starts.
		while (entities[actingEntity].GetPlayable() == false) {
			if (destroyedList[actingEntity] == false) {
				entities[actingEntity].NextStep();
			}
			IncreaseActingEntityCounter();
		}
	}

	private void IncreaseActingEntityCounter() {
		//Called every time an entity acts.
		actingEntity++;
		CheckRoundDone();
	}

	private void SetSceneControls() {
		//Finds the effect manager.
		if (_em == null) {
			_em = GetComponent<EffectManager>();
		}

	}

	private void CheckRoundDone() {
		//If no one is left alive, turn the manager off.
		if (System.Array.IndexOf(destroyedList, false) == -1) { //Check if there's no one left alive
			this.enabled = false;
			//TODO: Call game over.
		}

		if (actingEntity >= entities.Length) {
			StartNewTurn();
		}
	}

	private void StartNewTurn() {
		//Reset hasActed for all active entities and start entity list again
		foreach (MapEntity ent in entities) {
			ent.hasActed = false;
		}
		actingEntity = 0;
	}

	private void GetAllEntities(bool newDestroyedList = true) {
		//Gets a new list of entities.
		//if newDestroyedList is off, we know that the entities are updated but we need to keep
		//our dead enemies dead.
		entityObjects = GameObject.FindGameObjectsWithTag("Entity");
		entities = new MapEntity[entityObjects.Length];

		for (int i = 0; i < entities.Length; i++) {
			try {
				entities[i] = entityObjects[i].GetComponent<MapEntity>();
			} catch {
				Debug.LogError("ERROR: " + entities[i].name + "is tagged 'Entity' but is missing MapEntity component.");
			}
		}

		if (newDestroyedList) {
			destroyedList = new bool[entities.Length];
		}

	}

	static int SortEntity(GameObject a, GameObject b) {
		//Allows for sorting by initiative
		return a.GetComponent<CombatData>().initiative.CompareTo(b.GetComponent<CombatData>().initiative);
	}

	private void AlignAllToTile() {
		//Aligns entities and items to the grid. Mostly for visual purposes.
		for (int i = 0; i < entities.Length; i++) {

			Vector3Int cellPosition = gl.WorldToCell(entities[i].transform.position);
			entities[i].AlignToTile(gl.CellToWorld(cellPosition));

		}
	}

	private static void PlaceDeathEffect(Vector3Int location) {
		//Used for placing things like bloodstains, bones, whatever enemies drop on death.
		try {
			_em.PlaceEffect(location);
		} catch {
			Debug.Log("ERROR: Couldn't place death effect.");
		}
	}

}
