using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    //TODO: Make flags for RPCs as this system makes use of triggers/collisions

    #region Public Variables
    //public Text healthText;
    public GameObject[] healthSprite;
    public int hlth;

    #endregion

    #region Private Variables

    #endregion

    #region Callbacks
    void Start()
    {
        //healthText.text = hlth.ToString();
        hlth = 6;
    }
    void FixedUpdate()
    {

    }
    #endregion

    #region Functions

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && hlth < 6)
        {
            healthSprite[hlth].SetActive(true);
            hlth++;
            //healthText.text = hlth.ToString();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Damage" && Input.GetKey(KeyCode.E) && hlth > 0)
        {
            hlth--;
            healthSprite[hlth].SetActive(false);
            //healthText.text = hlth.ToString();
            other.gameObject.SetActive(false);
        }
    }
    #endregion
}
