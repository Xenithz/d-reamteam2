using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Pool : MonoBehaviour {

#region Public Variables
    public List<GameObject> zombies;
    public GameObject zombie;
    public float spawntime;
    public int zombiespooled;
    
#endregion

    private void Awake()
    {
        
         zombies = new List<GameObject>();
    }
    private void Start()
    {
        InvokeRepeating("spawn", spawntime, spawntime);
        for(int i = 0; i < zombiespooled; i++)
        {
            GameObject zombieobject = Instantiate(zombie, transform.position, Quaternion.identity);
            zombieobject.SetActive(true);
            zombies.Add(zombieobject);
        }




    }
    private void Update()
    {
        
    }



    private void spawn()
    {
        for (int i = 0; i <zombies.Count ; i++)
        {
            if (!zombies[i].activeInHierarchy)
            {

                zombies[i].transform.position = transform.position;
                zombies[i].transform.rotation = transform.rotation;
                zombies[i].SetActive(true);
                
                break;

            }




            
        }




    }
}
