using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAi : Photon.MonoBehaviour, IPunObservable
{
    public List<int> players;
    public int myId;
    public GameObject player;

    public GameObject myTextHolder;
    public GameObject myTextHolder2;

    public string targetName;

    GameObject controlScripts;

    private void Awake()
    {
        //players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //player = GameObject.FindGameObjectWithTag("Player");
        myTextHolder = GameObject.Find("target");
        myTextHolder2 = GameObject.Find("target2");
        controlScripts = GameObject.Find("ControlScripts");
        //photonView.RPC("AddPlayers", PhotonTargets.AllBuffered);
        photonView.RPC("AddPlayers", PhotonTargets.MasterClient);

        if (PhotonNetwork.player == PhotonNetwork.masterClient)
        {
            int randomizedInt = Random.Range(0, players.Count);
            player = PhotonView.Find(players[randomizedInt]).gameObject;
            myId = player.GetComponent<PhotonView>().viewID;
            //photonView.RPC("SetInt", PhotonTargets.MasterClient);
        }
    }

    private void Update()
    {
        player = PhotonView.Find(myId).gameObject;
        myTextHolder.GetComponent<Text>().text = player.GetComponent<PhotonView>().viewID.ToString() + " " + players.Count;
        if(Input.GetKeyDown(KeyCode.M))
        {
            CallDamage();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            player.GetComponent<TestPlayer>().hp--;
        }
    }

    // [PunRPC]
    // public void SetInt()
    // {
    //     int randomizedInt = Random.Range(0, players.Count);
    //     this.photonView.RPC("ChoosePlayer", PhotonTargets.AllViaServer, randomizedInt.ToString());
    //     myTextHolder2.GetComponent<Text>().text = randomizedInt.ToString();
    // }

    // [PunRPC]
    // public void ChoosePlayer(string intToPass)
    // {
    //     int myInt = int.Parse(intToPass);
    //     player = players[myInt];
    // }

    [PunRPC]
    public void Damage()
    {
        player.GetComponent<TestPlayer>().hp--;
    }

    [PunRPC]
    public void AddPlayers()
    {
        players = controlScripts.GetComponent<TestInstantiate>().allPlayers;
    }

    public void CallDamage()
    {
        photonView.RPC("Damage",PhotonTargets.All);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}
