using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapAI : MonoBehaviour {

	/*	MAP AI
	 *
	 *	Parent class from which all AI behaviours should inherit. Does basic tasks (getting the MapEntity if possible),
	 *	and ensures child classes implement generic methods.
	 *
	 */

	protected MapEntity _me;

	protected virtual void Awake() {
		try {
			_me = GetComponent<MapEntity>();
		} catch {
			Debug.Log("ERROR: 'MapEntity' not found for " + transform.name);
		}
	}

	public abstract void NextStep(); //This should be called each step by EntityManager for each NPC entity

	public virtual Vector2 NextStepDirection() {
		return Vector2.zero;
	}

	public virtual void AddWaypoint(Vector2 location) {
		Debug.Log(this.GetType() + " does not implement way points.");
	}

}
