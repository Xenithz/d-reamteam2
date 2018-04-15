﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    #region Public variables
    public static UserStats instance = new UserStats();

    public string myUsername;
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
    public void SetUserStats(string usernameToSet, int roundsToSet, int expToSet)
    {
        PhotonNetwork.playerName = usernameToSet;
        PhotonNetwork.player.NickName = usernameToSet;
        myUsername = usernameToSet;
        myRounds = roundsToSet;
        myExp = expToSet;
    }
    #endregion
}
