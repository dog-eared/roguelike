using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerVisibilityManager : MonoBehaviour {
/*	PLAYER VISIBILITY MANAGER
*
*	This manager determines what tiles are visible to the player.
*
*	Implements the recursive shadowcast algorithm as defined at
*	https://gridbugs.org/visible-area-detection-recursive-shadowcast/
*/

	public Vector3 Location {get; private set;}
	public Tilemap collisionMap;

	private int layerMask = 1 << 10;//only get the layer of walls

	void FixedUpdate() {
		//TODO remove this
		Location = transform.position;

	}



}
