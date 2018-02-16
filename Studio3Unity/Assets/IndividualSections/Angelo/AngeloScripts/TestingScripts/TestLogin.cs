using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogin : MonoBehaviour
{
    public string testUsername;
    public string testPassword;
    public string testEmail;

    string myRegisterUrl = "http://localhost/studio3/RegisterData.php";
    string myLoginUrl = "http://localhost/studio3/Login.php";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CreateUser(testUsername, testPassword, testEmail));
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            StartCoroutine(Login(testUsername, testPassword));
        }
    }

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
}
