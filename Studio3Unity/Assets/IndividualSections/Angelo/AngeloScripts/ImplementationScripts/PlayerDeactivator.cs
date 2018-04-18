using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeactivator : MonoBehaviour 
{
	#region Unity callbacks
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "killbox")
		{
			GameManagerBase.instance.playersDead.Add(this.gameObject);
			this.gameObject.SetActive(false);
			// AIHandler.instance.CallRemoveFromList(this.gameObject.name);
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "killbox")
		{
			GameManagerBase.instance.playersDead.Add(this.gameObject);
			this.gameObject.SetActive(false);
			// AIHandler.instance.CallRemoveFromList(this.gameObject.name);
		}
	}
	#endregion
}
