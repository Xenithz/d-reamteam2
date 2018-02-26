using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    #region Public Variables
    public Text healthText;
    #endregion

    #region Private Variables
    public PlayerStats ply;
    #endregion

    #region Callbacks
    void Start()
    {
        healthText.text = ply.health.ToString();
    }
    void FixedUpdate()
    {

    }
    #endregion

    #region Functions

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && ply.health < 6)
        {
            ply.health++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Damage" && Input.GetKey(KeyCode.E) && ply.health > 0)
        {
            ply.health--;
            //ply.healthSprite[0].SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
    #endregion
}
