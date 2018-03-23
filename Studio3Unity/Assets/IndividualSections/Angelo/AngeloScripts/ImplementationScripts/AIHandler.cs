using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour 
{
	#region Public variables
	public List<GameObject> aiToTrack;

	public AIHandler instance;
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
			for (int i = 0; i < aiToTrack.Count; i++)
			{
				if(aiToTrack[i] == gameObjectToRemove)
				{
					aiToTrack.Remove(aiToTrack[i]);
				}
			}
		}
	}
	#endregion

}
