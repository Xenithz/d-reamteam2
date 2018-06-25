using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineZombieTutorial : MonoBehaviour
{
    #region Public Variables
    [Header("Variables For Zombies")]
    public float attackDistance;
    public float distanceToPlayer;
    public GameObject player;
    public float delayedDamage;
    public float timeToAttack;
    public bool attacking;
    public Animator zombieAnim;
    [Header("Other")]
    public float timer;
    public bool easy;
    public bool medium;
    public bool hard;
    [Header("Forces")]
    public float seekWeight;
    public int maxSpeed;
    public int maxForce;
    #endregion

    #region Private Variables
    private Rigidbody rg;
    private int currCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    #endregion

    #region Callbacks
    void Awake()
    {
        zombieAnim = this.gameObject.GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        delayedDamage = 2;
        timeToAttack = 2;
        attacking = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackDistance && timer < 4)
        {
            if (currCondition != attackCondition)
            {
                currCondition = 2;
                zombieAnim.SetBool("isAttacking", true);
                zombieAnim.SetBool("isWalking", false);
            }
            /*if (easy || medium)
                AudioManager.auidoInstance.PlaySFX(AudioManager.auidoInstance.effectSource, 10, 4, 0.8f, AudioManager.auidoInstance.effectClips);
            if (hard)
                AudioManager.auidoInstance.PlaySFX(AudioManager.auidoInstance.effectSource, 10, 3, 0.8f, AudioManager.auidoInstance.effectClips);*/
        }
        else if (distanceToPlayer > attackDistance)
        {
            if (currCondition != chaseCondition)
            {
                currCondition = 1;
                zombieAnim.SetBool("isAttacking", false);
                zombieAnim.SetBool("isWalking", true);
            }
        }

        if (!player.activeInHierarchy)
        {
            currCondition = 3;
        }

        //Zombie Attacking
        if (attacking == true)
        {
            timeToAttack = timeToAttack + Time.deltaTime;
            if (timeToAttack >= delayedDamage)
            {
                player.SetActive(false);
                SceneManager.LoadScene("Main_Menu");
            }
        }
    }

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1:
                CompiledAgents();
                Vector3 target = player.transform.position + rg.velocity;
                target.y = transform.position.y;
                transform.LookAt(target);
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

    #region My Functions
    void CompiledAgents()
    {
        Vector3 seek = Seek(player.transform.position);

        rg.AddForce(seek * seekWeight);

    }
    Vector3 Seek(Vector3 target)
    {
        target.y = transform.position.y;
        Vector3 desiredSeekVel = (target - transform.position).normalized * maxSpeed;
        Vector3 seekSteering = desiredSeekVel - rg.velocity;
        Vector3 seekSteeringClamped = Vector3.ClampMagnitude(seekSteering, maxForce);
        return seekSteeringClamped;
    }
    #endregion
}
