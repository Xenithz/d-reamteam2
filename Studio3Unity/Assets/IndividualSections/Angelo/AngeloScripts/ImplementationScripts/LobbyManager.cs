using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour
{
    #region Public variables
    public Transform[] playerNamePositions;
    public GameObject playerListPanel;
    #endregion

    #region Photon callbacks
    public override void OnJoinedRoom()
    {
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            GameObject playerName = PhotonNetwork.Instantiate("PlayerName", playerNamePositions[i].position, Quaternion.identity, 0);
            playerName.GetComponentInChildren<Text>().text = PhotonNetwork.player.NickName;
            playerName.transform.SetParent(playerListPanel.transform);
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        for(int i = 1; i < PhotonNetwork.playerList.Length; i++)
        {
            GameObject playerName = PhotonNetwork.Instantiate("PlayerName", playerNamePositions[i].position, Quaternion.identity, 0);
            playerName.GetComponentInChildren<Text>().text = newPlayer.NickName;
            playerName.transform.SetParent(playerListPanel.transform);
        }
    }
    #endregion
}
