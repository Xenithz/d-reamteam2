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

		Vector3 desiredVel = (healthTarget.transform.position - transform.position).normalized * maxSpeed;
		Vector3 steering = desiredVel - rg.velocity;
		Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
		rg.AddForce(steeringClamped);
		transform.LookAt(transform.position + rg.velocity);
		//Debug.DrawLine(transform.position, steering);
	}
}
