using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Camera_Controller : Photon.MonoBehaviour
{

#region Public Variables
    public Transform player = null;
    
    public Vector3 offset;
    public float smoothing;
    #endregion


#region Private Variables
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    #endregion


#region Unity Functions
    private void Awake()
    {

    }
   void Update()
   {
    GivePlayer();
   }
	void LateUpdate ()
    {
        target = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing);
    }

    public void GivePlayer()
    {
        if(PhotonNetwork.connected)
        {
        GameObject[] myPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject checkPlayer in myPlayers)
        {
            if (checkPlayer.GetComponent<PhotonView>().isMine)
            {
                player = checkPlayer.transform;
            }
        }
        return;
        }
    }
}
#endregion
