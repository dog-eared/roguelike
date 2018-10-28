using UnityEngine;

public class DoNotDestroy : MonoBehaviour {

	/*	DO NOT DESTROY
	*	Minimal class to  preserve global controller class across all scenes.
	*/

	private static bool created;

	private void Awake() {
		//Flips created (incase of error w. this object being reloaded/recreated)
		if (!created) {
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			Debug.Log("Global Controller called Awake() but has already been created.");
		}
	}
}
