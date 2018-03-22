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
    #endregion

    #region Photon callbacks
    public override void OnJoinedRoom()
    {
        GameObject playerName = PhotonNetwork.Instantiate("PlayerName", myPlayerNamePosition.position, Quaternion.identity, 0);
        playerName.GetComponentInChildren<Text>().text = PhotonNetwork.player.NickName;
        playerName.transform.SetParent(lobbyPanel.transform);

        if(PhotonNetwork.isMasterClient == false)
        {
            for(int i = 0; i < PhotonNetwork.playerList.Length - 1; i++)
            {
                GameObject otherPlayerNames = PhotonNetwork.Instantiate("PlayerName", playerNamePositions[i].position, Quaternion.identity, 0);
                otherPlayerNames.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[i].NickName;
                otherPlayerNames.transform.SetParent(playerListPanel.transform);
            }
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        PhotonPlayer[] myPlayerList = PhotonNetwork.playerList;
        System.Array.Reverse(myPlayerList);
        for(int i = 0; i < PhotonNetwork.playerList.Length - 1; i++)
        {
            GameObject playerName = PhotonNetwork.Instantiate("PlayerName", playerNamePositions[i].position, Quaternion.identity, 0);
            playerName.GetComponentInChildren<Text>().text = newPlayer.NickName;
            playerName.transform.SetParent(playerListPanel.transform);
        }
    }
    #endregion
}
