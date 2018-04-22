using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 
 public class OfflineZombiePool : MonoBehaviour {
 
 #region Public Variables
    public List<GameObject> zombies;
    public List<GameObject> easyZombies;
    public List<GameObject> mediumZombies;
    public List<GameObject> hardZombies;

    public int easyZombiesPooled;
    public int mediumZombiesPooled;
    public int hardZombiesPooled;
    public int easyZombieToSpawn;
    public int mediumZombieToSpawn;
    public int hardZombieToSpawn;

    public GameObject zombie;
    public GameObject easyZombie;
    public GameObject mediumZombie;
    public GameObject hardZombie;

    public float spawnTime;
    public int zombiesPooled;
    public Transform spawnPoint;
    public int spawnIndex;

    public float startWait;
    public float waveWait;
    public float spawnWait;
    public int round;
    #endregion

 #region Unity Functions
     private void Awake()
    {
        zombies = new List<GameObject>();
        easyZombies=new List<GameObject>();
        mediumZombies=new List<GameObject>();
        hardZombies=new List<GameObject>();
        PoolZombies(easyZombie,easyZombiesPooled);
        PoolZombies(mediumZombie,mediumZombiesPooled);
        PoolZombies(hardZombie,hardZombiesPooled);
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        spawnIndex = Random.Range(0, spawnPoint.childCount);
    }
    #endregion
 
    #region My Functions
 private void PoolZombies(GameObject zombieToPool,int zombiesPooled)
 {
    for(int i = 0; i < zombiesPooled; i++)
    {
        GameObject zombieobject = Instantiate(zombieToPool, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
        zombieobject.SetActive(false);
        zombies.Add(zombieobject);
    }
 }
private void spawn()
{
    for (int i = 0; i <zombies.Count ; i++)
    {
        if (!zombies[i].activeInHierarchy)
        {
            zombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
            zombies[i].transform.rotation = transform.rotation;
            zombies[i].SetActive(true);
            break;
        }
    }
}
 IEnumerator Spawnner ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < easyZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
                Instantiate(easyZombie, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            for (int i = 0; i < mediumZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
                Instantiate(mediumZombie, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            for (int i = 0; i < hardZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
                Instantiate(hardZombie, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            round++;
            easyZombieToSpawn+=5;
            mediumZombieToSpawn+=3;
            hardZombieToSpawn+=1;
        }
    }
}
#endregion