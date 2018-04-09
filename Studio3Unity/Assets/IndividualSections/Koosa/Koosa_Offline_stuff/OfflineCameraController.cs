using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraController : MonoBehaviour 
{
	public List <Transform> Players;
	public Vector3 offset;
	public Vector3 vel;
	public float smoothing;
	public float minZoom;
	public float maxZoom;
	public float limitZoom;
	public Camera playerCam;
void Start()
{
	playerCam=GetComponent<Camera>();
	
}
	void LateUpdate()
	{
		if(Players.Count==0)
		return;
		MoveCamera();
	}
	public void ZoomCamera()
	{
		float updatedZoom=Mathf.Lerp(maxZoom,minZoom,GetDistance()/limitZoom);
        playerCam.fieldOfView=Mathf.Lerp(playerCam.fieldOfView,updatedZoom ,Time.deltaTime);
	}
	public void MoveCamera()
	{
		Vector3 focusPoint=GetFocusPoint();
		Vector3 updatedPosition=focusPoint+offset;
		transform.position=Vector3.SmoothDamp(transform.position,updatedPosition,ref vel, smoothing*Time.deltaTime);
	}
	Vector3 GetFocusPoint()
	{
		if(Players.Count==1)
		{
        return Players[0].position;
		}
		var bounds =new Bounds(Players[0].position,Vector3.zero);
		for (int i = 0; i < Players.Count; i++)
		{
		bounds.Encapsulate(Players[i].position);
		}
		return bounds.center;
	}
	float GetDistance()
	{
		var bounds= new Bounds (Players[0].position,Vector3.zero);
		for (int i = 0; i < Players.Count; i++)
		{
		bounds.Encapsulate(Players[i].position);
		}
		return bounds.size.x;
	}
}
