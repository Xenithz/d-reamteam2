using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpritesP1;
    public GameObject[] healthSpritesP2;
    public GameObject player;
    public int healthP1 = 6;
    public int healthP2 = 6;
    #endregion

    #region Private Variables
    private ZombieStats zomStats;
    #endregion

    #region Callbacks
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        zomStats = GetComponent<ZombieStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            DamageTaken(1);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            DamageTaken(2);
        }
    }
    #endregion

    #region Functions
    public void DamageTaken(int damage)
    {
        if (damage == 1)
        {
            zomStats.DamageGiven(1);
        }

        if (damage == 2)
        {
            zomStats.DamageGiven(2);
        }
    }
    #endregion
}
