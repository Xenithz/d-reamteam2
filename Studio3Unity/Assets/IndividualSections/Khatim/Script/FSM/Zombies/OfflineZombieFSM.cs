using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineZombieFSM : MonoBehaviour
{
    #region Public Variables
    public int speed;
    public int maxSpeed;
    public float attackDistance;
    public float distanceToPlayer;
    public GameObject[] players;
    public float delayedDamage;
    public int randomTarget;
    public float timeToAttack;
    public GameObject offlinePlyStats;
    public OfflinePlayerStats offlinePly;
    //public Animator zombieAnim;
    public bool attacking;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private int currCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;


    #endregion

    #region Callbacks
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        currCondition = chaseCondition;
        delayedDamage = 2;
        timeToAttack = 2;
        offlinePlyStats = GameObject.FindGameObjectWithTag("OfflinePlayerStats");
        offlinePly = offlinePlyStats.GetComponent<OfflinePlayerStats>();
        attacking = false;
    }

    void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        randomTarget = Random.Range(0, players.Length);
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, players[randomTarget].transform.position);

        if (distanceToPlayer < attackDistance)
        {
            if (currCondition != attackCondition)
            {
                currCondition = 2;
                /*zombieAnim.SetBool("isAttacking", true);
                zombieAnim.SetBool("isWalking", false);*/
            }
        }
        else if (distanceToPlayer > attackDistance)
        {
            if (currCondition != chaseCondition)
            {
                currCondition = 1;
                /*zombieAnim.SetBool("isAttacking", false);
                zombieAnim.SetBool("isWalking", true);*/
            }
        }

        if (!players[randomTarget].activeInHierarchy)
        {
            currCondition = 3;
        }

        if (attacking == true)
        {
            timeToAttack = timeToAttack + Time.deltaTime;
            if (timeToAttack >= delayedDamage)
            {
                Debug.Log("Attacking");
                timeToAttack = 0;
            }
        }
    }

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1:
                Vector3 heading = (players[randomTarget].transform.position - this.gameObject.transform.position).normalized;

                speed = Mathf.Clamp(speed, 0, maxSpeed);
                rg.AddForce(heading * speed, ForceMode.Impulse);
                transform.LookAt(heading + this.transform.position);
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

    #endregion

}
