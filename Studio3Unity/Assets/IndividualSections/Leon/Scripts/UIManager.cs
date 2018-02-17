using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public float scrollingTextTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scrollingTextTimer -= Time.deltaTime;
	}

    public void OnClickToLogin() // to go to log in
    {
        SceneManager.LoadScene("Login");
    }

    public void OnClickQuit() // qjit application
    {
        Application.Quit();
    }
    public void OnClickPlay() //to go to tutorial
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickTwoPlayer() // to go to 2 player map
    {
        SceneManager.LoadScene("2_Player");
    }
    public void OnClickFourPlayer() // to go to 4 player map
    {
        SceneManager.LoadScene("4_Player");
    }

    public void OnClickToMain() // to go to main menu
    {
        SceneManager.LoadScene("Main_Menu");
    }

    
}
