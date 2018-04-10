using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleeing : MonoBehaviour
{
    #region  Public Variable
    public GameObject enemyTarget;
    public float maxSpeed;
    public float maxForce;
    public float distance;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    #endregion

    #region Callbacks
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        enemyTarget = GameObject.FindGameObjectWithTag("Health");
    }

    void FixedUpdate()
    {
        Vector3 desiredVel = (transform.position - enemyTarget.transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);
    }
    #endregion

}
