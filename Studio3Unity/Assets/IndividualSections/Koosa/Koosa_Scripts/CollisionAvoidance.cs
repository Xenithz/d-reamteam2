using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CollisionAvoidance : MonoBehaviour
 {

private RaycastHit colliderHit;
public Vector3 targetDir;
public float raycastLenght;
public Vector3 height;
public Transform player;
public Vector3 left;
public Vector3 right;
public float avoidanceForce;
public float vision;
public float speed;
	void Start () 
	{
	left = transform.position;
    right = transform.position;
    left.x -= vision;
    right.x += vision;
	}
	void Update () 
	{
	  Look();
	  targetDir = (player.transform.position - transform.position).normalized;	
     if (Physics.Raycast(transform.position+height, transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(transform.position, colliderHit.point, Color.blue);
		 targetDir+=AvoidFront();
	 }
	 if (Physics.Raycast(left+height, transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(left, colliderHit.point, Color.red);
		 targetDir+=AvoidLeft();
	 }
	 if (Physics.Raycast(right+height, transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(right, colliderHit.point, Color.green);
		 targetDir+=AvoidRight();
	 }
	}
	public Vector3 AvoidFront()
	{
    Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
           avoidanceVector+=colliderHit.normal*avoidanceForce;
		   Debug.Log("avoidfront");
		}
	 return avoidanceVector;
	}
	public Vector3 AvoidLeft()
	{
		Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
           avoidanceVector+=colliderHit.normal*avoidanceForce;
		   Debug.Log("avoidleft");
		}
	 return avoidanceVector;
	}
	public Vector3 AvoidRight()
	{
    Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
           avoidanceVector+=colliderHit.normal*avoidanceForce;
		   Debug.Log("avoidright");
		}
	 return avoidanceVector;
	}
   public void Look()
   {
	   Quaternion rot = Quaternion.LookRotation(targetDir); 
	   transform.rotation= Quaternion.Slerp(transform.rotation,rot,Time.deltaTime*speed);
	   transform.position+=transform.forward*Time.deltaTime;
   }
    public bool CanAvoid()
	{
	return colliderHit.transform != player && colliderHit.collider.tag == "Avoid";
	}
}
