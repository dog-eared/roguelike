using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour {

	public int CheckDistance = 4; //Represents how far to send rays to calculate line of sight.

	public Vector2[] targets;
	Camera c;

	private const int layerMask = 1 << 10;

	void Awake() {
		c = Camera.main;
		BuildTargets();
	}

	void FixedUpdate() {
		for (int i = 0; i < targets.Length; i++) {
			RaycastHit2D hit = Physics2D.Raycast(c.transform.position, targets[i], CheckDistance, layerMask);

			if (hit) {
				Debug.DrawLine(c.transform.position, hit.point, Color.white);
			}

		}
	}

	void BuildTargets() {

		targets = new Vector2[(CheckDistance) * 4];

		int horiz = CheckDistance / 2;
		int vert = horiz;

		//Top Row
		for (int t = 0; t <= CheckDistance; t++) {
			Debug.Log("TOP: Built " + t);
			targets[t] = new Vector2(-horiz + t, vert);
		}

		//Bottom Row
		for (int b = 0; b <= CheckDistance; b++) {
			Debug.Log("BOTTOM: Built " + (CheckDistance + 1 + b));
			targets[CheckDistance + 1 + b] = new Vector2(-horiz + b, -vert);
		}

		//Left Side
		for (int l = 1; l < CheckDistance; l++) {
			Debug.Log("LEFT: Built " + (2* CheckDistance + 1 + l));
			targets[(2 * CheckDistance) + 1 + l] = new Vector2(-horiz, -vert + l);
		}

		//Right Side
		for (int r = 1; r < CheckDistance; r++) {
			Debug.Log("RIGHT: Built " + (3* CheckDistance + r));
			targets[(3 * CheckDistance) + r] = new Vector2(horiz, -vert + r);
		}

	}

}
