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
		inputH=Input.GetAxisRaw("Horizontal");
		inputV= Input.GetAxisRaw("Vertical");
		playerAnim.SetFloat("inputH",inputH);
		playerAnim.SetFloat("inputV",inputV);

        Countdown();

        playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, lockRot);

        Vector3 vectorOfMovement = MovementInput();

        Movement(vectorOfMovement);
       
        if (MovementInput() != Vector3.zero)
        {
			playerAnim.SetInteger("int",1);
            transform.rotation = Turn();
        }
		else
		playerAnim.SetInteger("int",0);
    }

    private void Update()
    {
		if(Input.GetKeyDown(KeyCode .J))
		playerAnim.SetInteger("int",5);
        if(coolDown<=0 ){
            coolDownImage.SetActive(true);
      
        if (Input.GetKeyDown(KeyCode.G)){
         DropMyTile();
		 playerAnim.SetInteger("int",2);
		}
        }
		else
		playerAnim.SetInteger("int",0);
        if(Input.GetKeyDown(KeyCode.Y)){
            AudioManager.auidoInstance.PlaySingleEffectPoint(0,1f);
            Debug.Log("ff");
            
        }
        Jump();

        
    }
    #endregion

#region  My Functions
    private Vector3 MovementInput()
    {
        Vector3 playerinput;
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        playerinput = new Vector3(horInput, 0f, verInput).normalized;
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
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 1);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsNotGrounded())
        {
            Debug.Log("jump");
            playerBody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
			playerAnim.SetInteger("int",3);
        }        
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


