using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    #region  Public Variable
    public GameObject[] zombieTargets;
    public GameObject[] healthTargets;
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
    private Vector3 totalDesiredFleeVel;
    private Vector3 desiredSeekVel;
    private Vector3 totalDesiredSeekVel;
    #endregion

    #region Callbacks
    void Start()
    {
        totalDesiredFleeVel = Vector3.zero;
        totalDesiredSeekVel = Vector3.zero;

        rg = GetComponent<Rigidbody>();
    }
    void Update()
    {
        healthTargets = GameObject.FindGameObjectsWithTag("Health");
        zombieTargets = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < healthTargets.Length; i++)
        {
            distanceToHealth = Vector3.Distance(transform.position, healthTargets[i].transform.position);
            if (healthTargets[i] != null)
                myCondition = 2;
        }


        for (int j = 0; j < zombieTargets.Length; j++)
        {
            distanceToZombies = Vector3.Distance(transform.position, zombieTargets[j].transform.position);
            if (zombieTargets[j] != null && distanceToZombies < 30)
                myCondition = 1;
        }
    }
    void FixedUpdate()
    {
        switch (myCondition)
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

    #region Functions
    void SeekHealth()
    {
        totalDesiredSeekVel = Vector3.zero;
        for (int i = 0; i < healthTargets.Length; i++)
        {
            if (healthTargets[i].activeInHierarchy)
            {
                Debug.Log("Seeking Health");
                Vector3 desiredSeekVel = (healthTargets[i].transform.position - transform.position).normalized * maxSpeed;
                totalDesiredSeekVel += desiredSeekVel;
            }
        }
        Vector3 steering = totalDesiredSeekVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);

        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);
    }

    void AvoidZombies()
    {
        totalDesiredFleeVel = Vector3.zero;
        for (int j = 0; j < zombieTargets.Length; j++)
        {
            //This is setting a new velocity everytime.
            //Need to add all of the forces together;
            if (distanceToZombies <= 30)
            {
                Debug.Log("Avoiding Zombies");
                desriedFleeVel = (transform.position - zombieTargets[j].transform.position).normalized * maxSpeed;
                totalDesiredFleeVel += desriedFleeVel;
            }
        }
        Vector3 steering = totalDesiredFleeVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);

        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);
    }
    #endregion
}
