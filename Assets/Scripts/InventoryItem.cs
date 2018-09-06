using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {
	
	private string itemName = "New Item";
	
	public InventoryItem(string itemName) {
		this.itemName = itemName;
	}
	

	public string GetName() {
		return itemName;
	}
	
}
