using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieFSM : Photon.PunBehaviour, IPunObservable
{
    #region Public Variables
    public int speed;
    public int maxSpeed;
    public float attackDistance;
    public float distanceToPlayer;
    [HideInInspector] public Rigidbody rg;
    public GameObject[] players;
    public GameObject player;
    public float damageDelay = 2;

    public float attackTimer;

    public bool canAttack;
    #endregion
    public PlayerStats myPlayer;
    #region Private Variables
    private int myCondition;
    private int chaseCondition = 1;
    private int attackCondition = 2;
    public Animator zombieAnime;
    #endregion

    #region Callbacks
    void Awake()
    {
        rg = GetComponent<Rigidbody>();
        myCondition = chaseCondition;
        canAttack = true;
        damageDelay = 2;
        attackTimer = 0;
    }

    void OnEnable() 
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player");

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

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Processing
        if(PhotonNetwork.isMasterClient && PhotonNetwork.connected)
        {
            if (distanceToPlayer < attackDistance)
            {
                if(myCondition != attackCondition && canAttack == true)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "2");
                }
            }
            else if(distanceToPlayer > attackDistance)
            {
                if(myCondition != chaseCondition)
                {
                    photonView.RPC("ChangeCondition", PhotonTargets.All, "1");
                }
            }

            if(canAttack == false)
            {
                attackTimer = attackTimer + Time.deltaTime;
                if(attackTimer >= damageDelay)
                {
                    canAttack = true;
                    attackTimer = 0;
                }
            }    
        }
        else if(!PhotonNetwork.connected) //OFFLINE
        {
            if (distanceToPlayer < attackDistance)
            {
                if(myCondition != attackCondition)
                {
                   myCondition=2;
                   zombieAnime.SetBool("isAttacking",true);
                   zombieAnime.SetBool("isWalking",false);
                }
            }
            else if(distanceToPlayer > attackDistance)
            {
                if(myCondition != chaseCondition)
                {
                   myCondition=1;
                   zombieAnime.SetBool("isAttacking",false);
                   zombieAnime.SetBool("isWalking",true);
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
            Vector3 heading = (player.transform.position - this.gameObject.transform.position).normalized;
            Vector3 heading1=( new Vector3 (player.transform.position.x,0,player.transform.position.z)- new Vector3 (this.gameObject.transform.position.x,0,this.gameObject.transform.position.z)).normalized;                                        
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            rg.AddForce(heading * speed, ForceMode.Impulse);
            transform.LookAt(heading+this.transform.position);
            
            break;

            case 2:
            Debug.Log("attacking");
            GameManagerBase.instance.myLocalPlayer.GetComponent<PlayerStats>().Damage();
            canAttack = false;
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

        if(intToPass == "1")
        {
            zombieAnime.SetBool("isAttacking",false);
            zombieAnime.SetBool("isWalking",true);
        }
        else if(intToPass == "2")
        {
            zombieAnime.SetBool("isAttacking",true);
            zombieAnime.SetBool("isWalking",false);
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
