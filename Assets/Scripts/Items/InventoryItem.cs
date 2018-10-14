using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "invItem", menuName = "Inventory Item", order = 1)]
public class InventoryItem : ScriptableObject {
	/*	INVENTORY ITEM
	*	Scriptable data representing items that are already in the player's inventory
	*/

	public string itemName = "New Item";



	public string GetName() {
		return itemName;
	}

}
