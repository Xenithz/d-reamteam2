using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour 
{
	#region  Public Variable
	public GameObject[] zombies;
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
	void Awake()
	{
		rg = GetComponent<Rigidbody>();
		myCondition = fleeCondition;
	}
	void OnEnable () 
	{
		zombies = GameObject.FindGameObjectsWithTag("Zombies");
		healthTarget = GameObject.FindGameObjectWithTag("Health");
	}
	void Update()
	{
		distanceToHealth = Vector3.Distance(transform.position, healthTarget.transform.position);
		distanceToZombies = Vector3.Distance(transform.position, zombie.transform.position);

		if(GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp <= 5)
		{
			myCondition = 2;
		}
		else
		myCondition = 1;
	}
	void FixedUpdate () 
	{
		switch(myCondition)
		{
			case 1:
			AvoidZombies();
			break;

			case 2:
			SeekHealth();
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
		Vector3 desiredVel = (transform.position - zombie.transform.position).normalized * maxSpeed;
		Vector3 steering = desiredVel - rg.velocity;
		Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
		rg.AddForce(steeringClamped);
		transform.LookAt(transform.position + rg.velocity);
	}
	#endregion
}
