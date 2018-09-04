using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {
	
	private GameObject[] entityObjects;
	private PlayerInputManager _pim;
	
	public MapEntity[] entities;
	
	

	void Awake() {

		if (_pim == null) {
			_pim = GetComponent<PlayerInputManager>();
		}
		
		GetAllEntities();
		
	}

	void Update() {

		if (Input.GetKeyDown("1")) {
			_pim.SetPlayable(entities[0]);			
		}

		if (Input.GetKeyDown("2")) {
			_pim.SetPlayable(entities[1]);			
		}

		if (Input.GetKeyDown("3")) {
			_pim.SetPlayable(entities[2]);			
		}

	}


	void GetAllEntities() {
		entityObjects = GameObject.FindGameObjectsWithTag("Entity");
		entities = new MapEntity[entityObjects.Length];

		for (int i = 0; i < entities.Length; i++) {
			try {
				entities[i] = entityObjects[i].GetComponent<MapEntity>();
			} catch {
				Debug.Log("Entity " + entities[i].name + " is missing MapEntity Component!");
			}
		}
		
	}

}
