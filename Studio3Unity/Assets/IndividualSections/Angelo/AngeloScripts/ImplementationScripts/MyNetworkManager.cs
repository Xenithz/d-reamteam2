using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MyNetworkManager : Photon.PunBehaviour
{
	#region Public varaibles
	public MyNetworkManager instance = new MyNetworkManager();

	public bool shouldConnect;
	public bool hasConnected;

	public string myScene;
	#endregion

	#region Private variables
	private static int maxPlayersForTwo = 2;
	private static int maxPlayersForFour = 4;
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
	}

	private void Update()
	{
		if(shouldConnect == true && PhotonNetwork.connected == false)
		{
			PhotonNetwork.ConnectUsingSettings("0.1");
		}
	}
	#endregion

	#region Photon callbacks
	public override void OnConnectedToPhoton()
	{
		Debug.Log("Initial Photon connection established");
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a room succesfully");
		PhotonNetwork.automaticallySyncScene = true;
		//enable lobby panel
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		Debug.Log("Player just joined");

		if(PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
		{
			PhotonNetwork.LoadLevel(myScene);
		}
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		if(myScene == "twoplayers")
		{
			PhotonNetwork.CreateRoom(null, TwoPlayerOnline(), null);
		}
		else if(myScene == "fourplayers")
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

	// public void CallSyncLoad(string sceneName)
	// {
	// 	this.photonView.RPC("SyncLoadScene", PhotonTargets.All, sceneName);
	// }

	public void JoinTwoPlayersRandom()
	{
		Hashtable roomPropertiesToSearch = new Hashtable() {{"twoplayers", 1}};
		PhotonNetwork.JoinRandomRoom(roomPropertiesToSearch, (byte)maxPlayersForTwo);
	}

	public void JoinFourPlayersRandom()
	{
		Hashtable roomPropertiesToSearch = new Hashtable() {{"fourplayers", 1}};
		PhotonNetwork.JoinRandomRoom(roomPropertiesToSearch, (byte)maxPlayersForFour);
	}
	#endregion

	#region My RPCs
	// [PunRPC]
	// public void SyncLoadScene(string levelToLoad)
	// {
	// 	PhotonNetwork.LoadLevel(levelToLoad);
	// }
	#endregion
}
