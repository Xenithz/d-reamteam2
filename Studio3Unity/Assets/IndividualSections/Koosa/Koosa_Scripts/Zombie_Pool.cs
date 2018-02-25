using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Pool : MonoBehaviour {

#region Public Variables
    public GameObject zombie;
    public int zombiesPooled;
    public Transform spawnPoint;
    public float spawnTime;  
    #endregion


#region Private Variables
    private List<GameObject> zombies;
    private int spawnIndex;
    private int rate = 2;
    [SerializeField]
    private float maxTime = 20;
    [SerializeField]
    private float time = 20;
    #endregion


#region Unity Functions
    private void Awake()
    {  
      zombies = new List<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < zombiesPooled; i++)
        {
           GameObject zombieObject = Instantiate(zombie, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
           zombieObject.SetActive(true);
           zombies.Add(zombieObject);
        }
    }

    private void Update()
    {
       time -=Time.deltaTime*rate;
        
       if (time <=spawnTime )
        { 
           Spawn();
           time = maxTime;
       }
        spawnIndex = Random.Range(0, spawnPoint.childCount);
    }
    #endregion


#region My Functions
    private void Spawn()
    {
        for (int i = 0; i <zombies.Count ; i++)
        {
            if (!zombies[i].activeInHierarchy)
            {
                zombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
                zombies[i].transform.rotation = transform.rotation;
                zombies[i].SetActive(true);             
            }  
        }
    }
}
#endregion
