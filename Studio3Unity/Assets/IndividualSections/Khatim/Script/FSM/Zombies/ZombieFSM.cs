using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : Photon.MonoBehaviour
{
    #region Public Variables
    public GameObject[] healthSprite;

    public int speed;
    public int maxSpeed;
    public int attackDistance;
    public float distanceToPlayer;
    [HideInInspector] public Rigidbody rg;

    public GameObject[] players;
    public GameObject player;
    public float damageDelay = 2;

    #endregion
    public PlayerStats myPlayer;
    #region Private Variables
    //private enum Condition { Chase, Attack };
    //private Condition currCondition;
    private int myCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    #endregion

    #region Callbacks
    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        rg = GetComponent<Rigidbody>();
        //currCondition = Condition.Chase;
        myCondition = chaseCondition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PhotonNetwork.isMasterClient)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                //currCondition = Condition.Attack;
                photonView.RPC("ChangeCondition", PhotonTargets.All, "2");
            }
            else
                photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
        }

        switch (/*currCondition*/ myCondition)
        {
            case /*Condition.Chase*/ 1:
                transform.LookAt(player.transform.position);
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                rg.AddForce(transform.forward * speed);
                Debug.Log("Chasing");
                break;
            case /*Condition.Attack*/ 2:
                //myPlayer.Damage();
                Debug.Log("Attacking");
                break;
            default:
                break;
        }
    }
    #endregion
 
    #region Functions

    [PunRPC]
    public void ChangeCondition(string intToPass)
    {
        int myInt = int.Parse(intToPass);
        myCondition = myInt;
    }
    #endregion

}
