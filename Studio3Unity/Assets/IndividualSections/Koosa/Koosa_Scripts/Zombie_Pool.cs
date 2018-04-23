using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Pool : Photon.MonoBehaviour 
{

#region Public Variables
    public GameObject zombie;
    public GameObject easyZombie;
    public GameObject mediumZombie;
    public GameObject hardZombie;
    public int zombiesPooled;
    public int easyZombiesPooled;
    public int mediumZombiesPooled;
    public int hardZombiesPooled;

    public Transform spawnPoint;
    public float spawnTime;  

    public static Zombie_Pool zombiePoolInstance;
    public List<GameObject> zombies;
    public List<GameObject> easyZombies;
    public List<GameObject> mediumZombies;
    public List<GameObject> hardZombies;
    public List<GameObject> activeZombies;

    public bool zombiesHaveSpawned;

    public bool stopSpawning;
    #endregion


#region Private Variables
    
    [SerializeField]
    private int spawnIndex;
    [SerializeField]
    private int rate;
    [SerializeField]
    private float maxTime = 20;
    [SerializeField]
    private float time;

    private bool myFlag;

    private bool spawnFlag;
    #endregion


#region Unity Functions
    private void Awake()
    {
        zombiePoolInstance = this;
        zombies = new List<GameObject>();
        easyZombies= new List<GameObject>();
        mediumZombies= new List<GameObject>();
        hardZombies= new List<GameObject>();
        time = 20;
        myFlag = true;
        spawnFlag = true;
        zombiesHaveSpawned = false;
        stopSpawning = false;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && PhotonNetwork.isMasterClient)
        {
            Spawn(GameManagerBase.instance.amountOfEasyToSpawn,GameManagerBase.instance.amountOfMediumToSpawn,GameManagerBase.instance.amountOfHardToSpawn);
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            Spawn(1,0,0);
        }

        //Spawn zombies for Pool
        if(PhotonNetwork.connected && myFlag == true && PhotonNetwork.room.PlayerCount >= 1 && PhotonNetwork.isMasterClient)
        {
            Initialize();
        }
    }
    #endregion


#region My Functions
    public void Spawn(int zombiesToSpawn, int mediumZombiesToSpawn, int hardZombiesToSpawn)
    {
        stopSpawning = true;
        Debug.Log("spawn called");
        for(int i = 0; i < zombiesToSpawn; i++)
        {
            spawnFlag = true;
            RandomizeSpawn("easy");
        }
        for(int i = 0; i < mediumZombiesToSpawn; i++)
        {
            spawnFlag = true;
            RandomizeSpawn("medium");
        }
        for(int i = 0; i < hardZombiesToSpawn; i++)
        {
            spawnFlag = true;
            RandomizeSpawn("hard");
        }
    }

    IEnumerator Spawn2(int zombiesToSpawn, int mediumZombiesToSpawn, int hardZombiesToSpawn)
    {
        stopSpawning = true;
        Debug.Log("spawn called");
        for(int i = 0; i < zombiesToSpawn; i++)
        {
            yield return new WaitForSeconds(4);
            spawnFlag = true;
            RandomizeSpawn("easy");
        }
        for(int i = 0; i < mediumZombiesToSpawn; i++)
        {
            yield return new WaitForSeconds(4);
            spawnFlag = true;
            RandomizeSpawn("medium");
        }
        for(int i = 0; i < hardZombiesToSpawn; i++)
        {
            yield return new WaitForSeconds(4);
            spawnFlag = true;
            RandomizeSpawn("hard");
        }   
    }

    public void CallSpawn2(int zombiesToSpawn, int mediumZombiesToSpawn, int hardZombiesToSpawn)
    {
        StartCoroutine(Spawn2(zombiesToSpawn, mediumZombiesToSpawn, hardZombiesToSpawn));
    }

    public void Initialize()
    {
        for (int i = 0; i < zombiesPooled; i++)
        {
           Debug.Log("Starting to easy pool zombies");
           GameObject zombieObject = PhotonNetwork.Instantiate(zombie.name, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity, 0);
           zombieObject.name = "balllicker" + i;
           zombies.Add(zombieObject);
        } 
        for (int i = 0; i < mediumZombiesPooled; i++)
        {
           Debug.Log("Starting to pool medium zombies");
           GameObject zombieObject = PhotonNetwork.Instantiate(mediumZombie.name, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity, 0);
           zombieObject.name = "medium balllicker" + i;
           mediumZombies.Add(zombieObject);
        }
        for (int i = 0; i < hardZombiesPooled; i++)
        {
           Debug.Log("Starting to pool hard zombies");
           GameObject zombieObject = PhotonNetwork.Instantiate(hardZombie.name, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity, 0);
           zombieObject.name = "hard balllicker" + i;
           hardZombies.Add(zombieObject);
        }
        zombiesHaveSpawned = true;
        myFlag = false;
    }
    
    private GameObject ZombieToSpawn(string type)
    {
        if(type == "easy")
        {
            for (int i=0; i < zombies.Count; i++)
            {
                if (!zombies[i].activeInHierarchy)
                {
                    return zombies[i];
                }
            }
        }
        else if(type == "medium")
        {
            for (int i=0; i < mediumZombies.Count; i++)
            {
                if (!mediumZombies[i].activeInHierarchy)
                {
                    return mediumZombies[i];
                }   
            }
        }
        else if(type == "hard")
        {
            for (int i=0; i < hardZombies.Count; i++)
            {
                if (!hardZombies[i].activeInHierarchy)
                {
                    return hardZombies[i];
                }   
            }
        }
        return null;
    }

    private void RandomizeSpawn(string type)
    {
        if(PhotonNetwork.isMasterClient && spawnFlag == true)
        {
            spawnIndex = Random.Range(0, spawnPoint.childCount);
            int intToSend = spawnIndex;
            this.photonView.RPC("SetZombie", PhotonTargets.AllViaServer, intToSend.ToString(), type);
            Debug.Log("Going to active zombies now");
        }
        else
        {
            Debug.Log("Don't do anything");
        }
    }

    [PunRPC]
    public void SetZombie(string myInt, string type)
    {
        GameObject myZombie = ZombieToSpawn(type);
        myZombie.transform.position = new Vector3(spawnPoint.GetChild(int.Parse(myInt)).position.x, spawnPoint.GetChild(int.Parse(myInt)).position.y + 3f, spawnPoint.GetChild(int.Parse(myInt)).position.z);
        myZombie.transform.rotation = Quaternion.identity;
        activeZombies.Add(myZombie);
        //AIHandler.instance.CallRefreshList();
        myZombie.SetActive(true);
        spawnFlag = false;
        GameManagerBase.instance.myGameState = GameStates.Playing;
        stopSpawning = false;
    }

    public void CallRemoveZombie(string objectToPass)
    {
        this.photonView.RPC("RemoveZombieFromActive", PhotonTargets.All, objectToPass);
    }

    [PunRPC]
    public void RemoveZombieFromActive(string objectNameToRemove)
    {
        GameObject objectToRemove = GameObject.Find(objectNameToRemove);
        if(activeZombies.Contains(objectToRemove))
		{
			for(int i = 0; i < activeZombies.Count; i++)
            {
                if(activeZombies[i] ==  objectToRemove)
                {
                    activeZombies.Remove(objectToRemove);
                }
            }
		}
		else
		{
			Debug.Log("Object not present");
		}
    }
}
#endregion
