using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpritesP1;
    public GameObject[] healthSpritesP2;
    public GameObject playerOne;
    public int healthP1 = 6;
    public int healthP2 = 6;
    #endregion

    #region Private Variables
    #endregion

    #region Callbacks
    void Awake()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }
    #endregion

    #region Functions
    public void DamageTaken()
    {

    }
    #endregion
}
