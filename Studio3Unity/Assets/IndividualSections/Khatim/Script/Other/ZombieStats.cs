using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    #region Public Variables
    public int speed;
    public int maxSpeed;
    public int attackDistance;
    public float distanceToPlayer;
    [HideInInspector] public Rigidbody rg;

    public GameObject[] players;
    public GameObject player;
    public float damageDelay = 2;
    #endregion

    #region Private Variables
    /*private Animation anim;
    private int health = 1;
    private int regularDamage = 1;
    private int heavyDamage = 3;*/
    #endregion

    #region Callbacks
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        
    }
    void FixedUpdate()
    {

    }
    #endregion

    #region Functions

    #endregion
}
