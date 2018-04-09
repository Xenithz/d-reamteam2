using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    #region Public Variables
    public int neighbourCount = 0;
    public int maxSpeed;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private List<Rigidbody> boids;
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
    /*Vector3 CompiledAgents ()
	{

	}

	Vector3 Cohesion()
	{

	}

	Vector3 Seperation()
	{

	}

	Vector3 Alignment()
	{
		Vector3 totalVector =  new Vector3(0,0,0);

		foreach(var other in boids)
		{
			totalVector = totalVector + other.velocity;
		}

		Vector3 avgVel = totalVector/boids.Count;
		//Set Magnitude.
	}*/
    #endregion
}
