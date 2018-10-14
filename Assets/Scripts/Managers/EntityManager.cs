using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

	private static GameObject[] entityObjects;
	private static bool[] destroyedList;

	private static EffectManager _em;

	public MapEntity[] entities;

	public int actingEntity = 0;

	static private GameObject currentMap;
	static private GridLayout gl;

	//private GameObject _scene; //SceneController game object.


	/* PUBLIC METHODS *///

	public static void EntityDestroyed(GameObject entity) {
		destroyedList[System.Array.IndexOf(entityObjects, entity)] = true;
		PlaceDeathEffect(Vector3Int.FloorToInt(entity.transform.position));
	}

	/* PRIVATE METHODS */

	private void Awake() {

		SetSceneControls();
		GetCurrentMap();
		GetAllEntities();

	}

	private void Update() {


		while (entities[actingEntity].GetPlayable() == false) {
			if (destroyedList[actingEntity] == false) {
				entities[actingEntity].NextStep();
			}
			IncreaseActingEntityCounter();
		}

		/*
		if (destroyedList[actingEntity]) {
			IncreaseActingEntityCounter();
		} else if (entities[actingEntity].GetPlayable() == false) {
			entities[actingEntity].NextStep();
			IncreaseActingEntityCounter();
		}*/
	}

	private void IncreaseActingEntityCounter() {
		actingEntity++;
		CheckRoundDone();
	}

	private void SetSceneControls() {
		if (_em == null) {
			_em = GetComponent<EffectManager>();
		}

	}

	private void GetCurrentMap() {
		if (currentMap == null) {
			currentMap = GameObject.FindGameObjectWithTag("WorldMap");
			gl = currentMap.GetComponent<GridLayout>();
		}
	}


	private void CheckRoundDone() {

		if (System.Array.IndexOf(destroyedList, false) == -1) { //Check if there's no one left alive
			this.enabled = false;
		}

		if (actingEntity >= entities.Length) {
			StartNewTurn();
		}
	}

	private void StartNewTurn() {
		//Reset hasActed for all active entities
		//Start entity list again

		foreach (MapEntity ent in entities) {
			ent.hasActed = false;
		}

		actingEntity = 0;
	}


	private void GetAllEntities(bool newDestroyedList = true) {
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
			//Array.Sort(entities, SortEntity);
			destroyedList = new bool[entities.Length];
		}

	}

	static int SortEntity(GameObject a, GameObject b) {
		return a.GetComponent<CombatData>().initiative.CompareTo(b.GetComponent<CombatData>().initiative);
	}


	private void AlignAllToTile() {

		for (int i = 0; i < entities.Length; i++) {

			Vector3Int cellPosition = gl.WorldToCell(entities[i].transform.position);
			entities[i].AlignToTile(gl.CellToWorld(cellPosition));

		}
	}

	private static void PlaceDeathEffect(Vector3Int location) {
		try {
			_em.PlaceEffect(location);
		} catch {
			Debug.Log("ERROR: Couldn't place death effect.");
		}
	}

}
