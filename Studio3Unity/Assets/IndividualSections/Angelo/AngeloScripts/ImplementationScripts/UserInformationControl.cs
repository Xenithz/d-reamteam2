using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInformationControl : MonoBehaviour
{
    #region Public variables
    public static UserInformationControl instance = new UserInformationControl();

    public string[] userStatsArray;
    public string[] leaderboardArray;
    #endregion

    #region Private variables
    [SerializeField]
    // string myRegisterUrl = "http://localhost/studio3/RegisterData.php";
    string myRegisterUrl = "https://amcg.000webhostapp.com/RegisterData.php";
    [SerializeField]
    // string myLoginUrl = "http://localhost/studio3/Login.php";
    string myLoginUrl = "https://amcg.000webhostapp.com/Login.php";
    [SerializeField]
    // string myBanCheckUrl = "http://localhost/studio3/BanCheck.php";
    string myBanCheckUrl = "https://amcg.000webhostapp.com/BanCheck.php";
    [SerializeField]
    // string myAdminCheckUrl = "http://localhost/studio3/AdminCheck.php";
    string myAdminCheckUrl = "https://amcg.000webhostapp.com/AdminCheck.php";
    [SerializeField]
    // string myBanUserUrl = "http://localhost/studio3/BanUser.php";
    string myBanUserUrl = "https://amcg.000webhostapp.com/BanUser.php";
    [SerializeField]
    // string myUnbanUserUrl = "http://localhost/studio3/UnbanUser.php";
    string myUnbanUserUrl = "https://amcg.000webhostapp.com/UnbanUser.php";
    [SerializeField]
    // string myEditDataUrl = "http://localhost/studio3/EditData.php";
    string myEditDataUrl = "https://amcg.000webhostapp.com/EditData.php";
    [SerializeField]
    // string myRecieveDataUrl = "http://localhost/studio3/GrabUserData.php";
    string myRecieveDataUrl = "https://amcg.000webhostapp.com/GrabUserData.php";
    [SerializeField]
    // string myLeaderboardUrl = "http://localhost/studio3/Leaderboard.php";
    string myLeaderboardUrl = "https://amcg.000webhostapp.com/Leaderboard.php";

    [SerializeField]
    int localRounds;
    [SerializeField]
    int localExp;
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

    private void Update()
    {
        //TESTING STUFF
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CallEditData(UserStats.instance.myUsername, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CallGrabData(UserStats.instance.myUsername, UserStats.instance.myPassword, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CallGrabLeaderboard();
        }
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
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);
        myForm.AddField("playerpasswordpost", password);

        WWW myWWW = new WWW(myLoginUrl, myForm);
        yield return myWWW;

        Debug.Log("login PHP: " + myWWW.text);

        if(myWWW.text == "Login success")
        {
            Debug.Log("Login success");
            gameObject.GetComponent<MyNetworkManager>().isLoggedIn = true;
            gameObject.GetComponent<MyNetworkManager>().shouldConnect = true;
            StartCoroutine(GrabData(username, password, true));
            StartCoroutine(GrabLeaderboard());
        }
        else if(myWWW.text == "Password incorrect")
        {
            Debug.Log("Login failed due to incorrect password");
        }
        else if(myWWW.text == "User not found")
        {
            Debug.Log("Login failed due to nonexistant user");
        }
    }

    IEnumerator BanCheck(string username)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);

        WWW myWWW = new WWW(myBanCheckUrl, myForm);
        yield return myWWW;

        Debug.Log("ban check PHP: " + myWWW.text);

        if(myWWW.text == "User is banned")
        {
            Debug.Log("User is banned, don't continue");
            gameObject.GetComponent<LoginUI>().DisplayBanned();
        }
        else if(myWWW.text == "User isn't banned")
        {
            Debug.Log("User isn't banned, continue");
            StartCoroutine(AdminCheck(username));
        }
    }

    IEnumerator AdminCheck(string username)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);

        WWW myWWW = new WWW(myAdminCheckUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);

        if(myWWW.text == "User is an admin")
        {
            while(GetComponent<MyNetworkManager>().hasConnected != true)
            {
                yield return null;
            }

            GetComponent<LoginUI>().networkChoosePanel.SetActive(true);
            GetComponent<LoginUI>().loginPanel.SetActive(false);
            gameObject.GetComponent<LoginUI>().EnableAdminPanel();
        }
        else if(myWWW.text == "User isn't an admin")
        {
            GetComponent<LoginUI>().networkChoosePanel.SetActive(true);
            GetComponent<LoginUI>().loginPanel.SetActive(false); 
            Debug.Log("User not admin, don't enable panel");
        }
    }

    IEnumerator BanUser(string username)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);

        WWW myWWW = new WWW(myBanUserUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);
    }

    IEnumerator UnbanUser(string username)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);

        WWW myWWW = new WWW(myUnbanUserUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);
    }

    IEnumerator EditData(string username, int roundsWon)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);
        myForm.AddField("roundssurvivedpost", roundsWon);

        WWW myWWW = new WWW(myEditDataUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);

        StartCoroutine(GrabData(username, UserStats.instance.myPassword, false));
    }

    IEnumerator GrabData(string username, string password, bool usedForLogin)
    {
        WWWForm myForm = new WWWForm();
        myForm.AddField("playerusernamepost", username);

        WWW myWWW = new WWW(myRecieveDataUrl, myForm);
        yield return myWWW;

        Debug.Log(myWWW.text);

        string dataString = myWWW.text;
        userStatsArray = dataString.Split(';');
        localRounds = int.Parse(userStatsArray[0]);
        localExp = int.Parse(userStatsArray[1]);
    
        UserStats.instance.SetUserStats(username, password, localRounds, localExp);
        
        if(usedForLogin == true)
        {
            StartCoroutine(BanCheck(username));
        }
        else
        {
            Debug.Log("Coroutine not used for login");
        }
    }

    IEnumerator GrabLeaderboard()   
    {
        WWW myWWW = new WWW(myLeaderboardUrl);
        yield return myWWW;
        string leaderboardString = myWWW.text;
        leaderboardArray = leaderboardString.Split(';');

        for(int i = 0; i < GetComponent<LoginUI>().leaderboardTextArray.Length; i++)
        {
            GetComponent<LoginUI>().leaderboardTextArray[i].text = UserInformationControl.instance.leaderboardArray[i];
            Debug.Log("hit");
        }
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

    public void CallBanUser(string inputUsername)
    {
        StartCoroutine(BanUser(inputUsername));
    }

    public void CallUnbanUser(string inputUsername)
    {
        StartCoroutine(UnbanUser(inputUsername));
    }

    public void CallEditData(string inputUsername, int roundsSurvived)
    {
        StartCoroutine(EditData(inputUsername, roundsSurvived));
    }

    public void CallGrabData(string inputUsername, string inputPassword, bool inputBool)
    {
        StartCoroutine(GrabData(inputUsername, inputPassword, inputBool));
    }

    public void CallGrabLeaderboard()
    {
        StartCoroutine(GrabLeaderboard());
    }
    #endregion
}
