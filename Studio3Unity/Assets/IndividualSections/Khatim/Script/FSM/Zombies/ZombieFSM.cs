using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : Photon.PunBehaviour
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

    void OnEnable() 
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if(PhotonNetwork.isMasterClient)
        {
            int randomizedInt = Random.Range(0, players.Length);
            this.photonView.RPC("ChoosePlayer", PhotonTargets.AllViaServer, randomizedInt.ToString());
        }
    }

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        gameObject.SetActive(false);
        Zombie_Pool.zombiePoolInstance.zombies.Add(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Processing
        if(PhotonNetwork.isMasterClient)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                //currCondition = Condition.Attack;
                if(myCondition != attackCondition)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "2");
                }
            }
            else
            {
                if(myCondition != chaseCondition)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
                }
            }
                
        }

        //Execution
        switch (/*currCondition*/ myCondition)
        {
            case /*Condition.Chase*/ 1:
                //transform.LookAt(player.transform.position);
                Vector3 heading = player.transform.position - this.gameObject.transform.position;
                Vector3.Normalize(heading);
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                rg.AddForce(heading * speed);
                //Debug.Log("Chasing");
                break;
            case /*Condition.Attack*/ 2:
                //myPlayer.Damage();
                //Debug.Log("Attacking");
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
        Debug.Log("switched");
    }

    [PunRPC]
    public void ChoosePlayer(string intToPass)
    {
        int myInt = int.Parse(intToPass);
        player = players[myInt];
    }

    [PunRPC]
    public void AddZombie(string gameObjectToPass)
    {
        GameObject myGameObject = GameObject.Find(gameObjectToPass);
        Zombie_Pool.zombiePoolInstance.zombies.Add(myGameObject);
    }
    #endregion

}
