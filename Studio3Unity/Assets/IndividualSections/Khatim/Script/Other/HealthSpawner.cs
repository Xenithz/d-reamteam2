using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour 
{
	#region Public Variables
	public GameObject[] healthSpawnLocation;
	public GameObject healthPrefab;
	public List<GameObject> health;
	public float timer = 0;
	public int index;
	#endregion

	#region Private Variables
	private float maxTime = 5;
	#endregion

	#region Callbacks
	void Awake () 
	{
		timer = maxTime;
		healthSpawnLocation = GameObject.FindGameObjectsWithTag("HealthSpawn");

		for(int i = 0; i < healthSpawnLocation.Length; i++)
		{
			GameObject healthObj = Instantiate(healthPrefab, healthSpawnLocation[i].transform.position, Quaternion.identity);
			healthPrefab.SetActive(false);
			health.Add(healthObj);
		}
		#region Commented Code
		//Instantiate(healthPrefab, healthSpawnLocation.)
		/*index = Random.Range(0, healthSpawnLocation.childCount);
		health = new List<GameObject>();
		for(int i = 0; i < healthPooled; i++)
		{
			GameObject healthObj = Instantiate(healthPrefab, healthSpawnLocation.GetChild(index).position, Quaternion.identity);
			healthObj.SetActive(false);
			health.Add(healthObj);
		}*/
		#endregion
	}
	
	void FixedUpdate () 
	{
		index = Random.Range(0, health.Count);
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			SpawnHealth();
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
				health[index].SetActive(true);
				break;
			}
		}
	}
	#endregion
}
