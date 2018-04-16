using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSpritesP1;
    public GameObject[] healthSpritesP2;
    public GameObject playerOne;
    public GameObject playerTwo;
    public int healthP1 = 6;
    public int healthP2 = 6;
    #endregion

    #region Private Variables
    #endregion

    #region Callbacks
    void Awake()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player1");
        playerTwo = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DamageP1();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            DamageP2();
        }

        /*if (healthP1 <= 0)
        {
            playerOne.SetActive(false);
        }*/

        if (healthP2 <= 0)
        {
            playerTwo.SetActive(false);
        }
    }
    #endregion

    #region Functions
    public void DamageP1()
    {
        healthP1--;
        healthSpritesP1[healthP1].SetActive(false);
    }

    public void DamageP2()
    {
        healthP2--;
        healthSpritesP2[healthP2].SetActive(false);
    }

    public void HealP1()
    {
        healthSpritesP1[healthP1].SetActive(true);
        healthP1++;
    }

    public void HealP2()
    {
        healthSpritesP2[healthP2].SetActive(true);
        healthP2++;
    }
    #endregion
}
