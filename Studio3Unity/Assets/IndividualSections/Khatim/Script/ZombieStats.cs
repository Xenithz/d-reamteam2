using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    #region Public Variables
    public float speed;
    public float attackDistance;
    public float distanceToPlayer;
    public float recycleTime;
    //[HideInInspector] public Rigidbody rg;
    public GameObject player;
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
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    private void OnEnable()
    {
        StartCoroutine("ZombieCou");
    }

    private void OnDisable()
    {
        StopCoroutine("ZombieCou");
    }
    #endregion

    #region Functions
    void Recycle()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region IEnumerators
    IEnumerator ZombieCou()
    {
        yield return new WaitForSeconds(recycleTime);
        Recycle();
    }
    #endregion

    
}
