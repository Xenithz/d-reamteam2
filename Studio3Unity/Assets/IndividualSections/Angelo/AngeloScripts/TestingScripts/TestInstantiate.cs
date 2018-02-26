using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    public Transform mySpawn;
    public GameObject prefab;
    public int i;
    
    public void Awake()
    {
        i = 0;
    }

    public void OnJoinedRoom()
    {
        GameObject myObj = PhotonNetwork.Instantiate(prefab.name, mySpawn.position, Quaternion.identity, 0);
        PhotonNetwork.playerName = "player " + i;
        myObj.name = PhotonNetwork.playerName;
        //photonView.RPC("UpdateInt", PhotonTargets.AllBuffered);
        
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            Debug.Log(PhotonNetwork.playerList[i]);
        }
    }
}
