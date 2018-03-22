using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour
{
    #region Public variables
    public Transform[] playerNamePositions;
    public Transform myPlayerNamePosition;
    public GameObject playerListPanel;
    public GameObject lobbyPanel;

    public PhotonPlayer[] myPlayerList;
    #endregion

    #region Unity callbacks
    private void Update()
    {
        Debug.Log(myPlayerList.Length);
        for(int i = 0; i < myPlayerList.Length; i++)
        {
            playerNamePositions[i].GetComponentInChildren<Text>().text = myPlayerList[i].NickName;
        }
    }
    #endregion

    #region Photon callbacks
    public override void OnJoinedRoom()
    {
        myPlayerNamePosition.GetComponentInChildren<Text>().text = PhotonNetwork.player.NickName;
        myPlayerList = PhotonNetwork.playerList;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        myPlayerList = PhotonNetwork.playerList;
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        myPlayerList = PhotonNetwork.playerList;
        for (int i = 0; i < playerNamePositions.Length; i++)
        {
            playerNamePositions[i].GetComponentInChildren<Text>().text = "";
        }
    }
    #endregion
}
