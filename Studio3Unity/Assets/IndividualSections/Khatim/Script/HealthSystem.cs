using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    #region Public Variables
    public Text healthText;
    public GameObject healthObj;
    #endregion

    #region Private Variables
    private int Score = 0;
    #endregion
    // Update is called once per frame
    void Update ()
    {

	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E))
        {
            Score++;
            healthText.text = Score.ToString();
            healthObj.SetActive(false);
        }
    }
}
