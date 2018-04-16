using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflinePlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthsprites;
    public GameObject player;
    public int health = 6;
    #endregion

    #region Private Variables
    #endregion

    #region Callbacks
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Damage();
        }

        if (health <= 0)
        {
            player.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }
    #endregion

    #region Functions
    public void Damage()
    {
        health--;
        healthsprites[health].SetActive(false);
    }

    public void Heal()
    {
        healthsprites[health].SetActive(true);
        health++;
    }
    #endregion
}
