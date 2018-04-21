using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MyNetworkManager : Photon.PunBehaviour
{
	#region Public variables
	public static MyNetworkManager instance;

	public bool shouldConnect;
	public bool hasConnected;

	public bool isLoggedIn;

	public string myScene;

	public GameObject myButton;
	#endregion

	#region Private variables
	private static int maxPlayersForTwo = 2;
	private static int maxPlayersForFour = 4;

	private	GameObject uiHolder;
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

		shouldConnect = false;
		isLoggedIn = false;
		uiHolder = GameObject.Find("UI");
	}

	private void Update()
	{
		if(shouldConnect == true && PhotonNetwork.connected == false && PhotonNetwork.offlineMode == false)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
	}
	#endregion

	#region Photon callbacks
	public override void OnConnectedToPhoton()
	{
		Debug.Log("Initial Photon connection established");
		hasConnected = true;
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a room succesfully");
		PhotonNetwork.automaticallySyncScene = true;
		//enable lobby panel
		uiHolder.GetComponent<UIManager>().lobbyPanel.gameObject.SetActive(true);
		uiHolder.GetComponent<UIManager>().onlinePickPanel.gameObject.SetActive(false);

		if(PhotonNetwork.room.PlayerCount == 1)
		{
			UserStats.instance.spawnType = 0;
		}
		else if(PhotonNetwork.room.PlayerCount == 2)
		{
			UserStats.instance.spawnType = 1;	
		}
		else if(PhotonNetwork.room.PlayerCount == 3)
		{
			UserStats.instance.spawnType = 2;
		}
		else if(PhotonNetwork.room.PlayerCount == 4)
		{
			UserStats.instance.spawnType = 3;
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("Player just joined");
		Debug.Log(PhotonNetwork.room.MaxPlayers);

		if(PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
		{
			PhotonNetwork.room.IsOpen = false;
			Debug.Log("Room is now closed");
			//PhotonNetwork.LoadLevel(myScene);
			//LoadSceneFromLobby();
			myButton.SetActive(true);
		}
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		if(myScene == "2_Player_Online")
		{
			PhotonNetwork.CreateRoom(null, TwoPlayerOnline(), null);
		}
		else if(myScene == "4_Player_Online")
		{
			PhotonNetwork.CreateRoom(null, FourPlayerOnline(), null);
		}
		else if(myScene == null || myScene == "")
		{
			Debug.Log("myScene var empty");
		}
	}

	public override void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon");
	}
	#endregion
	
	#region My functions
	public RoomOptions TwoPlayerOnline()
    {
        RoomOptions myRoomOptions = new RoomOptions();
		myRoomOptions.MaxPlayers = (byte)maxPlayersForTwo;
		myRoomOptions.CustomRoomProperties = new Hashtable() { { "twoplayers", 1 } };
        myRoomOptions.CustomRoomPropertiesForLobby = new string[] { "twoplayers"};

        return myRoomOptions;
    }

	public RoomOptions FourPlayerOnline()
	{
		RoomOptions myRoomOptions = new RoomOptions();
		myRoomOptions.MaxPlayers = (byte)maxPlayersForFour;
		myRoomOptions.CustomRoomProperties = new Hashtable() { { "fourplayers", 1 } };
        myRoomOptions.CustomRoomPropertiesForLobby = new string[] { "fourplayers"};

		return myRoomOptions;
	}

	public void LoadSceneFromLobby()
	{
		PhotonNetwork.LoadLevel(myScene);
	}

	public void JoinTwoPlayersRandom()
	{
		myScene = "2_Player_Online";
		Hashtable roomPropertiesToSearch = new Hashtable() {{"twoplayers", 1}};
		PhotonNetwork.JoinRandomRoom(roomPropertiesToSearch, (byte)maxPlayersForTwo);
	}

	public void JoinFourPlayersRandom()
	{
		myScene = "4_Player_Online";
		Hashtable roomPropertiesToSearch = new Hashtable() {{"fourplayers", 1}};
		PhotonNetwork.JoinRandomRoom(roomPropertiesToSearch, (byte)maxPlayersForFour);
	}

	public void GoBackToPickerFromRoom()
    {
        PhotonNetwork.LeaveRoom();
		uiHolder.GetComponent<UIManager>().lobbyPanel.gameObject.SetActive(false);
		uiHolder.GetComponent<UIManager>().onlinePickPanel.gameObject.SetActive(true);
        
    }

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		if(PhotonNetwork.room.PlayerCount < PhotonNetwork.room.MaxPlayers)
		{
			PhotonNetwork.room.IsOpen = true;
			Debug.Log("Room is now open");
		}
	}
	#endregion
}
