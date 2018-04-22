using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : Photon.MonoBehaviour, IPunObservable
{

#region Private Variables 
    [SerializeField]
    private Rigidbody playerBody;
    [SerializeField]
    private Collider playerCollider;
    private RaycastHit hit;
    private Tile myTile;
    [SerializeField]
    private float coolDown;
    public float coolDownToSet;
    public GameObject coolDownImage;
    #endregion

#region Public Variables
    public float moveSpeed;
    public float lockRot;
    public float magnitudeToClamp;
    public float jumpPower;
    public Tile_Manager tileManager;
    public Animator playerAnim;
    public int hp;

    public Joystick JoystickScript;
    
    #endregion

#region Unity Functions


    private void Awake()
    {
        playerAnim=GetComponent<Animator>();
        coolDownImage=GameObject.FindGameObjectWithTag("DropAbility");
        coolDown=0;
        GameObject controlScripts = GameObject.Find("ControlScripts");
        tileManager = controlScripts.GetComponent<Tile_Manager>();
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCollider = gameObject.GetComponent<BoxCollider>();
        tileManager = controlScripts.GetComponent<Tile_Manager>();
        JoystickScript = GameObject.FindWithTag("joystick").GetComponent<Joystick>();
        hp = 6;
    }

    private void FixedUpdate()
    {
        if(photonView.isMine)
        {
            Countdown();

            playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);

            Vector3 vectorOfMovement = JoystickScript.Input;

            Movement(vectorOfMovement);
       
            if (JoystickScript.Input != Vector3.zero)
            {
                transform.rotation = Turn();
                playerAnim.SetBool("isWalk", true);
            }
            else
            {
                playerAnim.SetBool("isWalk", false);
            }
        }
    }

    private void Update()
    {
        if(photonView.isMine)
        {
            if(coolDown<=0 )
            {
            coolDownImage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.G))
            DropMyTile();
            
            }

            if(Input.GetKeyDown(KeyCode.Y))
            {   
                AudioManager.auidoInstance.PlaySingleEffectPoint(0,1f);
                Debug.Log("ff");
            }

            Jump();
            
            if(Input.GetKeyDown(KeyCode .J))
            {
		        playerAnim.SetBool("death",true);
            }
            else playerAnim.SetBool("death",false);

            if(hp <= 0)
            {
                GameManagerBase.instance.playersDead.Add(this.gameObject);
			    photonView.RPC("Deactivate", PhotonTargets.All);
            }
        }
    }

    //Khatim health system
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Health" && Input.GetKey(KeyCode.E) && GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp < 6)
        {
            GetComponent<PlayerStats>().healthSprite[gameObject.GetComponent<Character_Controller>().hp].SetActive(true);
            GameManagerBase.instance.myLocalPlayer.GetComponent<Character_Controller>().hp++;
            other.gameObject.SetActive(false);
        }
    }
    #endregion

#region  My Functions

    private void Movement(Vector3 movementvector)
    {
        movementvector.x = movementvector.x * moveSpeed;
        movementvector.z = movementvector.z *moveSpeed;
        movementvector.y = 0f;
        movementvector = Vector3.ClampMagnitude(movementvector, magnitudeToClamp);
        playerBody.AddForce(movementvector, ForceMode.Impulse);
    }

    private Quaternion Turn()
    {
        Quaternion look;
        look= Quaternion.LookRotation(JoystickScript.Input);
        return look;
    }

    private bool IsNotGrounded()
    {
        float groundDistance;
        groundDistance = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 1);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsNotGrounded())
        {
            Debug.Log("jump");
            playerBody.AddForce(new Vector3(0f, jumpPower, 0f),ForceMode.Impulse);
            playerAnim.SetBool("ground",true);
            playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }        
        else playerAnim.SetBool("ground",false);
    }
    
    private void DropMyTile()
    {
        coolDownImage.SetActive(false);
        coolDown = coolDownToSet;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f);
        Debug.Log("shooting");
        
        if(hit.transform.gameObject.tag == "Tile")
        {
            GameObject thisTile = hit.transform.gameObject;
            for(int i=0; i<Tile_Manager.instance.tiles.Count;i++)
            {
                if(thisTile==Tile_Manager.instance.tiles[i].myTile)
                {
                    Debug.Log("HITTTING");
                    myTile=Tile_Manager.instance.tiles[i];
                    //Tile_Manager.instance.CallDropRPC(myTile.myTile.gameObject.name);
                    Tile_Manager.instance.photonView.RPC("CallDropTile", PhotonTargets.All, myTile.myTile.name);
                }
            }
        }
    }
    private void Countdown()
    {
        coolDown -= Time.deltaTime;
    }        
        #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playerAnim.GetBool("isWalk"));
            stream.SendNext(playerAnim.GetBool("ground"));
            stream.SendNext(playerAnim.GetBool("death"));
            //stream.SendNext(hp);
        }
        else
        {
            this.playerAnim.SetBool("isWalk", (bool)stream.ReceiveNext());
            this.playerAnim.SetBool("ground", (bool)stream.ReceiveNext());
            this.playerAnim.SetBool("death", (bool)stream.ReceiveNext());
            //this.hp = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void TakeDamage(int damageToTake)
    {
        hp = hp - damageToTake;
    }

    [PunRPC]
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}