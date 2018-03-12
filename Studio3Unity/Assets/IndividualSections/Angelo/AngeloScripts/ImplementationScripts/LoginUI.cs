using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    #region Public variables

    public GameObject uiPanel;
    public GameObject loginPanel;
    public GameObject networkChoosePanel;
    public InputField registrationUsername;
    public InputField registrationPassword;
    public InputField registrationEmail;

    public InputField loginUsername;
    public InputField loginPassword;

    public GameObject adminPanel;
    public InputField adminPanelUsername;

    public GameObject banPanel;

    public GameObject leaderboardPanel;
    public Text[] leaderboardTextArray;
    #endregion

    #region My functions
    public void CreateUser()
    {
        string username = registrationUsername.text.ToString();
        string password = registrationPassword.text.ToString();
        string email = registrationEmail.text.ToString();

        UserInformationControl.instance.CallCreateUser(username, password, email);
    }

    public void Login()
    {
        string username = loginUsername.text.ToString();
        string password = loginPassword.text.ToString();

        UserInformationControl.instance.CallLogin(username, password);
    }

    public void BanUser()
    {
        string username = adminPanelUsername.text.ToString();

        UserInformationControl.instance.CallBanUser(username);
    }

    public void UnbanUser()
    {
        string username = adminPanelUsername.text.ToString();

        UserInformationControl.instance.CallUnbanUser(username);
    }

    public void EnableAdminPanel()
    {
        adminPanel.SetActive(true);
    }

    public void DisplayBanned()
    {
        //uiPanel.SetActive(false);
        loginPanel.SetActive(false);
        banPanel.SetActive(true);
    }

    public void UpdateLeaderboard()
    {
        UserInformationControl.instance.CallGrabLeaderboard();
        leaderboardPanel.SetActive(true);
        uiPanel.SetActive(false);
    }
    #endregion
}
