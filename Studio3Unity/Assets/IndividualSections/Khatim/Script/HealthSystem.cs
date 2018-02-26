using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    #region Public Variables
    public Text healthText;
    public int hlth;
    public GameObject[] healthSprite;
    #endregion

    #region Private Variables
    #endregion

    #region Callbacks
    void Start()
    {
        healthText.text = hlth.ToString();
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
            healthText.text = hlth.ToString();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Damage" && Input.GetKey(KeyCode.E) && hlth > 0)
        {
            hlth--;
            healthSprite[hlth].SetActive(false);
            healthText.text = hlth.ToString();
            other.gameObject.SetActive(false);
        }
    }
    #endregion
}
