using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EffectManager : MonoBehaviour {

	public Tilemap _underdeco;
	public Tile[] bloodEffects;
	
	public void PlaceEffect(Vector3Int location) {
		try {
			_underdeco.SetTile(location, bloodEffects[0]);
		} catch {
			Debug.Log("EFFECT MANAGER ERROR: No underdeco layer set.");
		}
	}

}
