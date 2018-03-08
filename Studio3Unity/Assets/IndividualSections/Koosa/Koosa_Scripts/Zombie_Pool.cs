using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Pool : Photon.MonoBehaviour {

#region Public Variables
    public GameObject zombie;
    public int zombiesPooled;
    public Transform spawnPoint;
    public float spawnTime;  

    public static Zombie_Pool zombiePoolInstance;
    #endregion


#region Private Variables
    private List<GameObject> zombies;
    [SerializeField]
    private int spawnIndex;
    [SerializeField]
    private int rate;
    [SerializeField]
    private float maxTime = 20;
    [SerializeField]
    private float time;
    #endregion


#region Unity Functions
    private void Awake()
    {
        zombiePoolInstance = this;
        zombies = new List<GameObject>();
        time = 20;
    }

    private void Start()
    {
        for (int i = 0; i < zombiesPooled; i++)
        {
           GameObject zombieObject = Instantiate(zombie, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
           zombieObject.SetActive(false);
           zombies.Add(zombieObject);
        } 
    }

    private void Update()
    {
    //    time -= Time.deltaTime*rate;

    //    if (time <= spawnTime)
    //    { 
    //        spawnIndex = Random.Range(0, spawnPoint.childCount);
    //        Spawn(1);
    //        time = maxTime;
    //    }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Spawn(1);
        }
    }
    #endregion


#region My Functions
    public void Spawn(int zombiesToSpawn)
    {
        // for (int i = 0; i <zombies.Count ; i++)
        // {
        //     if (!zombies[i].activeInHierarchy)
        //     {
        //         zombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
        //         zombies[i].transform.rotation = transform.rotation;
        //         zombies[i].SetActive(true);
        //         break;
        //     }  
        // }

        for(int i = 0; i < zombiesToSpawn; i++)
        {
            RandomizeSpawn();
        }
    }

    private GameObject ZombieToSpawn()
    {
        for (int i=0; i < zombies.Count; i++)
        {
            if (!zombies[i].activeInHierarchy)
            {
                return zombies[i];
            }
        }
        return null;
    }

    private void RandomizeSpawn()
    {
        if(PhotonNetwork.isMasterClient)
        {
            spawnIndex = Random.Range(0, spawnPoint.childCount);
            int intToSend = spawnIndex;
            this.photonView.RPC("SetZombie", PhotonTargets.AllViaServer, intToSend.ToString());
        }
        else
        {
            Debug.Log("Don't do anything");
        }
    }

    [PunRPC]
    public void SetZombie(string myInt)
    {
        GameObject myZombie = ZombieToSpawn();
        myZombie.transform.position = spawnPoint.GetChild(int.Parse(myInt)).position;
        myZombie.transform.rotation = Quaternion.identity;
        myZombie.SetActive(true);
    }
}
#endregion
