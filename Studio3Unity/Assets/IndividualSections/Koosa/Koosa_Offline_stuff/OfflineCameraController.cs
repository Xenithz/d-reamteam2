using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraController : MonoBehaviour 
{
	#region Public Variables
	public Vector3 offset;
	public Vector3 velocity;
	public float smoothing;
	public float minZoom;
	public float maxZoom;
	public float limitZoom;
	public Camera playerCam;
	#endregion
	#region Private Variables
	private Bounds bounds;
	private List <Transform> Players;
	#endregion
    #region Unity Functions
void Awake()
{
	playerCam=GetComponent<Camera>();
	foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(player.transform);
        }
}
	void LateUpdate()
	{
		if(Players.Count==0)
		return;
		CameraMove();
	}
	#endregion
	#region My Functions
	public void ZoomCameraIn()
	{
		float updatedZoom=Mathf.Lerp(maxZoom,minZoom,Distance()/limitZoom);
        playerCam.fieldOfView=Mathf.Lerp(playerCam.fieldOfView,updatedZoom ,Time.deltaTime);
	}
	public void CameraMove()
	{
		Vector3 focusPoint=PointToFocus();
		Vector3 updatedPosition=focusPoint+offset;
		transform.position=Vector3.SmoothDamp(transform.position,updatedPosition,ref velocity, smoothing*Time.deltaTime);
	}
	Vector3 PointToFocus()
	{
		if(Players.Count==1)
		{
        return Players[0].position;
		}
		bounds =new Bounds(Players[0].position,Vector3.zero);

		for (int i = 0; i < Players.Count; i++)
		{
		bounds.Encapsulate(Players[i].position);
		}
		return bounds.center;
	}
	float Distance()
	{
	    bounds= new Bounds (Players[0].position,Vector3.zero);
		for (int i = 0; i < Players.Count; i++)
		{
		bounds.Encapsulate(Players[i].position);
		}
		return bounds.size.x;
	}
}
#endregion
