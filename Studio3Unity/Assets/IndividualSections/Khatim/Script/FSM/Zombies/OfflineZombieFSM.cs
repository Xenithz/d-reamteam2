using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineZombieFSM : MonoBehaviour
{
    #region Public Variables
    [Header("Variables For Zombies")]
    public float attackDistance;
    public float distanceToPlayer;
    public GameObject[] players;
    public float delayedDamage;
    public int randomTarget;
    public float timeToAttack;
    public int maxSpeed;
    public int maxForce;
    public bool attacking;
    public Animator zombieAnim;
    public int zomDamage;
    public float timer;
    [Header("Flocking Weightage")]
    public GameObject[] noOfBoids;
    public float seekWeight;
    public float alignWeight;
    public float separateWeight;
    public float cohesionWeight;


    #endregion

    #region Private Variables
    private Rigidbody rg;
    private List<Rigidbody> boids;
    private int currCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    private GameObject offlinePlyStats;
    private OfflinePlayerStats offlinePly;
    #endregion

    #region Callbacks
    void Awake()
    {
        zombieAnim = this.gameObject.GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        boids = new List<Rigidbody>();
        delayedDamage = 2;
        timeToAttack = 2;
        attacking = false;

        offlinePlyStats = GameObject.FindGameObjectWithTag("OfflineStats");
        offlinePly = offlinePlyStats.GetComponent<OfflinePlayerStats>();
    }

    void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        randomTarget = Random.Range(0, players.Length);

        noOfBoids = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < noOfBoids.Length; i++)
        {
            Rigidbody rgBoid = noOfBoids[i].GetComponent<Rigidbody>();
            boids.Add(rgBoid);
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, players[randomTarget].transform.position);
        if (distanceToPlayer < attackDistance && timer < 4)
        {
            if (currCondition != attackCondition)
            {
                currCondition = 2;
                zombieAnim.SetBool("isAttacking", true);
                zombieAnim.SetBool("isWalking", false);
            }
        }
        else if (distanceToPlayer > attackDistance)
        {
            if (currCondition != chaseCondition)
            {
                currCondition = 1;
                zombieAnim.SetBool("isAttacking", false);
                zombieAnim.SetBool("isWalking", true);
            }
        }

        if (!players[randomTarget].activeInHierarchy)
        {
            currCondition = 3;
        }

        if (attacking == true)
        {
            timeToAttack = timeToAttack + Time.deltaTime;
            if (timeToAttack >= delayedDamage)
            {
                offlinePly.DamageTaken(zomDamage);
                timeToAttack = 0;
            }
        }
    }

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1:
                CompiledAgents();
                Vector3 target = players[randomTarget].transform.position + rg.velocity;
                target.y = transform.position.y;
                transform.LookAt(target);
                //transform.LookAt(players[randomTarget].transform.position + rg.velocity);
                attacking = false;
                break;

            case 2:
                attacking = true;
                break;

            case 3:
                Debug.Log("No More Players");
                break;

            default:
                break;
        }
    }
    #endregion

    #region Functions
    void CompiledAgents()
    {
        //This is where all the force gets applied.
        Vector3 seek = Seek(players[randomTarget].transform.position);
        Vector3 separate = Separation();
        Vector3 align = Alignment();
        Vector3 cohesion = Cohesion();

        rg.AddForce(seek * seekWeight);
        rg.AddForce(separate * separateWeight);
        rg.AddForce(align * alignWeight);
        rg.AddForce(cohesion * cohesionWeight);

        Vector3 target = players[randomTarget].transform.position;
        target.y = transform.position.y;
        transform.LookAt(target + rg.velocity);

    }

    Vector3 Seek(Vector3 target)
    {
        target.y = transform.position.y;
        Vector3 desiredSeekVel = (target - transform.position).normalized * maxSpeed;
        Vector3 seekSteering = desiredSeekVel - rg.velocity;
        Vector3 seekSteeringClamped = Vector3.ClampMagnitude(seekSteering, maxForce);
        //transform.LookAt(transform.position + rg.velocity);
        return seekSteeringClamped;
    }

    Vector3 Cohesion()
    {
        float distanceFromNeighbour = 6;
        Vector3 totalCohesionDesiredVel = Vector3.zero;
        Vector3 cohesionDesiredVel = Vector3.zero;
        Vector3 sumOfPos = Vector3.zero;
        int count = 0;

        foreach (var other in boids)
        {
            float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);

            if (distanceBetweenBoids > 0 && distanceBetweenBoids < distanceFromNeighbour)
            {
                sumOfPos += other.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            Vector3 avgPos = cohesionDesiredVel / boids.Count;
            return Seek(avgPos);
            //return Seek(avgCohesionVel);
            //return to Seek function with the avgCohesionVel;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 Separation()
    {
        float desiredSeperation = 10;
        Vector3 totalMoveAwayDesiredVel = Vector3.zero;
        Vector3 separationSteeringClamp = Vector3.zero;
        Vector3 moveAwayDesiredVel = Vector3.zero;
        int count = 0;

        foreach (var other in boids)
        {
            float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);

            if (distanceBetweenBoids > 0 && distanceBetweenBoids < desiredSeperation)
            {
                moveAwayDesiredVel = (transform.position - other.transform.position).normalized;
                Vector3 divVel = moveAwayDesiredVel / distanceBetweenBoids;
                totalMoveAwayDesiredVel += divVel;
                count++;
            }
        }

        if (count > 0)
        {
            Vector3 divVel = (totalMoveAwayDesiredVel / count).normalized * maxSpeed;
            Vector3 seperationSteering = divVel - rg.velocity;
            separationSteeringClamp = Vector3.ClampMagnitude(seperationSteering, maxForce);
            //Subtract the setMag with velocity.
            //Clamp it.
            //return.
        }
        return separationSteeringClamp;
    }

    Vector3 Alignment()
    {
        float neighbourDistance = 30;
        Vector3 totalVector = Vector3.zero;
        int count = 0;

        foreach (var other in boids)
        {
            float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);
            if (distanceBetweenBoids > 0 && distanceBetweenBoids < neighbourDistance)
            {
                totalVector = totalVector + other.velocity;
                count++;
            }
        }

        if (count > 0)
        {
            Vector3 avgVel = (totalVector / boids.Count).normalized * maxSpeed;
            Vector3 steerAlign = avgVel - rg.velocity;
            Vector3 steerAlignClamped = Vector3.ClampMagnitude(steerAlign, maxForce);
            return steerAlignClamped;
            //Set Magnitude.
            //Subtract the setMag with velocity.
            //Clamp it.
            //Return Magnitude.
        }
        else
        {
            //return V3.zero.
            return Vector3.zero;
        }
    }
    #endregion

}
