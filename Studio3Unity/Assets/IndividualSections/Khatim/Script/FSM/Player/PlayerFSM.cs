using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    #region  Public Variable
    public GameObject[] zombies;
    public GameObject healthTarget;
    //public GameObject[] healthTargets;
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
    private Vector3 desriedFleeVel;
    private Vector3 totalDesiredVel;
    #endregion

    #region Callbacks
    void Start()
    {
        totalDesiredVel = new Vector3(0, 0, 0);
        rg = GetComponent<Rigidbody>();
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
    }
    void Update()
    {
        //distanceToHealth = Vector3.Distance(transform.position, healthTarget.transform.position);


        if (GameObject.FindGameObjectWithTag("Health") != null)
        {
            myCondition = 2;
        }
        else
            myCondition = 0;

        for (int i = 0; i < zombies.Length; i++)
        {
            distanceToZombies = Vector3.Distance(transform.position, zombies[i].transform.position);
            if (zombies[i] != null)
                myCondition = 1;
        }
    }
    void FixedUpdate()
    {
        switch (myCondition)
        {
            case 1:
                AvoidZombies();
                //Debug.Log("Avoiding Zombies");
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

    #region Functions
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
        totalDesiredVel = Vector3.zero;
        for (int j = 0; j < zombies.Length; j++)
        {
            //This is setting a new velocity everytime.
            //Need to add all of the forces together;
            if (distanceToZombies < 30)
            {
                desriedFleeVel = (transform.position - zombies[j].transform.position).normalized * maxSpeed;
                totalDesiredVel += desriedFleeVel;
            }
        }
        Vector3 steering = totalDesiredVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);
    }
    #endregion
}
