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
        if (GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp<=0)
        {
            // gameObject.SetActive(false);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp < 6)
        {
            healthSprite[GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp].SetActive(true);
            GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp++;
            Destroy(other.gameObject);
        }
    }
    #endregion
}
