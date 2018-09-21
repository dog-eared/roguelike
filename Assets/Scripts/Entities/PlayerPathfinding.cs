using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathfinding : MapAI {

	int stepCounter = -1; //If -1, pathfinding not active
	public List<Vector2> pathSteps; //Actual target will be index 0, counter will move backwards after steps located

	public override void NextStep() {
		Debug.Log("PLAYER NEXT STEP");

		if (pathSteps.Count > 0 && stepCounter != -1) {

			if (_me.GetLocation() == pathSteps[stepCounter]) {
				stepCounter--;

				if (stepCounter <= -1) {
					ClearPath();
				}
			}

			_me.Move(NextStepDirection());

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
		Debug.Log(stepCounter + " is value of stepCounter");
	}

	private void ClearPath() {
		pathSteps = new List<Vector2>();
		stepCounter = -1;
		_me.followingPath = false;
	}

	private void AddMidpoint(Vector2 location) {
		pathSteps.Add(location);
	}


}
