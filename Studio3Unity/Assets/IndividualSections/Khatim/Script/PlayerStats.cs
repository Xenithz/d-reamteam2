using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Private Variables
    private HealthSystem hlthSys;
    #endregion

    #region Callbacks
    void Start()
    {
        hlthSys = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (hlthSys.hlth <=0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Damage();
            other.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Functions
    void Damage()
    {
            hlthSys.hlth--;
            hlthSys.healthSprite[hlthSys.hlth].SetActive(false);
            Debug.Log("Damage");
    }
    #endregion
}
