using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    private int regularDamage = 1;
    private int fallDamage = 2;
    private int heavyDamage = 6;
    private OfflinePlayerStats offlinePlyStats;

    #endregion

    #region Callbacks
    void Start()
    {
        offlinePlyStats = GetComponent<OfflinePlayerStats>();
    }
    void Update()
    {

    }
    #endregion

    #region Functions
    public void DamageGiven(int damage)
    {
        if (damage == 1)
        {
            Debug.Log("Regular Damage");
        }

        if (damage == 2)
        {
            Debug.Log("Heavy Damage");
        }
    }
    #endregion
}
