using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable<T> {

	/*	IAttackable
	*	Interface for anything players can hit
	*/

	void Attack(T attackData);

}
