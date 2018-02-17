using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInformationControl : MonoBehaviour
{
    #region Public variables
    public static UserInformationControl instance = new UserInformationControl();
    #endregion

    #region Private variables
    [SerializeField]
    string myRegisterUrl = "http://localhost/studio3/RegisterData.php";
    [SerializeField]
    string myLoginUrl = "http://localhost/studio3/Login.php";

    [SerializeField]
    string myUsername;
    [SerializeField]
    string myPassword;
    [SerializeField]
    string myEmail;

    #endregion

    #region Unity callbacks 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region IEnumerators
    IEnumerator CreateUser(string username, string password, string email)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);
        myForm.AddField("playerpasswordpost", password);
        myForm.AddField("playeremailpost", email);

        WWW myWWW = new WWW(myRegisterUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);
    }

    IEnumerator Login(string username, string password)
    {
        WWWForm myform = new WWWForm();
        myform.AddField("playerusernamepost", username);
        myform.AddField("playerpasswordpost", password);


        WWW myWWW = new WWW(myLoginUrl, myform);
        yield return myWWW;

        Debug.Log(myWWW.text);
    }
    #endregion

    #region My functions
    public void CallCreateUser(string inputUsername, string inputPassword, string inputEmail)
    {
        StartCoroutine(CreateUser(inputUsername, inputPassword, inputEmail));
    }

    public void CallLogin(string inputUsername, string inputPassword)
    {
        StartCoroutine(Login(inputUsername, inputPassword));
    }
    #endregion
}
