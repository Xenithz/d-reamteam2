using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyer : MonoBehaviour
{
    public TutorialHandler tutorial;
    public int playersInTwoPlayerMap;
    public bool tutorialflag;
    void OnCollisionEnter(Collision other)
    {

        if ((other.gameObject.GetComponent<ZombieFSM>() || other.gameObject.GetComponent<OfflineZombieTutorial>()) && tutorialflag)
        {
            other.gameObject.SetActive(false);
            tutorial.zombieCount--;
        }
        if (other.gameObject.GetComponent<ZombieFSM>() || other.gameObject.GetComponent<OfflineZombieFSM>())
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.GetComponent<Character_Controller>() || other.gameObject.GetComponent<OfflineCharacterController>() && tutorialflag)
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Main_Menu");
        }
        if (other.gameObject.GetComponent<Character_Controller>() || other.gameObject.GetComponent<OfflineCharacterController>())
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Game_Over_Scene");
        }


        if (other.gameObject.GetComponent<PlayerTwoOfflineController>() || other.gameObject.GetComponent<PlayerOneOfflineController>())
        {
            Destroy(other.gameObject);
            playersInTwoPlayerMap--;
            if (playersInTwoPlayerMap == 0)
                SceneManager.LoadScene("Game_Over");
        }



    }
    void Awake()
    {
        playersInTwoPlayerMap = 2;
        if (tutorialflag)
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialHandler>();
    }
}
