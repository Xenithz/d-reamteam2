using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : Photon.MonoBehaviour 
{
	#region Public variables
	public List<GameObject> aiToTrack;

	public static AIHandler instance;
	#endregion

	#region Private variables
		
	#endregion

	#region Unity callbacks
	private void Awake()
	{
		instance = this;
	}
	#endregion

	#region My functions
	public void CallRefreshList()
	{
		this.photonView.RPC("RefreshList", PhotonTargets.All);	
	}
	
	public void CallRemoveFromList(string gameObjectName)
	{
		this.photonView.RPC("RemoveFromList", PhotonTargets.All, gameObjectName);
	}
	#endregion

	#region My RPCs
	[PunRPC]
	public void RefreshList()
	{
		aiToTrack = Zombie_Pool.zombiePoolInstance.activeZombies;
	}

	[PunRPC]
	public void RemoveFromList(string objectName)
	{
		GameObject gameObjectToRemove = GameObject.Find(objectName);
		if(aiToTrack.Contains(gameObjectToRemove))
		{
			aiToTrack.Remove(gameObjectToRemove);
		}
		else
		{
			Debug.Log("Object not present");
		}
	}
	#endregion

}
