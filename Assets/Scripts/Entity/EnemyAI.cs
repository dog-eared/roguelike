using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour {
	
	/* ENEMY AI
	 *
	 * Parent class from which all AI behaviours should inherit. Does basic tasks (getting the MapEntity if possible)
	 * 
	 */

	protected MapEntity _me;

	private void Awake() {
		try {
			_me = GetComponent<MapEntity>();
		} catch {
			Debug.Log("ERROR 'MapEntity' not found for " + transform.name);
		}
	}

	public abstract void NextStep(); //This should be called each step by EntityManager for each NPC entity

}
