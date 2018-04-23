using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using UnityEngine.SceneManagement;
public enum GameStates
{
        Starting,
        Spawning,
        Prepping,
        Playing,
        Ending,
        Default
};

public class GameManagerBase : Photon.PunBehaviour
{
    #region Public variables
    public GameStates myGameState;

    public static GameManagerBase instance;

    public List<GameObject> playersDead;

    public GameObject[] playerHp;
    
    public GameObject[] spawnPoints;

    public GameObject playerPrefab;

    public GameObject[] playerPrefabsArray;

    public GameObject myLocalPlayer;

    public GameObject endingPanel;

    public Text myText;

    public Text roundText;

    public GameObject textHolder;

    public int roundNumber;

    public int amountOfEasyToSpawn;

    public int amountOfMediumToSpawn;

    public int amountOfHardToSpawn;

    public bool flag1;

    public Scene myScene;
    #endregion

    #region Unity callbacks
    private void Start()
    {
        roundNumber = 0;
        amountOfEasyToSpawn = 0;
        amountOfMediumToSpawn = 0;
        amountOfHardToSpawn = 0;
        myGameState = GameStates.Starting;
        flag1 = false;
        Initialize();
    }
    private void Awake() 
    {
        instance = this;
        textHolder = GameObject.FindWithTag("hpText");
    }

    private void Update()
    {
        myScene = SceneManager.GetActiveScene();
        if(GameManagerBase.instance.myGameState == GameStates.Playing && Zombie_Pool.zombiePoolInstance.activeZombies.Count == 0 && Zombie_Pool.zombiePoolInstance.zombiesHaveSpawned == true)
        {
            roundNumber++;
            Debug.Log("Current round number: " + roundNumber);
            UpdateSpawnValues();
            Debug.Log("Updating spawn values");
            myGameState = GameStates.Spawning;
            Debug.Log("Transitioning to spawning state");
        }


        if(GameManagerBase.instance.myGameState == GameStates.Spawning)
        {
            if(Zombie_Pool.zombiePoolInstance.stopSpawning == false)
            {
                SetUpNewRound(amountOfEasyToSpawn, amountOfMediumToSpawn, amountOfHardToSpawn);
            }
            Debug.Log("This is the count of active zombies: " + Zombie_Pool.zombiePoolInstance.activeZombies.Count);
        }

        roundText.text = roundNumber.ToString();
        textHolder.GetComponent<Text>().text = myLocalPlayer.GetComponent<Character_Controller>().hp.ToString();

        if(myScene.name == "2_Player_Online")
        {
            if(playersDead.Count == 2 && myGameState != GameStates.Ending)
            {
                EndGame();
            }
        }

        if(myScene.name == "4_Player_Online")
        {
            if(playersDead.Count == 4 && myGameState != GameStates.Ending)
            {
                EndGame();
            }
        }
        //failsafe
        if(Input.GetKeyDown(KeyCode.O) && PhotonNetwork.isMasterClient)
        {
            EndGame();
        }
    }
    #endregion

    #region My functions
    public void Initialize()
    {
        GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefabsArray[UserStats.instance.spawnType].name, spawnPoints[0].transform.position, Quaternion.identity, 0);
        myLocalPlayer = myPlayer;
        myPlayer.name = PhotonNetwork.player.NickName;
        amountOfEasyToSpawn = 2;
        this.myGameState = GameStates.Playing;
    }

    public void UpdateRoundsSurvived()
    {
        UserInformationControl.instance.CallEditData(UserStats.instance.myUsername, this.roundNumber);
    }

    public void SetUpNewRound(int easySpawn, int mediumSpawn, int hardSpawn)
    {
        //Zombie_Pool.zombiePoolInstance.Spawn(easySpawn, mediumSpawn, hardSpawn);
        Zombie_Pool.zombiePoolInstance.CallSpawn2(easySpawn, mediumSpawn, hardSpawn);
    }
    
    public void EndGame()
    {
        myGameState = GameStates.Ending;
        if(PhotonNetwork.isMasterClient)
        {
            string myInt = this.roundNumber.ToString();
            photonView.RPC("DisplayEndPanel", PhotonTargets.All, myInt);
        }
    }

    public void UpdateSpawnValues()
    {
        if(amountOfEasyToSpawn != 10)
        {
            amountOfEasyToSpawn = amountOfEasyToSpawn + 2;
        }
        if(amountOfEasyToSpawn == 10 && amountOfMediumToSpawn != 6)
        {
            amountOfMediumToSpawn = amountOfMediumToSpawn + 2;
        }
        if(amountOfEasyToSpawn == 10 && amountOfMediumToSpawn == 6 && amountOfHardToSpawn != 4)
        {
            amountOfHardToSpawn = amountOfHardToSpawn + 2;
        }
        if(amountOfEasyToSpawn == 10 && amountOfMediumToSpawn == 6 && amountOfHardToSpawn == 4)
        {
            Debug.Log("Stopping updates");
        }
    }

    public void BackToOnline()
    {
        SceneManager.LoadScene("Main_Menu");
        UserInformationControl.instance.CallLogin(UserStats.instance.myUsername, UserStats.instance.myPassword);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    
        
    #endregion

    #region My RPCs
    [PunRPC]
    public void DisplayEndPanel(string intToPass)
    {
        endingPanel.SetActive(true);
        myText.text = "You survived " + intToPass + " rounds!";
        UpdateRoundsSurvived();
    }
        
    #endregion

}
