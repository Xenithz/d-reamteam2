using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    #region  Public Variable
    [Header("Zombies")]
    public GameObject[] zombieTargets;
    public float distanceToZombies;
    public float closeRange;
    [Header("Health")]
    public GameObject[] healthTargets;
    public float distanceToHealth;
    [Header("Forces")]
    public float maxSpeed;
    public float maxForce;

    #endregion

    #region Private Variables
    private Rigidbody rg;
    private Animator aiAnim;
    private int currCondition;
    private Vector3 desriedFleeVel;
    private Vector3 totalDesiredFleeVel;
    private Vector3 totalDesiredSeekVel;
    private OfflinePlayerStats offlinePly;
    [SerializeField]
    private int randomTarget;
    #endregion

    #region Callbacks
    void Start()
    {
        offlinePly = GameObject.FindGameObjectWithTag("OfflineStats").GetComponent<OfflinePlayerStats>();
        rg = GetComponent<Rigidbody>();
        aiAnim = GetComponent<Animator>();

        totalDesiredFleeVel = Vector3.zero;
        totalDesiredSeekVel = Vector3.zero;
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
    }

    void FixedUpdate()
    {
        randomTarget = Random.Range(0, zombieTargets.Length);

        for (int j = 0; j < zombieTargets.Length; j++)
        {
            distanceToZombies = Vector3.Distance(transform.position, zombieTargets[randomTarget].transform.position);
            if (zombieTargets[randomTarget] != null)
            {
                currCondition = 1;
            }
        }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealthPickup")
        {
            other.gameObject.SetActive(false);
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
                aiAnim.SetBool("isWalk", true);
                Debug.Log("Seeking Health");
                Vector3 desiredSeekVel = (healthTargets[i].transform.position - transform.position).normalized * maxSpeed;
                totalDesiredSeekVel += desiredSeekVel;
            }
            else
                aiAnim.SetBool("isWalk", false);
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
            if (distanceToZombies < closeRange)
            {
                aiAnim.SetBool("isWalk", true);
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
