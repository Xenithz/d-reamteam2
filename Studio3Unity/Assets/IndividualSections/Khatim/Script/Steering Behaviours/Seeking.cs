using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : MonoBehaviour
{
    #region  Public Variable
    public GameObject healthTarget;
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
        healthTarget = GameObject.FindGameObjectWithTag("Health");
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(healthTarget.transform.position, transform.position);

        Vector3 desiredVel = (healthTarget.transform.position - transform.position).normalized * maxSpeed;
        Vector3 steering = desiredVel - rg.velocity;
        Vector3 steeringClamped = Vector3.ClampMagnitude(steering, maxForce);
        rg.AddForce(steeringClamped);
        transform.LookAt(transform.position + rg.velocity);
        //Debug.DrawLine(transform.position, steering);
    }
    #endregion
}
