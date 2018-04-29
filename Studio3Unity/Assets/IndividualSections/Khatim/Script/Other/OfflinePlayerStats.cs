using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
    [SerializeField]
    private int playerInt;
    #endregion

    #region Callbacks
    void Awake()
    {
        playerInt = players.Length;
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
    }
    void Start()
    {
        playerInt = players.Length;
    }

    void Update()
    {
        if (playerInt == 0)
        {
            SceneManager.LoadScene("Game_Over_Scene");
            AudioManager.auidoInstance.Playeffect(6);
        }

        if (healthP1 <= 0 && players[0].active)
        {
            players[0].SetActive(false);
            AudioManager.auidoInstance.Playeffect(6);
            playerInt--;
        }

        if (healthP2 <= 0 && players[1].active)
        {
            players[1].SetActive(false);
            AudioManager.auidoInstance.Playeffect(6);
            playerInt--;
        }
    }
    #endregion

    #region Functions
    public void DamageTaken(int damage, int target)
    {
        //For P1 Damage
        if (damage == 1 && target == 0)
        {
            for (int i = healthP1 - 1; i >= healthP1 - damage; i--)
            {
                healthSpritesP1[i].SetActive(false);
            }
            healthP1 -= damage;
        }

        if (damage == 2 && target == 0)
        {
            //if helath = 5
            for (int i = healthP1 - 1; i >= healthP1 - damage; i--)
            {
                healthSpritesP1[i].SetActive(false);
            }
            healthP1 -= damage;
        }

        //For P2 Damage
        if (damage == 1 && target == 1)
        {
            for (int i = healthP2 - 1; i >= healthP2 - damage; i--)
            {
                healthSpritesP2[i].SetActive(false);
            }
            healthP2 -= damage;
        }

        if (damage == 2 && target == 1)
        {
            //if helath = 5
            for (int i = healthP2 - 1; i >= healthP2 - damage; i--)
            {
                healthSpritesP2[i].SetActive(false);
            }
            healthP2 -= damage;
        }
    }

    public void HealthGained(int health)
    {
        if (health == 1)
        {
            for (int i = healthP1 - 1; i <= healthP1; i++)
            {
                healthSpritesP1[i].SetActive(true);
            }
            healthP1++;
        }

        if (health == 2)
        {
            for (int i = healthP2 - 1; i <= healthP2; i++)
            {
                healthSpritesP2[i].SetActive(true);
            }
            healthP2++;
        }
    }
    #endregion
}
