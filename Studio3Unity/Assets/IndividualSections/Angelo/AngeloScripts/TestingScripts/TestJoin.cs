using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestJoin : MonoBehaviour
{
    public bool autoConnect = true;
    public byte version = 1;
    public bool connectInUpdate = true;

    public virtual void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
        //PhotonNetwork.ConnectUsingSettings(version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
    }

    public virtual void Update()
    {
        if (connectInUpdate && autoConnect && !PhotonNetwork.connected)
        {
            connectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }
}
