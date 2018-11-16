using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RandomInBounds : MapAI {

	/* 	AI: Random In Bounds
	*	Walks dumbly within a square boundary. Intended for foes/NPCs stuck in a room.
	*	Good for both neutral and hostile characters.
	*/

	public int pauseFrequency = 10; //Percentage chance to not act per turn

	public Vector2Int topLeftBound;		//Used rather than Bounds object because it's easier to parse x/y coords
	public Vector2Int bottomRightBound; //as they relate to room sizing

	protected const int maxStepCheck = 3; //Maximum number of checks for move

	public override void NextStep() {
		//Gets a random value to see if should move, else, pauses
		if (Random.Range(1, 100) > pauseFrequency) {
			_me.Move(NextStepDirection());
		} else {
			_me.Pause();
		}
	}

	public override Vector2 NextStepDirection() {
		//Checks random step times maxStepCheck -- when valid, returns the direction
		//If no valid direction found, returns (0, 0) which should trigger pause.

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

		//Loop did not work; return zero
		return Vector2.zero;
	}

}
