using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    #region Public variables
    public static UserStats instance = new UserStats();

    public string myUsername;

    public string myPassword;
    public int myRounds;
    public int myExp;

    public int spawnType;
    #endregion

    #region Private variables

    #endregion

    #region Unity callbacks
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region My functions
    public void SetUserStats(string usernameToSet, string passwordToSet, int roundsToSet, int expToSet)
    {
        PhotonNetwork.playerName = usernameToSet;
        PhotonNetwork.player.NickName = usernameToSet;
        myPassword = passwordToSet;
        myUsername = usernameToSet;
        myRounds = roundsToSet;
        myExp = expToSet;
    }
    #endregion
}
