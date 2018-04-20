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
    public Transform tileUIExplainCanvas;
    public int zombieCount;
	// Use this for initialization
	void Start () 
    {
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

    public void OnClickToTileUIExplain()
    {
        zombieExplainCanvas.gameObject.SetActive(false);
        tileUIExplainCanvas.gameObject.SetActive(true);
    }


    public void OnClickToDismantleTile()
    {
        tileUIExplainCanvas.gameObject.SetActive(false);
        dismantleTileCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    void Update () 
    {
          if(zombieCount==0)  
          {     
            SceneManager.LoadScene("Main_Menu");
          }
        }
	}

