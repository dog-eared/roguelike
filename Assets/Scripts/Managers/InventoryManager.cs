using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager {

	/*	INVENTORY MANAGER
	*	Handles storage of player's inventory items. Will be expanded out to direct methods involving use of Items
	*	to the right place.
	*
	*	Contains: variables for gold and inventory list, methods to add items.
	*/

	public static int gold;
	public static List<InventoryItem> inv = new List<InventoryItem>();

	public static string invText = "";

	public static void ModGold(int modValue) {
		gold += modValue;

		if (gold < 0) { gold = 0; }

	}

	public static void AddItem(InventoryItem item) {
		inv.Add(item);
		invText += item.GetName();
	}

}
