using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour 
{
	#region  Public Variable
	public GameObject zombie;
	public GameObject healthTarget;
	public float maxSpeed;
	public float maxForce;
	public float distanceToZombies;
	public float distanceToHealth;
	#endregion

	#region Private Variables
	private Rigidbody rg;
	private int myCondition;
	private int fleeCondition = 1;
	private int seekCondition = 2;
	#endregion
	
	#region Callbacks
	void Start()
	{
		rg = GetComponent<Rigidbody>();
	}
	void Update()
	{
		distanceToHealth = Vector3.Distance(transform.position, healthTarget.transform.position);
		distanceToZombies = Vector3.Distance(transform.position, zombie.transform.position);
		
		if(GameObject.FindGameObjectWithTag("Health") != null)
		{
			myCondition = 2;
		}
		else
		{
			myCondition = 0;
		}
		
		if(GameObject.FindGameObjectWithTag("Zombie") != null && distanceToZombies < 10)
		myCondition = 1;
	}
	void FixedUpdate () 
	{
		switch(myCondition)
		{
			case 1:
			AvoidZombies();
			Debug.Log("Avoiding Zombies");
			break;

			case 2:
			SeekHealth();
			Debug.Log("Seeking Health");
			break;

			default:
			break;
		}
	}
	#endregion

	#region  My Functions
	void SeekHealth()
	{
		Vector3 desiredVel = (healthTarget.transform.position - transform.position).normalized * maxSpeed;
		Vector3 steering = desiredVel - rg.velocity;
		Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
		rg.AddForce(steeringClamped);
		transform.LookAt(transform.position + rg.velocity);
	}

	void AvoidZombies()
	{
		Vector3 vel = (transform.position - zombie.transform.position).normalized * maxSpeed;
		Vector3 steering = vel - rg.velocity;
		Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
		rg.AddForce(steeringClamped);
		transform.LookAt(transform.position + rg.velocity);
	}
	#endregion
}
