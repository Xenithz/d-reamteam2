using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CollisionAvoidance : MonoBehaviour
{
private RaycastHit colliderHit;
public GameObject[] alex= new GameObject[6];
public Vector3 velocity;
public Vector3 targetVector;
public Vector3 steeringForce;
public float raycastLenght;
public Vector3 height;
public Transform target;
public float avoidanceForce;
public float vision;
public float speed;
public float lookSpeed;
public Rigidbody rb;
public float maxSpeed;
	void Start () 
	{
	    rb = this.gameObject.GetComponent<Rigidbody>();
		target=GameObject.FindGameObjectWithTag("Player").transform;
	}
	void FixedUpdate () 
	{
	velocity=rb.velocity;
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
	return colliderHit.transform != target;
	}
	public void Move()
	{
	steeringForce=(transform.position+=targetVector.normalized);
    steeringForce=Vector3.ClampMagnitude(steeringForce,10);
	rb.AddForce(steeringForce*Time.deltaTime*speed);
	rb.velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
	}



	
}

