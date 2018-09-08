using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	private const int zDist = -10; //Z placement of camera when it moves

	
	public void CenterOn(GameObject newParent) {
		transform.position = new Vector3(newParent.transform.position.x, newParent.transform.position.y, zDist);
	}
	
	
	
}
