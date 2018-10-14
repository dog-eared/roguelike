using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILootable<T> {
	/*	ILootable
	*	Simple interface for objects players can loot
	*/

	void Loot(T looter);

}
