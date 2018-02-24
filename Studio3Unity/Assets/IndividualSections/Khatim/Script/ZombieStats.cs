using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    #region Public Variables
    public float health = 1.0f;
    public float regularDamage = 0.5f;
    public float heavyDamage = 3.0f;
    public float speed;
    public float attackDistance;
    public float distanceToPlayer;
    [HideInInspector] public Rigidbody rg;
    public Transform player;


    #endregion

    #region Private Variables
    //private Animation anim;
    #endregion

    #region Callbacks
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

    }
    #endregion
}
