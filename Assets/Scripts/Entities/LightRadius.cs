using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadius : MonoBehaviour {

	public GameObject c; //user

	public int lightRadius = 4;
	public Vector2[][] lightPoints;


	int layerMask = 1 << 10;

	void Awake() {
		//c = Camera.main;

		lightPoints = new Vector2[lightRadius * 2 + 1][];

		for (int x = 0; x < (lightRadius * 2 + 1); x++) {
			lightPoints[x] = new Vector2[lightRadius * 2 + 1];

			for (int y = 0; y < (lightRadius * 2 + 1); y++) {
				lightPoints[x][y] = new Vector2(x - lightRadius - 0.5f, y - lightRadius - 0.5f);
				Debug.Log("Successfully made y");
			}

			Debug.Log("Successfully made x");

			Debug.Log(lightPoints[0]);
			Debug.Log(lightPoints[0][0]);
		}
	}

	void FixedUpdate() {

		Vector3 origin = new Vector3(Mathf.Round(c.transform.position.x), Mathf.Round(c.transform.position.y), 0);

		for (int x = 0; x < lightPoints.Length; x++) {
			for (int y = 0; y < lightPoints[x].Length; y++) {

				RaycastHit2D hit = Physics2D.Raycast(origin, (Vector3)lightPoints[x][y], lightRadius, layerMask);

				if (hit) {
					Debug.DrawLine(origin, hit.point, Color.white);
				} else {
					Debug.DrawLine(origin, origin + (Vector3)lightPoints[x][y], Color.yellow);
				}
			}
		}
	}


}
