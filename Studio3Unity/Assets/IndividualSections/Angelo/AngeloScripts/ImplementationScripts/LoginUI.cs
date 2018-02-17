using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    #region Public variables

    public InputField registrationUsername;
    public InputField registrationPassword;
    public InputField registrationEmail;

    public InputField loginUsername;
    public InputField loginPassword;
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
    #endregion
}
