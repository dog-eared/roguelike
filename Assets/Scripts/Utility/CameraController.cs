using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private const int zDist = -10; //Z placement of camera when it moves

	public float smoothing = 0.3f;
	public Vector2 topLeftBounds;
	public Vector2 bottomRightBounds;

	public static GameObject target;

	private Vector3 velocity = Vector3.zero;

	public static void SetTarget(GameObject newTarget) {
		target = newTarget;
	}

	public static void ClearTarget() {
		target = null;
	}

	void Update() {
		if (target != null) {
			Vector3 targetPosition = target.transform.TransformPoint(new Vector3(0, 0, -10));
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
		}

	}

}
