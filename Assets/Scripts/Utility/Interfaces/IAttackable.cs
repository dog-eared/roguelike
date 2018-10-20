using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable<T, U> {

	/*	IAttackable
	*	Interface for anything players can hit
	*/

	void Attack(T attackWith, U target);
	void Damage(T attackData);

}
