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
			this.gameObject.SetActive(false);
			Zombie_Pool.zombiePoolInstance.CallRemoveZombie(this.gameObject.name);
			AIHandler.instance.CallRemoveFromList(this.gameObject.name);
		}
	}
	#endregion
}
