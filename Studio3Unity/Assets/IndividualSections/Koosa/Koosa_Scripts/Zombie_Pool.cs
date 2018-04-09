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
    public List<GameObject> activeEasyZombies;
    public List<GameObject> activeMediumZombies;
    public List<GameObject> activeHardZombies;

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
        if(Input.GetKeyDown(KeyCode.P))
        {
            Spawn(1);
        }

        //Spawn zombies for Pool
        if(PhotonNetwork.connected && myFlag == true && PhotonNetwork.room.PlayerCount >= 1 && PhotonNetwork.isMasterClient)
        {
            Initialize();
        }
    }
    #endregion


#region My Functions
    public void Spawn(int zombiesToSpawn)
    {
        stopSpawning = true;
        Debug.Log("spawn called");
        for(int i = 0; i < zombiesToSpawn; i++)
        {
            spawnFlag = true;
            RandomizeSpawn();
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < zombiesPooled; i++)
        {
            Debug.Log("Starting to pool zombies");
           GameObject zombieObject = PhotonNetwork.Instantiate(zombie.name, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity, 0);
           zombieObject.name = "balllicker" + i;
        } 
        zombiesHaveSpawned = true;
        myFlag = false;
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
        if(PhotonNetwork.isMasterClient && spawnFlag == true)
        {
            spawnIndex = Random.Range(0, spawnPoint.childCount);
            int intToSend = spawnIndex;
            this.photonView.RPC("SetZombie", PhotonTargets.AllViaServer, intToSend.ToString());
            Debug.Log("Going to active zombies now");
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
        activeZombies.Add(myZombie);
        AIHandler.instance.CallRefreshList();
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
			activeZombies.Remove(objectToRemove);
		}
		else
		{
			Debug.Log("Object not present");
		}
    }
}
#endregion
