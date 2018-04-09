using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 
 public class OfflineZombiePool : MonoBehaviour {
 
 #region Public Variables
     public List<GameObject> zombies;
     public GameObject zombie;
     public float spawntime;
     public int zombiespooled;
     public Transform spawnpoint;
     public int spawnindex;
     public float time;
     public float timetoincrease;
     #endregion

 #region Unity Functions
     private void Awake()
     {
        zombies = new List<GameObject>();
     }
     private void Start()
     {
       //InvokeRepeating("spawn", spawntime, spawntime);
        for(int i = 0; i < zombiespooled; i++)
        {
            GameObject zombieobject = Instantiate(zombie, spawnpoint.GetChild(spawnindex).position, Quaternion.identity);
            zombieobject.SetActive(false);
            zombies.Add(zombieobject);
        }
     }
     private void Update()
     {
        time -=Time.deltaTime*2;
         
        if (time <= 10)
        {
            spawn();
            time = timetoincrease;
        }
        spawnindex = Random.Range(0, spawnpoint.childCount);
         
     }
     #endregion
 
 #region My Functions
    private void spawn()
    {
        for (int i = 0; i <zombies.Count ; i++)
        {
            if (!zombies[i].activeInHierarchy)
            {
                zombies[i].transform.position = spawnpoint.GetChild(spawnindex).position;
                zombies[i].transform.rotation = transform.rotation;
                zombies[i].SetActive(true);
                break;
            }
        }
    }
 }
 #endregion