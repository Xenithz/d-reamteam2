using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//(typeof(Rigidbody))]
public class OfflineCharacterController : MonoBehaviour {

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
    public OfflineTileManager tileManager;
    public Animator playerAnim;
    public float hp;
	private float inputH;
	private float inputV;
    
    #endregion

#region Unity Functions


    private void Awake()
    {
        playerAnim=GetComponent<Animator>();
        coolDownImage=GameObject.FindGameObjectWithTag("DropAbility");
        coolDown=0;
        GameObject TileManager = GameObject.Find("TileManager");
        //tileManager = TileManager.GetComponent<OfflineTileManager>();
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCollider = gameObject.GetComponent<BoxCollider>();
        //tileManager = TileManager.GetComponent<OfflineTileManager>();
    }

    private void FixedUpdate()
    {
        Countdown();

        playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);

        Vector3 vectorOfMovement = MovementInput();
        Vector3 vectorOfJump=JumpInput();

        Movement(vectorOfMovement);
        if (IsNotGrounded() && vectorOfJump!=Vector3.zero)
        {
        Jump(vectorOfJump);
        playerAnim.SetBool("ground",true);
        }
        else  playerAnim.SetBool("ground",false);
       
        if (vectorOfMovement != Vector3.zero)
        {
			playerAnim.SetBool("isWalk",true);
            transform.rotation = Turn();
            Debug.Log("i am ");
            playerAnim.SetInteger("anim",0);
        }
        else playerAnim.SetBool("isWalk",false);
    }

    private void Update()
    {
     float dropTile=Input.GetAxis("Tile");
        if(coolDown<=0 ){
            coolDownImage.SetActive(true);
        if (dropTile!=0){
         DropMyTile();
        playerAnim.SetInteger("anim",2);
		}
       // else playerAnim.SetInteger("anim",0);
        if(Input.GetKeyDown(KeyCode .J)){
		playerAnim.SetBool("death",true);
        }
        }
        
    }
    #endregion

#region  My Functions
    private Vector3 MovementInput()
    {
        Vector3 playerinput;
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        playerinput = new Vector3(horInput, 0, verInput).normalized;
        return playerinput;
    }

    private void Movement(Vector3 movementvector)
    {
        movementvector.x = movementvector.x * moveSpeed;
        movementvector.z = movementvector.z * moveSpeed;
        movementvector.y = 0f;
        movementvector = Vector3.ClampMagnitude(movementvector, magnitudeToClamp);
        playerBody.AddForce(movementvector, ForceMode.Impulse);
    }

    private Quaternion Turn()
    {
        Quaternion look;
        look= Quaternion.LookRotation(MovementInput());
        return look;
    }

    private bool IsNotGrounded()
    {
        float groundDistance;
        groundDistance = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 1f);
        
    }

    private Vector3 JumpInput()
    {
        Vector3 jumpInput;
        float jump=Input.GetAxis("Jump");
        jumpInput= new Vector3(0,jump,0).normalized;
         return jumpInput;
/* 
        if (IsNotGrounded())
        { 
            playerAnim.SetBool("ground",true);
           

        }
        else playerAnim.SetBool("ground",false);
        /
            Debug.Log("jump");
            playerBody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
			//playerAnim.SetInteger("anim",2);
            playerAnim.SetBool("ground",true);
            Debug.Log("i m getting called");
        }     
        else playerAnim.SetBool("ground",false);
         */
    }
    private void Jump(Vector3 jumpVector)
    
    {
        jumpVector.y=jumpVector.y*jumpPower;
        jumpVector.x=0;
        jumpVector.z=0;
        playerBody.AddForce(jumpVector,ForceMode.Impulse);


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

           StartCoroutine(tileManager.DroppingTile(thisTile));
			Debug.Log("HITTTING");
        }
        
        // if (hit.transform.gameObject.tag == ("Tile") && tileManager.tiles.Contains(myTile))
        // {
        //     Debug.Log("HITTTING");
        //     Tile_Manager.instance.CallDropRPC(myTile.myTile.gameObject.name);
        // }
    }
    private void Countdown()
    {
        coolDown -= Time.deltaTime;
    }        

 }
    #endregion


