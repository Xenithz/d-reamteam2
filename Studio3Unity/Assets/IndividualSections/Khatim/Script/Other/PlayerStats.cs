using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;

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

        }
    }
    #endregion

    #region Functions
    public void Damage()
    {
        //gameObject.GetComponent<Character_Controller>().hp--;
        GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp--;
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
