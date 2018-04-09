using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeactivator : MonoBehaviour 
{
	#region Unity callbacks
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "killbox")
		{
			Zombie_Pool.zombiePoolInstance.CallRemoveZombie(this.gameObject.name);
			this.gameObject.SetActive(false);
			// AIHandler.instance.CallRemoveFromList(this.gameObject.name);
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "killbox")
		{
			Zombie_Pool.zombiePoolInstance.CallRemoveZombie(this.gameObject.name);
			this.gameObject.SetActive(false);
			// AIHandler.instance.CallRemoveFromList(this.gameObject.name);
		}
	}
	#endregion
}
