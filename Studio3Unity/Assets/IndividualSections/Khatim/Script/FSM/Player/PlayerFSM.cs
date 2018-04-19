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
    public OfflinePlayerStats offlinePly;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private int currCondition;
    private Vector3 desriedFleeVel;
    private Vector3 totalDesiredFleeVel;
    //private Vector3 desiredSeekVel;
    private Vector3 totalDesiredSeekVel;
    #endregion

    #region Callbacks
    void Start()
    {
        offlinePly.GetComponent<OfflinePlayerStats>();
        rg = GetComponent<Rigidbody>();

        totalDesiredFleeVel = Vector3.zero;
        totalDesiredSeekVel = Vector3.zero;

        currCondition = 1;
    }
    void Update()
    {
        healthTargets = GameObject.FindGameObjectsWithTag("HealthPickup");
        zombieTargets = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < healthTargets.Length; i++)
        {
            distanceToHealth = Vector3.Distance(transform.position, healthTargets[i].transform.position);
            if (healthTargets[i] != null)
                currCondition = 2;
        }

        for (int j = 0; j < zombieTargets.Length; j++)
        {
            distanceToZombies = Vector3.Distance(transform.position, zombieTargets[j].transform.position);
            if (zombieTargets[j] != null)
                currCondition = 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealthPickup")
        {
            other.gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        switch (currCondition)
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
            if (healthTargets[i].activeInHierarchy && offlinePly.healthP2 <= 5)
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

        /*for (int i = 0; i < healthTargets.Length; i++)
        {
            if (healthTargets[i].activeInHierarchy && offlinePly.health <= 5)
            {
                desiredSeekVel = (healthTargets[i].transform.position - transform.position).normalized * maxSpeed;
            }
        }

        Vector3 steering = desiredSeekVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);*/
    }

    void AvoidZombies()
    {
        totalDesiredFleeVel = Vector3.zero;
        for (int j = 0; j < zombieTargets.Length; j++)
        {
            //This is setting a new velocity everytime.
            //Need to add all of the forces together;
            if (distanceToZombies <= 20)
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
