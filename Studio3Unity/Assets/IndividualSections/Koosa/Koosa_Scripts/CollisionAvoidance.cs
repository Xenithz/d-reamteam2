﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CollisionAvoidance : MonoBehaviour
 {

private RaycastHit colliderHit;
public Vector3 velocity;
public Vector3 targetVector;
public Vector3 steering;
public float raycastLenght;
public Vector3 height;
public Transform target;
public float avoidanceForce;
public Vector3 avoidanceVector;
public float vision;
public float speed;
public float lookSpeed;
public Rigidbody rb;
	void Start () 
	{
	rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate () 
	{
		Look();
        Vector3 left=transform.position;
		Vector3 right=transform.position;
	  targetVector = (target.transform.position - transform.position).normalized;
     if (Physics.Raycast(transform.position+height, transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(transform.position, colliderHit.point, Color.blue);
		 targetVector+=AvoidFront();
	 }
	  if (Physics.Raycast(left+(-transform.right*vision), transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(left, colliderHit.point, Color.red);
		 targetVector+=AvoidLeft();
	 }
	  if (Physics.Raycast(right + (transform.right * vision), transform.forward+height, out colliderHit, raycastLenght))
	 {
		 Debug.DrawLine(right, colliderHit.point, Color.green);
		 targetVector+=AvoidRight();
	 }
	 Move();
	}
	public Vector3 AvoidFront()
	{
    Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
           avoidanceVector=((targetVector-colliderHit.normal).normalized)*avoidanceForce*Time.deltaTime;

		   Debug.Log("avoidfront");
		}
	 return avoidanceVector;
	}
	public Vector3 AvoidLeft()
	{
		Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
			avoidanceVector=((targetVector-colliderHit.normal).normalized)*avoidanceForce*Time.deltaTime;
           
		   Debug.Log("avoidleft");
		}
	 return avoidanceVector;
	}
	public Vector3 AvoidRight()
	{
    Vector3 avoidanceVector=Vector3.zero;
		if(CanAvoid())
		{
           avoidanceVector=((targetVector-colliderHit.normal).normalized)*avoidanceForce*Time.deltaTime;
		   Debug.Log("avoidright");
		}
	 return avoidanceVector;
	}
   public void Look()
   {
	Quaternion.LookRotation(targetVector+transform.position); //add this transform.position becuz the targetdir is stored as value in memory on the graph, in order to actually look at we need to add out posotion
   }
    public bool CanAvoid()
	{
	return colliderHit.transform != target && colliderHit.collider.tag == "Avoid";
	}
	public void Move()
	{
	steering=(transform.position+=targetVector.normalized);
	rb.AddForce(steering*Time.deltaTime*speed);
	}



	
}

