using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject prefab;
    
    public void Awake()
    {

    }

    public void OnJoinedRoom()
    {
        GameObject myObj = PhotonNetwork.Instantiate(prefab.name, spawnPoints[PhotonNetwork.player.ID - 1].transform.position, Quaternion.identity, 0);
        Debug.Log(PhotonNetwork.player.ID);
    }
}
