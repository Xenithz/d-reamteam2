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
    private int regularDamage = 1;
    private int fallDamage = 2;
    private int heavyDamage = 6;

    #endregion

    #region Callbacks
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (healthP1 <= 0)
        {
            players[0].SetActive(false);
        }

        if (healthP2 <= 0)
        {
            players[1].SetActive(false);
        }
    }
    #endregion

    #region Functions
    public void DamageTaken(int damage)
    {
        //For P1 Health
        if (damage == 1)
        {
            for (int i = healthP1 - 1; i >= healthP1 - damage; i--)
            {
                healthSpritesP1[i].SetActive(false);
            }
            healthP1 -= damage;
        }

        if (damage == 2)
        {
            //if helath = 5
            for (int i = healthP1 - 1; i >= healthP1 - damage; i--)
            {
                healthSpritesP1[i].SetActive(false);
            }
            healthP1 -= damage;
        }
    }

    /*public void DamageTakenP2(int damage)
    {
        //For P2 Health
        if (damage == 1)
        {
            for (int i = healthP2 - 1; i >= healthP2 - damage; i--)
            {
                healthSpritesP2[i].SetActive(false);
            }
            healthP2 -= damage;
        }

        if (damage == 2)
        {
            //if helath = 5
            for (int i = healthP2 - 1; i >= healthP2 - damage; i--)
            {
                healthSpritesP2[i].SetActive(false);
            }
            healthP2 -= damage;
        }
    }*/
    #endregion
}
