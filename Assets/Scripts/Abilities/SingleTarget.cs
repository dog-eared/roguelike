using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "singleTarget", menuName = "Ability - Single Target", order = 2)]
public class SingleTarget : AbilityData {

	public void SetData(int range, int power) {
		this.range = range;
		this.power = power;
	}

}
