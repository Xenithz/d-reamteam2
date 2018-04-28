using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public float seekWeight;
    public float alignWeight;
    public float separateWeight;
    public float cohesionWeight;
    public bool easy;
    public bool medium;
    public bool hard;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private int currCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    private OfflinePlayerStats offlinePly;
    private OfflineZombiePool offZomPool;
    #endregion

    #region Callbacks
    void Awake()
    {
        zombieAnim = this.gameObject.GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        delayedDamage = 2;
        timeToAttack = 2;
        attacking = false;

        offZomPool = GameObject.FindGameObjectWithTag("OfflineZombieSpawner").GetComponent<OfflineZombiePool>();
        offlinePly = GameObject.FindGameObjectWithTag("OfflineStats").GetComponent<OfflinePlayerStats>();

        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
        randomTarget = Random.Range(0, players.Length);
    }

    /*void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
        randomTarget = Random.Range(0, players.Length);
    }*/

    void Update()
    {
        offZomPool.noOfBoids = GameObject.FindGameObjectsWithTag("Zombie");

        distanceToPlayer = Vector3.Distance(transform.position, players[randomTarget].transform.position);
        if (distanceToPlayer < attackDistance && timer < 4)
        {
            if (currCondition != attackCondition)
            {
                currCondition = 2;
                zombieAnim.SetBool("isAttacking", true);
                zombieAnim.SetBool("isWalking", false);
            }
            if (easy || medium)
                AudioManager.auidoInstance.PlaySFX(AudioManager.auidoInstance.effectSource, 10, 4, 0.8f, AudioManager.auidoInstance.effectClips);
            if (hard)
                AudioManager.auidoInstance.PlaySFX(AudioManager.auidoInstance.effectSource, 10, 3, 0.8f, AudioManager.auidoInstance.effectClips);
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
            randomTarget = Random.Range(0, players.Length);
            currCondition = 3;
        }

        //Zombie Attacking
        if (attacking == true)
        {
            timeToAttack = timeToAttack + Time.deltaTime;
            if (timeToAttack >= delayedDamage)
            {
                offlinePly.DamageTaken(zomDamage, randomTarget);
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

        foreach (var other in offZomPool.boids)
        {
            if (offZomPool.boids != null)
            {
                float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);

                if (distanceBetweenBoids > 0 && distanceBetweenBoids < distanceFromNeighbour)
                {
                    sumOfPos += other.transform.position;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            Vector3 avgPos = cohesionDesiredVel / offZomPool.boids.Count;
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

        foreach (var other in offZomPool.boids)
        {
            if (offZomPool.boids != null)
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

        foreach (var other in offZomPool.boids)
        {
            if (offZomPool.boids != null)
            {
                float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);
                if (distanceBetweenBoids > 0 && distanceBetweenBoids < neighbourDistance)
                {
                    totalVector = totalVector + other.velocity;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            Vector3 avgVel = (totalVector / offZomPool.boids.Count).normalized * maxSpeed;
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
