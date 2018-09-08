using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_SimplePatrol : EnemyAI {

	public int patrolCounter = 0;
	public Vector2[] patrolLocations;

	public override void NextStep() {

		Debug.Log(_me.GetLocation() + " to " + patrolLocations[patrolCounter]);

		if (_me.GetLocation() != patrolLocations[patrolCounter] ) {
			_me.Move(NextStepDirection());
		}
		else {
			patrolCounter++;
		}

		if (patrolCounter >= patrolLocations.Length) {
			patrolCounter = 0;
		}

	}


	private Vector2 NextStepDirection() {

		Vector2 loc = _me.GetLocation();
		
		float x = Mathf.Clamp(patrolLocations[patrolCounter].x - loc.x, -1f, 1);
		float y = Mathf.Clamp(patrolLocations[patrolCounter].y - loc.y, -1f, 1);

		Debug.Log("x " + x + " y " + y);
		
		return new Vector2(x, y);
		
	}


}
