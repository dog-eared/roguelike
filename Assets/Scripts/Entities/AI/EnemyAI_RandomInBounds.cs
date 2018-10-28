using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_RandomInBounds : MapAI {

	/* 	ENEMY AI: Random In Bounds
	*	Walks dumbly within a square boundary. Intended for NPCs in a room.
	*/

	public int pauseFrequency = 10; //Percentage chance to not act per turn

	public Vector2Int topLeftBound;		//Used rather than Bounds object because it's easier to parse x/y coords
	public Vector2Int bottomRightBound; //as they relate to room sizing

	private const int maxStepCheck = 3; //Maximum number of checks for move



	public override void NextStep() {
		if (Random.Range(1, 100) > pauseFrequency) {
			_me.Move(NextStepDirection());
		} else {
			_me.Pause();
		}
	}

	public override Vector2 NextStepDirection() {

		for (int i = 0; i < maxStepCheck; i++) {
			//Apply random value to location for attempted move
			Vector2 move = RandomDirection.Step();
			Vector2 locationCheck = _me.location + move;

			//Check move is ok; if so, return valid move
			if (locationCheck.x > topLeftBound.x && locationCheck.x < bottomRightBound.x
			&&	locationCheck.y < topLeftBound.y && locationCheck.y > bottomRightBound.y) {
				return move;
			}
		}

		//Loop never worked; return zero
		return Vector2.zero;
	}

	public override void AddWaypoint(Vector2 target) {
		Debug.Log("ERROR: EnemyAI_RandomInBounds does not allow you to add waypoints");
	}

}
