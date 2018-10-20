using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilities : MonoBehaviour {

	/*	ENTITY ABILITIES
	*	Container class for abilities of this entity.
	*
	*	Contains methods to use an ability and generate a new basic attack. Contains a helper method (attack) which
	*	acts as an 'alias' to generic attacks.
	*/

	public List<AbilityData> abilities;

	void Awake() {
		//TODO: Placeholder until I flesh this Count
		CreateAttackAbility(10);
	}

	public void CreateAttackAbility(int attackPower) { //Can be expanded to add other details
		//Creates a new normal attack with given attack power.
		//Intended for when new weapon equipped or entity instantiated
		SingleTarget attack = (SingleTarget)ScriptableObject.CreateInstance("SingleTarget");
		attack.SetData(1, 10);

		if (abilities.Count == 0) {
			abilities.Add(attack);
		} else {
			abilities.RemoveAt(0);
			abilities.Insert(0, attack); //Put it first
		}

	}

	public void Attack(GameObject target) {
		Debug.Log(name + " attacks " + target.name);
		UseAbility(0, target);
	}

	public void UseAbility(int abIndex = 0, GameObject target = null) {
		AbilityData _ab = abilities[abIndex]; //alias

		try {
			switch (_ab.GetType().ToString()) {
				case ("SingleTarget"):
					if (CheckInRange(this.gameObject, target, _ab.range)) {
						Debug.Log("Used " + _ab.displayName + " on " + target.name);
						target.GetComponent<CombatData>().Damage(_ab.power);
					}
					break;
				default:
					break;

			}
		} catch {
			Debug.Log("Couldn't use ability");
		}

	}

	private bool CheckInRange(GameObject a, GameObject b, int range) {
		//Checks for less than or equal to given range for both transforms
		//Must pass both to return true.
		return ((Mathf.Abs(a.transform.position.x - b.transform.position.x) <= range)
			&& (Mathf.Abs(a.transform.position.y - b.transform.position.y) <= range));
	}

}
