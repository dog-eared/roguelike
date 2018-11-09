using UnityEngine;

public class CombatData : MonoBehaviour {

	/*	COMBAT DATA
	*	Used to hold data for entitties that have the ability to be damaged or deal damage.
	*
	*	TODO: Take out all actions from this data, and have a general CombatManager class do the actual work.
	*	Then, this can just be a data file (and doesn't have to inherit from MonoBehaviour);
	*/

	public int currentHP = 100;
	public int maxHP = 100;
	public int attackPower = 25;
	public int initiative = 10;

	public bool destroyBodyOnDeath = true;



	public void Attack(GameObject target) {
		//General attack method, called by this object upon the target.
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
		//Called by the opponent on this class when damaged.
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
