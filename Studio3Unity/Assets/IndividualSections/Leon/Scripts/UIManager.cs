using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform loginPanel;
    public Transform registerPanel;
    public Transform mainMenuPanel;
    public Transform offlinePickPanel;
    public Transform onlinePickPanel;
    public Transform lobbyPanel;
    public GameObject banPanel;
    public Transform leaderboardPanel;

    public Transform youAreBannedPanel;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickToMainMenuScene()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1;
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

    public void OnClickOfflinePanel()
    {
        PhotonNetwork.offlineMode = true;
        offlinePickPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void OnClickOnlinePanel()
    {
        onlinePickPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(false);
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

    public void OnClickPlayOnline()
    {
        if (MyNetworkManager.instance.isLoggedIn == false)
        {
            PhotonNetwork.offlineMode = false;
            mainMenuPanel.gameObject.SetActive(false);
            loginPanel.gameObject.SetActive(true);
        }
        else if (MyNetworkManager.instance.isLoggedIn == true)
        {
            PhotonNetwork.offlineMode = false;
            mainMenuPanel.gameObject.SetActive(false);
            onlinePickPanel.gameObject.SetActive(true);
        }
    }

    public void OnClickLobbyPanel()
    {
        lobbyPanel.gameObject.SetActive(true);
        onlinePickPanel.gameObject.SetActive(false);
    }

    public void OnClickToLeaderboard()
    {
        leaderboardPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void OnClickToAdminPanel()
    {
        if (banPanel.activeInHierarchy == false)
        {
            banPanel.gameObject.SetActive(true);
        }
        else if (banPanel.activeInHierarchy == true)
        {
            banPanel.gameObject.SetActive(false);
        }
    }


    public void OnClickOfflineOnePlayerScene() // to go to 2 player map
    {
        SceneManager.LoadScene("Offline_1_Player");
    }


    public void OnClickOfflineTwoPlayerScene() // to go to 4 player map
    {
        SceneManager.LoadScene("Offline_2_Player");
    }

    public void OnClickFourPlayerScene() // to go to 4 player map
    {
        SceneManager.LoadScene("4_Player_Offline");
    }

    public void OnClickToMain() // to go to main menu
    {
        mainMenuPanel.gameObject.SetActive(true);
        loginPanel.gameObject.SetActive(false);
        offlinePickPanel.gameObject.SetActive(false);
        onlinePickPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(false);
        lobbyPanel.gameObject.SetActive(false);
        youAreBannedPanel.gameObject.SetActive(false);
        leaderboardPanel.gameObject.SetActive(false);
    }
}
