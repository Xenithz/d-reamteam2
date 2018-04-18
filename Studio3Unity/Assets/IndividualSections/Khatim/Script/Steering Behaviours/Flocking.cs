using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    #region Public Variables
    public int maxSpeed;
    public int maxForce;
    public Transform flockingTarget;
    public GameObject[] noOfBoids;
    public float seekWeight;
    public float alignWeight;
    public float separateWeight;
    public float cohesionWeight;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private List<Rigidbody> boids;
    private Vector3 moveAwayDesiredVel;
    private Vector3 totalMoveAwayDesiredVel;
    private Vector3 totalCohesionDesiredVel;
    private Vector3 cohesionDesiredVel;
    private Vector3 separationSteeringClamp;
    #endregion

    #region Callbacks
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        boids = new List<Rigidbody>();

        noOfBoids = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < noOfBoids.Length; i++)
        {
            Rigidbody rgBoid = noOfBoids[i].GetComponent<Rigidbody>();
            boids.Add(rgBoid);
        }
    }

    void Update()
    {
        CompiledAgents();
    }
    #endregion

    #region Function
    void CompiledAgents()
    {
        //This is where all the force gets applied.
        Vector3 seek = Seek(flockingTarget);
        Vector3 separate = Separation();
        Vector3 align = Alignment();
        //Vector3 cohesion = Cohesion();

        Vector3 seekMulti = seek * seekWeight;
        Vector3 separateMulti = separate * separateWeight;
        Vector3 alignMulti = align * alignWeight;
        //Vector3 cohesionMulti = cohesion * cohesionWeight;

        rg.AddForce(seekMulti);
        rg.AddForce(separateMulti);
        rg.AddForce(alignMulti);
        //rg.AddForce(cohesionMulti);
        /*Vector3 target = flockingTarget.position;
        target.y = transform.position.y;
        transform.LookAt(target + rg.velocity);*/

    }

    Vector3 Seek(Transform target)
    {
        Vector3 desiredSeekVel = (target.position - transform.position).normalized * maxSpeed;
        Vector3 seekSteering = desiredSeekVel - rg.velocity;
        Vector3 seekSteeringClamped = Vector3.ClampMagnitude(seekSteering, maxForce);
        return seekSteeringClamped;
    }

    /*Vector3 Cohesion()
    {
        float distanceFromNeighbour = 6;
        totalCohesionDesiredVel = Vector3.zero;
        int count = 0;

        foreach (var other in boids)
        {
            float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);

            if (distanceBetweenBoids > 0 && distanceBetweenBoids < distanceFromNeighbour)
            {
                cohesionDesiredVel += other.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            Vector3 avgCohesionVel = cohesionDesiredVel / boids.Count;
            return Seek(avgCohesionVel);
            //return Seek(avgCohesionVel);
            //return to Seek function with the avgCohesionVel;
        }
        else
        {
            return Vector3.zero;
        }

    }*/

    Vector3 Separation()
    {
        float desiredSeperation = 10;
        totalMoveAwayDesiredVel = Vector3.zero;
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
        Vector3 totalVector = new Vector3(0, 0, 0);
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
