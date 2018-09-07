using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryItem))]
public class InventoryEditor : Editor {

	Editor currentEditor;
	InventoryItem _ii;
	
	public void OnEnable() {
		_ii = (InventoryItem)target; //Loads the current item properly
	}
	
	public override void OnInspectorGUI() {
		_ii.itemName = EditorGUILayout.TextField("Name", _ii.itemName);
	}
	
	
}
