using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpritesP1;
    public GameObject[] healthSpritesP2;
    public GameObject[] players;
    public int healthP1 = 6;
    public int healthP2 = 6;
    #endregion

    #region Private Variables
    private ZombieStats zomStats;
    private int regularDamage = 1;
    private int fallDamage = 2;
    private int heavyDamage = 6;

    #endregion

    #region Callbacks
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        zomStats = GetComponent<ZombieStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            DamageTaken(4);
        }

        if (healthP1 <= 0)
        {
            players[0].SetActive(false);
        }
    }
    #endregion

    #region Functions
    public void DamageTaken(int damage)
    {
        if (damage == 1)
        {
            healthP1 -= regularDamage;
            healthSpritesP1[healthP1].SetActive(false);
            Debug.Log("Regular Daamge");
        }

        if (damage == 6)
        {
            Debug.Log("Heavy Damage");
        }

        //Testing
        if (damage == 4)
        {
            /*healthP1 -= fallDamage;
            healthSpritesP1[healthP1].SetActive(false);*/
            
            //if helath = 5
            for (int i = healthP1 - 1; i >= healthP1 - damage; i--)
            {
                healthSpritesP1[i].SetActive(false);
            }
        }
    }
    #endregion
}
