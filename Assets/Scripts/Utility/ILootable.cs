using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILootable<T> {

	void Loot(T looter);

}
