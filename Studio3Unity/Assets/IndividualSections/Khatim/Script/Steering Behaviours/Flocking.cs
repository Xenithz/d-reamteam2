using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    #region Public Variables
    public int maxSpeed;
    public int maxForce;
    public GameObject flockingTarget;
    public GameObject[] noOfBoids;
    public float seekWeight;
    public float alignWeight;
    public float separateWeight;
    public float cohesionWeight;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private List<Rigidbody> boids;
    #endregion

    #region Callbacks
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        boids = new List<Rigidbody>();

        noOfBoids = GameObject.FindGameObjectsWithTag("Zombie");
        flockingTarget = GameObject.FindGameObjectWithTag("Player");

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
        Vector3 seek = Seek(flockingTarget.transform.position);
        Vector3 separate = Separation();
        Vector3 align = Alignment();
        Vector3 cohesion = Cohesion();

        rg.AddForce(seek * seekWeight);
        rg.AddForce(separate * separateWeight);
        rg.AddForce(align * alignWeight);
        rg.AddForce(cohesion * cohesionWeight);
        /*Vector3 target = flockingTarget.position;
        target.y = transform.position.y;
        transform.LookAt(target + rg.velocity);*/

    }

    Vector3 Seek(Vector3 target)
    {
        Vector3 desiredSeekVel = (target - transform.position).normalized * maxSpeed;
        Vector3 seekSteering = desiredSeekVel - rg.velocity;
        Vector3 seekSteeringClamped = Vector3.ClampMagnitude(seekSteering, maxForce);
        transform.LookAt(flockingTarget.transform.position);
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
