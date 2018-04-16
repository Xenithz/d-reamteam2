using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    #region Public Variables
    public int neighbourCount = 0;
    public int maxSpeed;
    public int maxForce;
    public Transform target;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private List<Rigidbody> boids;
    private Vector3 moveAwayDesiredVel;
    private Vector3 totalMoveAwayDesiredVel;
    private Vector3 totalCohesionDesiredVel;
    private Vector3 cohesionDesiredVel;
    #endregion

    #region Callbacks
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region Function
    /*Vector3 CompiledAgents()
    {
        //This is where all the force gets applied.
        Vector3 seperate = Seperation();
        Vector3 align = Alignment();
        Vector3 cohesion = Cohesion();
    }*/

    Vector3 Seek()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        Vector3 desiredSeekVel = (target.transform.position - transform.position).normalized * maxSpeed;
        Vector3 seekSteering = desiredSeekVel - rg.velocity;
        Vector3 seekSteeringClamped = Vector3.ClampMagnitude(seekSteering, maxForce);
        return seekSteeringClamped;
        //transform.LookAt(transform.position + rg.velocity);
    }

    /*Vector3 Cohesion()
    {
        float distanceFromNeighbour = 50;
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
            //return Seek(avgCohesionVel);
            //return to Seek function with the avgCohesionVel;
        }
        else
        {
            //return V3.zero.
        }

    }*/

    /*Vector3 Seperation()
    {
        totalMoveAwayDesiredVel = Vector3.zero;
        float desiredSeperation = 20;
        int count = 0;

        foreach (var other in boids)
        {
            float distanceBetweenBoids = Vector3.Distance(transform.position, other.transform.position);

            if (distanceBetweenBoids > 0 && distanceBetweenBoids < desiredSeperation)
            {
                moveAwayDesiredVel = (transform.position - other.transform.position).normalized;
                totalMoveAwayDesiredVel += moveAwayDesiredVel;
                count++;
            }
        }

        if (count > 0)
        {
            Vector3 avgAwayVel = totalMoveAwayDesiredVel / boids.Count;
            //Set Magnitude.
            //Subtract the setMag with velocity.
            //Clamp it.
            //Apply the Force.
        }
    }*/

    /*Vector3 Alignment()
    {
        float neighbourDistance = 50;
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
            Vector3 avgVel = (totalVector / boids.Count).normalized;
            //Set Magnitude.
            //Subtract the setMag with velocity.
            //Clamp it.
            //Return Magnitude.
        }
        else
        {
            //return V3.zero.
            transform.position = Vector3.zero;
        }
    }*/
    #endregion
}
