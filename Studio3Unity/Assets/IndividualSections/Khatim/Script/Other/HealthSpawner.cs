using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour 
{
	#region Public Variables
	public Transform[] healthSpawnLocation;
	public GameObject healthPrefab;
	#endregion

	#region Private Variables
	private int index;
	public float timer = 0;
	private float maxTime = 5;
	#endregion

	#region Callbacks
	void Start () 
	{
		index = Random.Range(0, healthSpawnLocation.Length);
	}
	
	void Update () 
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			//SpawnHealth();
			Debug.Log("Health Spawned");
			timer = maxTime;
		}
	}
	#endregion

	#region Functions
	/*void SpawnHealth()
	{
		Instantiate(healthPrefab,,Quaternion.identity);
	}*/
	#endregion
}
