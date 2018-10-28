using UnityEngine;

public static class RandomDirection {

	public static Vector2 Step() {
		int dir = Random.Range(1, 8);

		switch (dir) {
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
			default:
				return new Vector2(-1, 1);
		}
	}

}
