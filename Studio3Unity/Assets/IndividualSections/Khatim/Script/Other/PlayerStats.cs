using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;
    #endregion

    #region Private Variables
    private int hlth = 6;
    #endregion

    #region Callbacks
    void Start()
    {

    }

    void Update()
    {
        if (hlth <=0)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region Functions
    public void Damage()
    {
            hlth--;
            healthSprite[hlth].SetActive(false);
            Debug.Log("Damage");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && hlth < 6)
        {
            healthSprite[hlth].SetActive(true);
            hlth++;
            Destroy(other.gameObject);
        }
    }
    #endregion
}
