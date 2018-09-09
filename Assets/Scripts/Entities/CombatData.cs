using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatData : MonoBehaviour {

	public int currentHP = 100;
	public int maxHP = 100;
	public int attackPower = 25;

	public bool destroyBodyOnDeath = true;
	
	public void Attack(GameObject target) {
	
		CombatData opposed;
		
		try {
			opposed = target.GetComponent<CombatData>();
			opposed.Damage(attackPower);
			Debug.Log(transform.name + " hit " + target.name + "for " + attackPower);
		} catch {
			Debug.Log("ATTACK ERROR: Couldn't find " + target.name + "'s CombatData");
		}
	}
	
	public void Damage(int damage) {
		currentHP -= damage;
		
		if (currentHP < 0) {
			Debug.Log(this.gameObject.name + " was killed!!");
			EntityManager.EntityDestroyed(this.gameObject);
			
			if (destroyBodyOnDeath) {
				Destroy(this.gameObject);
			}
		}
	}
	
}
