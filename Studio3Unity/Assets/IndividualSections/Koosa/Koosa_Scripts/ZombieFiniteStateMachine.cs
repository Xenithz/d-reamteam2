using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFiniteStateMachine : Photon.PunBehaviour {
public GameObject[] healthSprite;
public Rigidbody rb;
public int myCondition;
private int chaseCondition = 1;
public int attackCondition = 2;
[SerializeField]
public StrongZombie strongZombie;
public NormalZombie normalZombie;
public LongRnageZombie longRnageZombie;
public GameObject[] players;
public GameObject player;
public float distanceToPlayer;
public Character_Controller character;

void Awake()
{
	   character=GameObject.Find("player").GetComponent<Character_Controller>();
	    rb = GetComponent<Rigidbody>();
		longRnageZombie=GameObject.Find("Respawn").GetComponent<LongRnageZombie>();
		strongZombie=GameObject.Find("Respawn").GetComponent<StrongZombie>();
		normalZombie=GameObject.Find("Respawn").GetComponent<NormalZombie>();
        
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
	void FixedUpdate () {
		Debug.Log(PhotonNetwork.isMasterClient);

    if(PhotonNetwork.isMasterClient)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= strongZombie.distanceToAttack)
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
		 switch (/*currCondition*/ myCondition)
        {
            case /*Condition.Chase*/ 1:
                
                Vector3 heading = (player.transform.position - this.gameObject.transform.position).normalized;
				transform.LookAt(player.transform.position);
                strongZombie.speed = Mathf.Clamp(strongZombie.speed, 0, strongZombie.maxSpeed);
                rb.AddForce(heading * strongZombie.speed);
                //Debug.Log("Chasing");
                break;
            case /*Condition.Attack*/ 2:
                //attackanimation
				character.hp--;
                Debug.Log("Attacking");
                break;
            default:
                break;
        }
    }
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


