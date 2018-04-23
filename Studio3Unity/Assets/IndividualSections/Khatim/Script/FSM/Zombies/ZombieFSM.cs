using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieFSM : Photon.PunBehaviour, IPunObservable
{
    #region Public Variables

    public int playerId;
    public int speed;
    public int maxSpeed;
    public float attackDistance;
    public float distanceToPlayer;
    [HideInInspector] public Rigidbody rg;
    public GameObject[] players;
    public GameObject player;
    public OfflinePlayerStats offlinePlayerStats;
    public float timer;
    public float damageDelay = 4;

    public int damage;
    public PlayerStats playerStats;

    public float attackTimer;

    public bool canAttack;
    #endregion
    #region Private Variables
    public int myCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    public Animator zombieAnime;
    #endregion

    #region Callbacks
    void Awake()
    {
        zombieAnime=this.gameObject.GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        canAttack = true;
        damageDelay = 4;
        attackTimer = 0;
    }

    void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //player = GameObject.FindGameObjectWithTag("Player");

        if (PhotonNetwork.isMasterClient)
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

    void Update()
    {
        if (player == null)
        {
            if (PhotonNetwork.isMasterClient)
            {
                int randomizedInt = Random.Range(0, players.Length);
                this.photonView.RPC("ChoosePlayer", PhotonTargets.AllViaServer, randomizedInt.ToString());
            }
        }

        timer -= Time.deltaTime;
        playerId = player.GetComponent<PhotonView>().viewID;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Processing
        if (PhotonNetwork.isMasterClient && PhotonNetwork.connected && timer < 4)
        {
            if (distanceToPlayer < attackDistance)
            {
                if (myCondition != attackCondition && canAttack == true)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "2");
                    canAttack = false;
                }
            }
            else if (distanceToPlayer > attackDistance)
            {
                if (myCondition != chaseCondition)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
                }
            }

            if (canAttack == false)
            {
                attackTimer = attackTimer + Time.deltaTime;
                if (attackTimer >= damageDelay)
                {
                    canAttack = true;
                    attackTimer = 0;
                }
            }
        }
        else if (!PhotonNetwork.connected && timer < 4) //OFFLINE
        {
            if (distanceToPlayer < attackDistance)
            {
                if (myCondition != attackCondition && canAttack)
                {
                    myCondition = 2;
                    zombieAnime.SetBool("isAttacking", true);
                    zombieAnime.SetBool("isWalking", false);
                }
            }
            else if (distanceToPlayer > attackDistance)
            {
                if (myCondition != chaseCondition)
                {
                    myCondition = 1;
                    zombieAnime.SetBool("isAttacking", false);
                    zombieAnime.SetBool("isWalking", true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        //Execution
        switch (myCondition)
        {
            case 1:
                Vector3 heading = (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)).normalized;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                rg.AddForce(heading * speed, ForceMode.Impulse);
                transform.LookAt(heading + this.transform.position);
                break;

            case 2:
                Debug.Log("attacking");
                if (PhotonNetwork.connected)
                {
                    //GameManagerBase.instance.myLocalPlayer.GetComponent<PlayerStats>().Damage();
                    // player.GetComponent<PlayerStats>().CallDmg();
                    // myCondition = 1;
                    // canAttack = false;

                    // if(PhotonNetwork.isMasterClient)
                    // {
                    //     if(player == GameManagerBase.instance.myLocalPlayer)
                    //     {
                    //         GameManagerBase.instance.myLocalPlayer.GetComponent<PlayerStats>().Damage();
                    //         canAttack = false;
                    //     }
                    //     else if(player != GameManagerBase.instance.myLocalPlayer)
                    //     {
                    //         player.GetComponent<PlayerStats>().CallDmg();
                    //         canAttack = false;
                    //     }
                    // }

                    if (PhotonNetwork.isMasterClient)
                    {
                        player.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damage);
                        Debug.Log("Attacker: " + this.gameObject.name + "    " + "Victim: " + player.GetComponent<PhotonView>().viewID);
                        photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
                        canAttack = false;
                    }
                    if (!PhotonNetwork.isMasterClient)
                    {
                        player.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damage);
                        Debug.Log("Attacker: " + this.gameObject.name + "    " + "Victim: " + player.GetComponent<PhotonView>().viewID);
                        photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
                    }
                }
                // if(!PhotonNetwork.connected)
                //offlinePlayerStats.Damage();
                break;

            default:
                break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(canAttack);
        }
        else
        {
            canAttack = (bool)stream.ReceiveNext();
        }
    }
    #endregion

    #region Functions
    [PunRPC]
    public void ChangeCondition(string intToPass)
    {
        int myInt = int.Parse(intToPass);
        myCondition = myInt;

        if (intToPass == "1")
        {
            zombieAnime.SetBool("isAttacking", false);
            zombieAnime.SetBool("isWalking", true);
        }
        else if (intToPass == "2")
        {
            zombieAnime.SetBool("isAttacking", true);
            zombieAnime.SetBool("isWalking", false);
        }
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
