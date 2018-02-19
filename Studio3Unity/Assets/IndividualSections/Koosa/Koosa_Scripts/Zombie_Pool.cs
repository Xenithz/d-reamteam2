using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Pool : MonoBehaviour {

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
        
        for(int i = 0; i < zombiespooled; i++)
        {
            GameObject zombieobject = Instantiate(zombie, spawnpoint.GetChild(spawnindex).position, Quaternion.identity);
            zombieobject.SetActive(true);
            zombies.Add(zombieobject);
        }




    }
    private void Update()
    {
        time -=Time.deltaTime;
        
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
                

            }




            
        }




    }

}
#endregion
