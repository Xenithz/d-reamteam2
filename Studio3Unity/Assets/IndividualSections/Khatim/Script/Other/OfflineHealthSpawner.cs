﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineHealthSpawner : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpawnLocation;
    public GameObject healthPrefab;
    public List<GameObject> health;
    public float timer = 0;
    public int index;
    #endregion

    #region Private Variables
    private float maxTime = 30.0f;

    #endregion

    #region Callbacks
    void Awake()
    {
        timer = maxTime;
        healthSpawnLocation = GameObject.FindGameObjectsWithTag("HealthSpawn");

        for (int i = 0; i < healthSpawnLocation.Length; i++)
        {
            GameObject healthObj = Instantiate(healthPrefab, healthSpawnLocation[i].transform.position, Quaternion.identity);
            healthPrefab.SetActive(false);
            Debug.Log("Health Spawned");
            health.Add(healthObj);
        }
    }

    void FixedUpdate()
    {
        index = Random.Range(0, health.Count);
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnHealth();
            timer = maxTime;
        }
    }
    #endregion

    #region Functions
    void SpawnHealth()
    {
        if (!health[index].activeInHierarchy)
        {
            Debug.Log("Health Active");
            health[index].SetActive(true);
        }
    }
    #endregion
}
