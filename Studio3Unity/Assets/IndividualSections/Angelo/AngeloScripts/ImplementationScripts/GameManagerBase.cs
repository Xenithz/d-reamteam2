using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public enum GameStates
{
        Starting,
        Spawning,
        Playing,
        Ending
};

public class GameManagerBase : Photon.PunBehaviour
{
    #region Public variables
    public GameStates myGameState;

    public static GameManagerBase instance;

    public GameObject[] spawnPoints;

    public GameObject playerPrefab;

    public GameObject myLocalPlayer;

    public int roundNumber;

    public bool flag1;
    #endregion

    #region Unity callbacks
    private void Start()
    {
        roundNumber = 1;
        myGameState = GameStates.Starting;
        Initialize();
    }
    private void Awake() 
    {
        instance = this;
    }

    private void Update()
    {
        //if(AIHandler.instance.aiToTrack.Count == 0)
        //{
            // roundNumber++;
            // SetUpNewRound(1);
        //}

        if(GameManagerBase.instance.myGameState == GameStates.Spawning)
        {
            roundNumber++;
            SetUpNewRound(roundNumber);
        }
    }
    #endregion

    #region My functions
    public void Initialize()
    {
        GameObject myPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoints[0].transform.position, Quaternion.identity, 0);
        myLocalPlayer = myPlayer;
        this.myGameState = GameStates.Starting;
        SetUpNewRound(roundNumber);
    }

    public void UpdateRoundsSurvived()
    {
        UserInformationControl.instance.CallEditData(UserStats.instance.myUsername);
    }

    public void SetUpNewRound(int roundNumberToPass)
    {
        Zombie_Pool.zombiePoolInstance.Spawn(roundNumberToPass);
    }
    
    public void EndGame()
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
