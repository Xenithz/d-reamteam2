using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    private int regularDamage; //1 Point of Damage.
    private int fallDamage; //2 Points of Damage.
    private int heavyDamage; //6 Points of Damage.
    private OfflinePlayerStats offlinePlyStats;

    #endregion

    #region Callbacks*/
    void Start()
    {
        offlinePlyStats = GetComponent<OfflinePlayerStats>();
    }
    void Update()
    {

    }
    #endregion

    #region Functions
    public virtual void DamageGiven()
    {

    }
    #endregion
}
