using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_SimplePatrol : MapAI {

	/* 	ENEMY AI: SimplePatrol
	*	Walks dumbly between all given waypoints.
	*/

	public int patrolCounter = 0;
	public Vector2[] steps;

	public override void NextStep() {

		if (steps.Length == 0) {
			_me.Pause();
		}

		if (_me.GetLocation() == steps[patrolCounter] ) {
			patrolCounter++;
		}

		if (patrolCounter >= steps.Length) {
			patrolCounter = 0;
		}

		_me.Move(NextStepDirection());

	}

	public override Vector2 NextStepDirection() {

		Vector2 loc = _me.GetLocation();

		float x = Mathf.Clamp(steps[patrolCounter].x - loc.x, -1f, 1);
		float y = Mathf.Clamp(steps[patrolCounter].y - loc.y, -1f, 1);
		return new Vector2(x, y);

	}

	public override void AddWaypoint(Vector2 target) {
		Debug.Log("ERROR: EnemyAI_SimplePatrol does not allow you to add waypoints");
	}

}
