using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInstantiate : Photon.MonoBehaviour
{
    public GameObject spawnPoints;
    public GameObject prefab;

    public GameObject aiPrefab;

    public GameObject myLocal;

    public List<int> allPlayers;

    public Text myText;

    public bool flag;
    
    public void Start()
    {
        //GameObject myObj = PhotonNetwork.Instantiate(prefab.name, spawnPoints[PhotonNetwork.player.ID - 1].transform.position, Quaternion.identity, 0);
        flag = false;
    }

    public void OnJoinedRoom()
    {
        GameObject myObj = PhotonNetwork.Instantiate(prefab.name, spawnPoints.transform.position, Quaternion.identity, 0);
        myLocal = myObj;
        flag = true;
        if(flag == true)
        {
            this.photonView.RPC("AddToList", PhotonTargets.AllBufferedViaServer);
            flag = false;
        }
    }

    public void Update()
    {
        if(PhotonNetwork.isMasterClient && Input.GetKeyDown(KeyCode.P))
        {
            GameObject myObj = PhotonNetwork.Instantiate(aiPrefab.name, spawnPoints.transform.position, Quaternion.identity, 0);
        }

        myText.text = myLocal.GetComponent<TestPlayer>().hp.ToString();
    }

    [PunRPC]
    public void AddToList()
    {
        //GameObject localPlayer = GameObject.Find(localPlayerName);
        allPlayers.Add(myLocal.GetComponent<PhotonView>().viewID);
        Debug.Log("joined " + myLocal.GetComponent<PhotonView>().viewID);
        flag = false;

    }
}
