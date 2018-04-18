using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Photon.MonoBehaviour, IPunObservable
{
	public int hp;


	private void Awake() 
	{
		hp = 6;	
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(hp);
        }
        else
        {
            this.hp = (int)stream.ReceiveNext();
        }
    }
}
