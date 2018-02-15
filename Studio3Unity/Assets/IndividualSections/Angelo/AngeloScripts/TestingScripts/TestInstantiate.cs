using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    public Transform mySpawn;
    public GameObject prefab;

    public void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(prefab.name, mySpawn.position, Quaternion.identity, 0);
    }
}
