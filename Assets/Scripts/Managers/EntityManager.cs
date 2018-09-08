using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {
	
	private static GameObject[] entityObjects;
	private static bool[] destroyedList;
	
	private PlayerInputManager _pim;
	
	public MapEntity[] entities;

	public int actingEntity = 0;
	
	static private GameObject currentMap;
	static private GridLayout gl;


	void Awake() {

		if (_pim == null) {
			_pim = GetComponent<PlayerInputManager>();
		}
		
		GetCurrentMap();
		GetAllEntities();
		
	}

	void Update() {

		if (!destroyedList[actingEntity]) { //Check that the bool associated with this list is not destroyed
			if (!entities[actingEntity].GetPlayable()) {
				entities[actingEntity].NextStep();
				actingEntity++;
			} else if (entities[actingEntity].hasActed) {
				actingEntity++;
			}
		} else {
			actingEntity++;
		}
		

		CheckRoundDone();


	}
	
	void GetCurrentMap() {
		if (currentMap == null) {
			currentMap = GameObject.FindGameObjectWithTag("WorldMap");
			gl = currentMap.GetComponent<GridLayout>();
		}
	}


	void CheckRoundDone() {
		
		if (System.Array.IndexOf(destroyedList, false) == -1) { //Check if there's no one left alive
			this.enabled = false;
		}
		
		if (actingEntity >= entities.Length) {
			StartNewTurn();
		}
	}

	void StartNewTurn() {
		//Reset hasActed for all active entities
		//Start entity list again

		foreach (MapEntity ent in entities) {
			ent.hasActed = false;
		}

		actingEntity = 0;
	}


	void GetAllEntities(bool newDestroyedList = true) {
		entityObjects = GameObject.FindGameObjectsWithTag("Entity");
		entities = new MapEntity[entityObjects.Length];

		for (int i = 0; i < entities.Length; i++) {
			try {
				entities[i] = entityObjects[i].GetComponent<MapEntity>();
			} catch {
				Debug.Log("ERROR: " + entities[i].name + "is tagged 'Entity' but is missing MapEntity component.");
			}
		}
		
		if (newDestroyedList) {
			destroyedList = new bool[entities.Length];
		}
		
	}
	
	
	void AlignAllToTile() {
		
		for (int i = 0; i < entities.Length; i++) {
			
			Vector3Int cellPosition = gl.WorldToCell(entities[i].transform.position);
			entities[i].AlignToTile(gl.CellToWorld(cellPosition));

		}
	}
	
	public static void EntityDestroyed(GameObject entity) {
		destroyedList[System.Array.IndexOf(entityObjects, entity)] = true;
	}

}
