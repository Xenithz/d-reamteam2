using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpawnLocation;
    public GameObject healthPrefab;
    public List<GameObject> health;
    public float timer;
    public string index;
    #endregion

    #region Private Variables
    public float maxTime;
    #endregion

    #region Callbacks
    void Awake()
    {
        maxTime = 5f;
        timer = maxTime;
        healthSpawnLocation = GameObject.FindGameObjectsWithTag("HealthSpawn");

        for (int i = 0; i < healthSpawnLocation.Length; i++)
        {
            GameObject healthObj = PhotonNetwork.Instantiate(healthPrefab.name, healthSpawnLocation[i].transform.position, Quaternion.identity, 0);
            Debug.Log("spawned hp");
            health.Add(healthObj);
        }
    }

    void FixedUpdate()
    {
        if (PhotonNetwork.isMasterClient)
        {
            index = Random.Range(0, health.Count).ToString();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                this.photonView.RPC("SpawnHealth", PhotonTargets.All, index);
                timer = maxTime;
            }

            // if(Input.GetKeyDown(KeyCode.M))
            // {
            // 	this.photonView.RPC("SpawnHealth", PhotonTargets.All, index);
            // 	timer = maxTime;
            // }
        }
    }
    #endregion

    #region Functions
    [PunRPC]
    void SpawnHealth(string myInt)
    {
        Debug.Log("called enable hp func");
        int convertedInt = int.Parse(myInt);
        for (int i = 0; i < health.Count; i++)
        {
            if (!health[i].activeInHierarchy)
            {
                health[convertedInt].SetActive(true);
                break;
            }
        }
    }
    #endregion
}
