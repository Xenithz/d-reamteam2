using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Transform loginPanel;
    public Transform registerPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
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

    public void OnClickOfflineOnePlayer() // to go to 2 player map
    {
        SceneManager.LoadScene("2_Player_Offline");
    }
    public void OnClickOfflineTwoPlayer() // to go to 4 player map
    {
        SceneManager.LoadScene("2_Player_Offline");
    }

    public void OnClickToMain() // to go to main menu
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void OnClickRegisterPanel()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
    }

    public void OnClickLoginPanel()
    {
        loginPanel.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(false);
    }

    public void OnClickToOfflinePick() // to go to main menu
    {
        SceneManager.LoadScene("Offline_Level_Pick");
    }

    

}
