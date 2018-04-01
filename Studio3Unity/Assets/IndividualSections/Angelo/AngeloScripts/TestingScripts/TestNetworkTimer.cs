using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkTimer : Photon.PunBehaviour
{
	public bool isIn;

	[SerializeField]
	public double myTime;

	public void Start()
	{
		isIn = false;
	}
	public override void OnJoinedRoom()
	{
		isIn = true;
	}

	public void Update()
	{
		if(isIn == true)
		{
			myTime = PhotonNetwork.time;
		}
	}
}
