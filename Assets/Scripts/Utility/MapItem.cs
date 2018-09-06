using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour, ILootable<GameObject> {

	public string itemName = "Untitled Item";

	public ItemType itemType = ItemType.Gold;
	public int value = 10;

	static private GameObject currentMap;
	static private GridLayout gl;
	
	/* Utility Methods
	 */
	
	private void Awake() {
		GetCurrentMap();
		AlignToTile();

	}

	private void GetCurrentMap() {
		if (currentMap == null) {
			currentMap = GameObject.FindGameObjectWithTag("WorldMap");
			gl = currentMap.GetComponent<GridLayout>();
		}
	}
	
	private void AlignToTile() {
		//Used to force entities back into the correct tile
		
		Vector3Int cellPosition = gl.WorldToCell(transform.position);
		transform.position = gl.CellToWorld(cellPosition);
	}

	/* Public methods
	*/
	
	public void Loot(GameObject looter) {
		if (itemType == ItemType.Gold) {
			Debug.Log("Picked up " + value + " " + itemName + ".");
		}

		if (itemType == ItemType.Gold) {
			InventoryManager.ModGold(value);			
			GameObject.Destroy(this.gameObject);
		} else if (itemType == ItemType.InventoryItem) {
			InventoryManager.AddItem(new InventoryItem("rat poison"));
			GameObject.Destroy(this.gameObject);
		} else {
			Debug.Log("LOOT FAIL: ");
		}

	}
}


public enum ItemType {
	Gold, //Should immediately go into party gold
	Resource, //Should do a check and go into appropriate place
	Powerup,  //Should apply appropriate buff
	InventoryItem, //Should give item to inventory class
	KeyItem //Should go into quest log
}