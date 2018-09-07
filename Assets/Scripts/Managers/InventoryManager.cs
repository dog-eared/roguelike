using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	public static int gold;
	public static List<InventoryItem> inv = new List<InventoryItem>();
	
	static string invText = "";

	public static void ModGold(int modValue) {
		gold += modValue;
	}

	public static void AddItem(InventoryItem item) {
		inv.Add(item);
		invText += item.GetName();
		
	}

	void OnGUI() {
		
		GUI.Label(new Rect(20, 20, 100, 100), "Gold: " + gold);
		GUI.Label(new Rect(20, 60, 400, 100), "Items: "  + invText);
		
	}
}
