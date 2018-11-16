using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI_DumbSeekInBounds : AI_RandomInBounds {

	/* 	HOSTILE AI: Dumb Seek In Bounds
	*	Walks dumbly within a square boundary. If a character is in bounds, moves to attack them.
	*/

	public MapEntity _player;
	private Vector2 playerLoc;

	public bool playerInBounds;

	protected override void Awake() {
		base.Awake();
		try {
			_player = EntityManager.PlayerEntity;
		} catch {
			Debug.Log("ERROR: " + this.gameObject.name + "Couldn't get player object.");
		}
	}


	public override Vector2 NextStepDirection() {

		playerInBounds = CheckTargetInBounds();

		if (playerInBounds) {
			Vector2 loc = _me.GetLocation();

			float x = Mathf.Clamp(playerLoc.x - loc.x, -1f, 1);
			float y = Mathf.Clamp(playerLoc.y - loc.y, -1f, 1);

			return new Vector2(x, y);

		} else {
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

	private bool CheckTargetInBounds() {
		if (_player != null) {
			playerLoc = _player.GetLocation();

			if (playerLoc.x > topLeftBound.x && playerLoc.x < bottomRightBound.x
			&&	playerLoc.y < topLeftBound.y && playerLoc.y > bottomRightBound.y) {
				Debug.Log("playerLoc is in bounds");
				return true;
			}

		} else {
			try {
				_player = EntityManager.PlayerEntity;
			} catch {}
		}
		return false;
	}

}
