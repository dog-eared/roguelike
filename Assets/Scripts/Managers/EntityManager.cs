using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {
	
	private GameObject[] entityObjects;
	private PlayerInputManager _pim;
	
	public MapEntity[] entities;

	public int actingEntity = 0;
	
	

	void Awake() {

		if (_pim == null) {
			_pim = GetComponent<PlayerInputManager>();
		}
		
		GetAllEntities();
		
	}

	void Update() {

		if (!entities[actingEntity].GetPlayable()) {
			entities[actingEntity].NextStep();
			actingEntity++;
		} else if (entities[actingEntity].hasActed) {
			actingEntity++;
		}

		CheckRoundDone();


	}

	void CheckRoundDone() {
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


	void GetAllEntities() {
		entityObjects = GameObject.FindGameObjectsWithTag("Entity");
		entities = new MapEntity[entityObjects.Length];

		for (int i = 0; i < entities.Length; i++) {
			try {
				entities[i] = entityObjects[i].GetComponent<MapEntity>();
			} catch {
				Debug.Log("ERROR: " + entities[i].name + "is tagged 'Entity' but is missing MapEntity component.");
			}
		}
		
	}

}
