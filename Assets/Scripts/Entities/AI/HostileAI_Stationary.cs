using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAI_Stationary : MapAI {

	/* 	HOSTILE AI: Stationary
	*	Checks for hostiles in range. Attempts attack on one of them.
	*/

	public Vector2 attackRange = Vector2.one;

	public Collider2D[] inRange;

	protected override void Awake() {
		base.Awake();
	}

	public override void NextStep() {
			if (!_me.hasActed) {
			inRange = Physics2D.OverlapBoxAll(_me.location, attackRange, 0, 1 << 9);

			if (inRange.Length > 1) {
				GameObject target = inRange[Random.Range(0, inRange.Length - 1)].gameObject;
				if (target.transform != transform) {
					try {
						_me.Attack(target);
						_me.hasActed = true;
					} catch {
						Debug.Log("ERROR: " + this.gameObject.name + " couldn't hit " + target.name + " for some reason.");
					}
				}
			}
		}
	}

}
