using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;

    public GameObject[] players;

    #endregion

    #region Private Variables
    private Character_Controller myChar;
    #endregion

    #region Callbacks
    void Awake()
    {
        healthSprite = GameManagerBase.instance.playerHp;
        myChar = GetComponent<Character_Controller>();
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if(PhotonNetwork.connected)
        {
        if (GetComponent<Character_Controller>().hp <= 0)
        {
            Debug.Log("Dead");
        }

        //Testing purposes
        if (Input.GetKeyDown(KeyCode.L))
        {
            Damage();
        }

        // if(Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     players[1].GetComponent<PlayerStats>().CallDmg();
        // }

        if(myChar.hp == 0)
        {
            healthSprite[0].SetActive(false);
            healthSprite[1].SetActive(false);
            healthSprite[2].SetActive(false);
            healthSprite[3].SetActive(false);
            healthSprite[4].SetActive(false);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 1)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(false);
            healthSprite[2].SetActive(false);
            healthSprite[3].SetActive(false);
            healthSprite[4].SetActive(false);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 2)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(true);
            healthSprite[2].SetActive(false);
            healthSprite[3].SetActive(false);
            healthSprite[4].SetActive(false);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 3)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(true);
            healthSprite[2].SetActive(true);
            healthSprite[3].SetActive(false);
            healthSprite[4].SetActive(false);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 4)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(true);
            healthSprite[2].SetActive(true);
            healthSprite[3].SetActive(true);
            healthSprite[4].SetActive(false);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 5)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(true);
            healthSprite[2].SetActive(true);
            healthSprite[3].SetActive(true);
            healthSprite[4].SetActive(true);
            healthSprite[5].SetActive(false);
        }
        else if(myChar.hp == 6)
        {
            healthSprite[0].SetActive(true);
            healthSprite[1].SetActive(true);
            healthSprite[2].SetActive(true);
            healthSprite[3].SetActive(true);
            healthSprite[4].SetActive(true);
            healthSprite[5].SetActive(true);
        }

        }
    }
    #endregion

    #region Functions
    public void Damage()
    {
        gameObject.GetComponent<Character_Controller>().hp--;
        healthSprite[gameObject.GetComponent<Character_Controller>().hp].SetActive(false);        
        Debug.Log("Damage");
    }

    [PunRPC]
    public void Damage2()
    {
        gameObject.GetComponent<Character_Controller>().hp--;
        //healthSprite[gameObject.GetComponent<Character_Controller>().hp].SetActive(false);        
        Debug.Log("Damage");
    }

    public void CallDmg()
    {
        photonView.RPC("Damage2", PhotonTargets.All);
    }
    #endregion
}
