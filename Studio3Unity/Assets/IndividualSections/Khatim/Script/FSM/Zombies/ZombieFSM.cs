﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;
    #endregion
    public PlayerStats myPlayer;
    #region Private Variables
    private enum Condition { Chase, Attack };
    private Condition currCondition;
    private ZombieStats zom;
    #endregion

    #region Callbacks
    // Use this for initialization
    void Start()
    {
        zom = GetComponent<ZombieStats>();
        zom.rg = GetComponent<Rigidbody>();
        GameObject myGameObject = GameObject.FindGameObjectWithTag("Player");
        myPlayer = myGameObject.GetComponent<PlayerStats>();
        currCondition = Condition.Chase;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zom.distanceToPlayer = Vector3.Distance(transform.position, zom.player.transform.position);
        if (zom.distanceToPlayer <= zom.attackDistance)
        {
            currCondition = Condition.Attack;
        }
        else
            currCondition = Condition.Chase;

        switch (currCondition)
        {
            case Condition.Chase:
                transform.LookAt(zom.player.transform.position);
                zom.speed = Mathf.Clamp(zom.speed, 0, zom.maxSpeed);
                zom.rg.AddForce(transform.forward * zom.speed);
                Debug.Log("Chasing");
                break;
            case Condition.Attack:
                //myPlayer.Damage();
                Debug.Log("Attacking");
                break;
            default:
                break;
        }
    }
    #endregion

    #region Functions

    #endregion

}