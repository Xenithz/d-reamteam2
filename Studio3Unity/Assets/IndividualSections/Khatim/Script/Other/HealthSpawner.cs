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
	public float timer = 0;
	private float maxTime = 5;
	private int index;
	#endregion

	#region Callbacks
	void Start () 
	{
		timer = maxTime;
	}
	
	void Update () 
	{
		index = Random.Range(0, healthSpawnLocation.Length);
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			SpawnHealth();
			Debug.Log("Health Spawned");
			timer = maxTime;
		}
	}
	#endregion

	#region Functions
	void SpawnHealth()
	{
		Instantiate(healthPrefab,healthSpawnLocation[index].position,Quaternion.identity);
	}
	#endregion
}
