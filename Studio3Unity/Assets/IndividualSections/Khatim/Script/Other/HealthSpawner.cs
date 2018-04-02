using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour 
{
	#region Public Variables
	public Transform healthSpawnLocation;
	public GameObject healthPrefab;
	public int healthPooled;
	public List<GameObject> health;
	public float timer = 0;
	#endregion

	#region Private Variables
	private float maxTime = 5;
	public int index;
	#endregion

	#region Callbacks
	void Start () 
	{
		index = Random.Range(0, healthSpawnLocation.childCount);
		health = new List<GameObject>();
		for(int i = 0; i < healthPooled; i++)
		{
			GameObject healthObj = Instantiate(healthPrefab, healthSpawnLocation.GetChild(index).position, Quaternion.identity);
			healthObj.SetActive(false);
			health.Add(healthObj);
		}

		timer = maxTime;
	}
	
	void FixedUpdate () 
	{
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
		for(int i = 0; i < health.Count; i++)
		{
			if(!health[i].activeInHierarchy)
			{
				health[i].SetActive(true);
				break;
			}
		}
	}
	#endregion
}
