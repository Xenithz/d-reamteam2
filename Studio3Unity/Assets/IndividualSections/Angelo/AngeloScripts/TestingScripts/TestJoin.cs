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
    }

    public virtual void Update()
    {
        if (connectInUpdate && autoConnect && !PhotonNetwork.connected)
        {
            connectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
        }
    }
}
