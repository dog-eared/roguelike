using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathfinding : MapAI {

	/*	PLAYER PATHFINDING
	*	This class should be attached to the player character. Essentially, we're re-using the basic AI class and
	*	issuing commands whenever we touch the screen.
	*
	*	This class should only decide what to do with the given input -- the actual form of the input will be handled by
	*	the PlayerInputManager class.
	*
	*	Unlike the simple enemy patrol AI, this class will work backwards through a List (as opposed to an array) of
	*	valid locations.
	*/

	private int stepCounter = -1; //If -1, pathfinding not active;
	public List<Vector2> pathSteps; //Actual target will be index 0, counter will move backwards after steps located


	public override void NextStep() {

		if (pathSteps.Count > 0 && stepCounter != -1) {

			if (_me.GetLocation() == pathSteps[stepCounter]) {
				stepCounter--;

				if (stepCounter <= -1) {
					//If we've run out of steps, we can let this component become inactive.
					ClearPath();
					return;
				}
			}

			if (! _me.Move(NextStepDirection() ) ) {
				ClearPath();
				return;
			}

		}
	}

	public override Vector2 NextStepDirection() {

		Vector2 loc = _me.GetLocation();

		float x = Mathf.Clamp(pathSteps[stepCounter].x - loc.x, -1f, 1);
		float y = Mathf.Clamp(pathSteps[stepCounter].y - loc.y, -1f, 1);

		return new Vector2(x, y);

	}

	public override void AddWaypoint(Vector2 target) {
		//Index 0 should be our actual target
		pathSteps.Add(target);

		//Calculate waypoints to get to target, add them to the list

		stepCounter = pathSteps.Count - 1;
	}

	private void AddMidpoint(Vector2 location) {
		pathSteps.Add(location);
	}

	private void ClearPath() {
		pathSteps = new List<Vector2>();
		stepCounter = -1;
		//_me.followingPath = false;
	}

}
