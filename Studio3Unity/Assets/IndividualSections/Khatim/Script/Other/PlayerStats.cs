using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;

    #endregion

    #region Private Variables
    #endregion

    #region Callbacks
    void Awake()
    {
        healthSprite = GameManagerBase.instance.playerHp;
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
        healthSprite[gameObject.GetComponent<Character_Controller>().hp].SetActive(false);        
        Debug.Log("Damage");
    }

    public void CallDmg()
    {
        photonView.RPC("Damage2", PhotonTargets.All);
    }
    #endregion
}
