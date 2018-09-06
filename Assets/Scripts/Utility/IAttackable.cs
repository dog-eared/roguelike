using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable<T> {

	void Attack(T attackData);
	
}
