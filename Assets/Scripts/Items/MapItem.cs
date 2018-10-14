using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour, ILootable<GameObject>, IAlignable {
	/* 	MAP ITEM
	*	Used to represent items that could be picked up by the player. These hold an InventoryItem which is given to
	*	the player when they call loot on this object.
	*/

	public string mapName = "Untitled Item"; //this can be separate from the actual GameObject name; the player can see
											 //something in-character but I can see something sensible in the editor

	public InventoryItem inventoryItem; //ScriptableObject to give to the player when they loot this item (if relevant)

	public ItemType itemType; //will default to giving user gold
	public int value = 1;


	private void Awake() {
		AlignToTile();
	}

	public void AlignToTile() {
		//Used to force entities back into the correct tile
		transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, 0);
	}

	/* PUBLIC METHODS */

	public void Loot(GameObject looter) {
		if (itemType == ItemType.Gold) {
			Debug.Log("Picked up " + value + " " + mapName + ".");
			InventoryManager.ModGold(value);
			GameObject.Destroy(this.gameObject);
		} else if (itemType == ItemType.InventoryItem) {
			InventoryManager.AddItem(inventoryItem);
			GameObject.Destroy(this.gameObject);
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
