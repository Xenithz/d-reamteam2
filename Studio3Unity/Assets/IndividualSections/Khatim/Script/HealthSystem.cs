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
    private int Health;
    #endregion

    #region Functions
    void Start()
    {
        Health = 6;
        healthText.text = Health.ToString();

    }
    void FixedUpdate()
    {

    }
    #endregion


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && Health < 6)
        {
            Health++;
            healthText.text = Health.ToString();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Damage" && Input.GetKey(KeyCode.E) && Health > 0)
        {
            Health--;
            healthText.text = Health.ToString();
            other.gameObject.SetActive(false);
        }
    }
}
