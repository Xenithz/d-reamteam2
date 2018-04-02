using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : MonoBehaviour 
{
	public GameObject healthTarget;
	public float maxSpeed;
	public float maxForce;
	public float distance;
	private Rigidbody rg;
	// Use this for initialization
	void Start () 
	{
		rg = GetComponent<Rigidbody>();
		healthTarget = GameObject.FindGameObjectWithTag("Health");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		distance = Vector3.Distance(healthTarget.transform.position, transform.position);

		Vector3 desiredVel = healthTarget.transform.position - transform.position;

		Vector3 vel	= desiredVel * maxSpeed;
		Vector3 steering = (vel -rg.velocity) * maxForce;
		rg.AddForce(steering);
		transform.LookAt(steering);

		rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		Debug.DrawLine(transform.position, steering);
	}
}
