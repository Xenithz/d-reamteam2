using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour {
    public Transform welcomeCanvas;
    public Transform tileExplainCanvas;
    public Transform dismantleTileCanvas;
    public Transform zombieExplainCanvas;
    public Transform playerExplainCanvas;
    public List<GameObject> zombiesInLevel=new List<GameObject>();
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}

    // Update is called once per frame

    public void OnClickToTileExplain()
    {
        welcomeCanvas.gameObject.SetActive(false);
        tileExplainCanvas.gameObject.SetActive(true);
    }

    public void OnClickToPlayerExplain()
    {
        tileExplainCanvas.gameObject.SetActive(false);
        playerExplainCanvas.gameObject.SetActive(true);
    }

    public void OnClickToZombieExplain()
    {
        playerExplainCanvas.gameObject.SetActive(false);
        zombieExplainCanvas.gameObject.SetActive(true);
    }

    public void OnClickToDismantleTile()
    {
        zombieExplainCanvas.gameObject.SetActive(false);
        dismantleTileCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    void Update () {

     foreach (var zombie in GameObject.FindGameObjectsWithTag("Zombie"))
     {
         if(!zombiesInLevel.Contains(zombie))
             zombiesInLevel.Add(zombie);
     }




        if(!zombiesInLevel.Contains(gameObject))
        {
            SceneManager.LoadScene("Main_Menu");
        }
		
	}
}
