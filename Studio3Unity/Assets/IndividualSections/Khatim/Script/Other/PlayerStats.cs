using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;

    public static PlayerStats instance;
    #endregion

    #region Private Variables
    private int hlth = 6;
    #endregion

    #region Callbacks
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp<=0)
        {
            Debug.Log("Dead");
        }

        //Testing purposes
        if(Input.GetKeyDown(KeyCode.L))
        {
            Damage();
        }
    }
    #endregion

    #region Functions
    public void Damage()
    {
            GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp--;
            healthSprite[GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp].SetActive(false);
            Debug.Log("Damage");
    }
    #endregion
}
