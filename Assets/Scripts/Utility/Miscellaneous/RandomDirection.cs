using UnityEngine;

public static class RandomDirection {

	/*	RANDOM DIRECTION
	*	Utility class to get random directions in various contexts.
	*
	*/

	public static Vector2 Step(bool includePause = false) {
		//Gets a random step
		//NOTE: In C#, switches are hashed, so this should be pretty fast.
		int dir;

		if (includePause) {
			dir = Random.Range(0, 8);
		} else {
			dir = Random.Range(1, 8);
		}

		switch (dir) {
			case 0:
				return Vector2.zero;
			case 1:
				return new Vector2(0, 1);
			case 2:
				return new Vector2(1, 1);
			case 3:
				return new Vector2(1, 0);
			case 4:
				return new Vector2(1, -1);
			case 5:
				return new Vector2(0, -1);
			case 6:
				return new Vector2(-1, -1);
			case 7:
				return new Vector2(-1, 0);
			default: //8
				return new Vector2(-1, 1);
		}
	}

}
