using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GameManagerBase : Photon.PunBehaviour
{
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

    private void Awake() 
    {
        instance = this;
    }

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

    [PunRPC]
    public void DisplayEndPanel()
    {
        
    }

}
