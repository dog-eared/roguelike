using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	/*	GUI MANAGER
	*	Handles GUI elements including: buttons to access interface/issue commands, feedback for the player.
	*
	*	NOTE: Currently, this is implemented with Unity's OnGUI methods. When the game is advanced enough to
	*	warrant a more in depth interface, much of this method will have to be rewritten.
	*
	*	NOTE 2: Might break this out into multiple modules as well -- depends on the overall amount of work.
	*/

	public int fontSizing = 18;

	public CombatData _player; 	//TODO: Make this static when there's a PlayerData object to create/save the object each
	 							//scene load.

	private void OnGUI() {
		GUI.skin.GetStyle("label").fontSize = fontSizing;

		GUI.BeginGroup (new Rect (20, 20, Screen.width - 20, 150));
		GUI.Label(new Rect(0, 0, 300, 30), "Gold: " + InventoryManager.gold);

		if (_player == null) {
			//Show health
			GUI.Label(new Rect(0, 25, 300, 30), "Health: DEAD.");
		} else {
			GUI.Label(new Rect(0, 25, 300, 30), "Health: " + _player.currentHP + " of " + _player.maxHP + ".");
		}

		GUI.EndGroup();
	}


}
