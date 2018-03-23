using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GameManagerBase : Photon.PunBehaviour
{
    #region Public variables
    public enum GameStates
    {
        Starting,
        Playing,
        Ending
    };

    public GameStates myGameState;

    public static GameManagerBase instance;

    public GameObject[] spawnPoints;

    public string playerPrefabName;
    #endregion

    #region Unity callbacks
    private void Awake() 
    {
        instance = this;
    }
    #endregion

    #region My functions
    public void Initialize()
    {
        if(PhotonNetwork.player.IsLocal)
        {
            GameObject myPlayer = PhotonNetwork.Instantiate(this.playerPrefabName, spawnPoints[PhotonNetwork.player.ID - 1].transform.position, Quaternion.identity, 0);
        }
        this.myGameState = GameStates.Starting;
    }

    public void UpdateRoundsSurvived()
    {
        UserInformationControl.instance.CallEditData(UserStats.instance.myUsername);
    }

    public void SetUpNewRound()
    {

    }
    
    public void EndGame ()
    {

    }
        
    #endregion

    #region My RPCs
    [PunRPC]
    public void DisplayEndPanel()
    {
        
    }
        
    #endregion

}
