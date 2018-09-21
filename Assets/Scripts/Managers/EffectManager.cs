using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EffectManager : MonoBehaviour {

	public Tilemap _underdeco;
	public Tilemap _effects;
	public Tilemap _data;
	
	public Tile[] bloodEffects;
	public Tile highlight;
	
	List<Vector3Int> highlightTiles = new List<Vector3Int>();
	
	public void MouseHighlight(Vector3 location) {
		WipeHighlights();
		PlaceHighlight(location);
	}
	
	public void PlaceHighlight(Vector3 location, bool includeDraw = true) {
		highlightTiles.Add(new Vector3Int((int)location.x, (int)location.y, -10));
		
		if (includeDraw) {
			DrawHighlights();
		}
		
	}
	
	public void DrawHighlights() {
		foreach (Vector3Int hl in highlightTiles)
		{
			_data.SetTile(hl, highlight);
		}
	}

	
	public void WipeHighlights() {
		_data.ClearAllTiles();
		highlightTiles = new List<Vector3Int>();
	}
	
	public void PlaceEffect(Vector3Int location) {
		try {
			_underdeco.SetTile(location, bloodEffects[0]);
		} catch {
			Debug.Log("EFFECT MANAGER ERROR: No underdeco layer set.");
		}
	}

}
