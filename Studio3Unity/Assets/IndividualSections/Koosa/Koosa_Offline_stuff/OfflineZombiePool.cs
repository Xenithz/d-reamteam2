using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

 
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
    public int roundWait;
    public Text roundText;
    #endregion

 #region Unity Functions
     private void Awake()
    {
        roundText.text = round.ToString();
        roundText.enabled = true;
        zombies = new List<GameObject>();
        easyZombies=new List<GameObject>();
        mediumZombies=new List<GameObject>();
        hardZombies=new List<GameObject>();
        PoolZombies(easyZombie,easyZombiesPooled,easyZombies);
        PoolZombies(mediumZombie,mediumZombiesPooled,mediumZombies);
        PoolZombies(hardZombie,hardZombiesPooled,hardZombies);
        StartCoroutine(SpawnnerPool());
    }
    private void Update()
    {
        spawnIndex = Random.Range(0, spawnPoint.childCount);
    }
    #endregion
 
    #region My Functions
 private void PoolZombies(GameObject zombieToPool,int zombiesPooled,List<GameObject> zombielist)
 {
    for(int i = 0; i < zombiesPooled; i++)
    {
        GameObject zombieobject = Instantiate(zombieToPool, spawnPoint.GetChild(spawnIndex).position, Quaternion.identity);
        zombieobject.SetActive(false);
        zombielist.Add(zombieobject);
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
                Instantiate(easyZombie, (spawnPoint.GetChild(spawnIndex).position + new Vector3(0,2,0)), Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            for (int i = 0; i < mediumZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
                Instantiate(mediumZombie,  (spawnPoint.GetChild(spawnIndex).position + new Vector3(0,2,0)), Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            for (int i = 0; i < hardZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
                Instantiate(hardZombie, (spawnPoint.GetChild(spawnIndex).position + new Vector3(0,2,0)), Quaternion.identity);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds(roundWait);
            round++;
            easyZombieToSpawn+=5;
            mediumZombieToSpawn+=2;
            hardZombieToSpawn+=1;
        }
    }
    IEnumerator SpawnnerPool ()
   {
         yield return new WaitForSeconds (startWait);
        while (true)
        {
           for (int i = 0; i < easyZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
            if (!easyZombies[i].activeInHierarchy)
        {
            easyZombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
            easyZombies[i].transform.rotation = transform.rotation;
            easyZombies[i].SetActive(true);
            
        }
            yield return new WaitForSeconds (spawnWait);
            }
           yield return new WaitForSeconds (waveWait);

           for (int i = 0; i < mediumZombieToSpawn; i++)
           {
            spawnIndex = Random.Range(0, spawnPoint.childCount);
            if (!mediumZombies[i].activeInHierarchy)
        {
            mediumZombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
            mediumZombies[i].transform.rotation = transform.rotation;
            mediumZombies[i].SetActive(true);
            
        }
            yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            for (int i = 0; i < hardZombieToSpawn; i++)
            {
                spawnIndex = Random.Range(0, spawnPoint.childCount);
            if (!hardZombies[i].activeInHierarchy)
        {
            hardZombies[i].transform.position = spawnPoint.GetChild(spawnIndex).position;
            hardZombies[i].transform.rotation = transform.rotation;
            hardZombies[i].SetActive(true);
           
        }
           yield return new WaitForSeconds (spawnWait);
           }
           
           round++;
           easyZombieToSpawn+=5;
           mediumZombieToSpawn+=2;
            hardZombieToSpawn+=1;
        }
}
}
#endregion