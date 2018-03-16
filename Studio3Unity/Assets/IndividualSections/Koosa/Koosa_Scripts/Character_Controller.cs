using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour {

#region Private Variables 
    [SerializeField]
    private Rigidbody playerBody;
    [SerializeField]
    private BoxCollider playerCollider;
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
    
    #endregion

#region Unity Functions
    private void Start()
    {
        coolDownImage=GameObject.FindGameObjectWithTag("DropAbility");
        coolDown=0;
        GameObject controlScripts = GameObject.Find("ControlScripts");
        tileManager = controlScripts.GetComponent<Tile_Manager>();
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCollider = gameObject.GetComponent<BoxCollider>();
        tileManager = controlScripts.GetComponent<Tile_Manager>();
    }

    private void FixedUpdate()
    {
        Countdown();

        playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);

        Vector3 vectorOfMovement = MovementInput();

        Movement(vectorOfMovement);
       
        if (MovementInput() != Vector3.zero)
        {
            transform.rotation = Turn();
        }
    }

    private void Update()
    
    {
        if (IsNotGrounded())
        {
            Debug.Log("notgrounded");
        }





        if(coolDown<=0 ){
            coolDownImage.SetActive(true);
      
        if (Input.GetKeyDown(KeyCode.G) )
         DropMyTile();
        }
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
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        playerinput = new Vector3(horInput, 0f, verInput).normalized;
        return playerinput;
    }

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
        }        
    }
    
    private void DropMyTile()
    {
        coolDownImage.SetActive(false);
        coolDown = coolDownToSet;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f);
        Debug.Log("shooting");
        GameObject thisTile =hit.transform.gameObject;
        for(int i=0; i<tileManager.tiles.Count;i++){
            Debug.Log("searching");
           if(thisTile==tileManager.tiles[i].myTile){
               tileManager.tiles[i]=myTile;
               Debug.Log("FOUND!!");
           }
           
        }
        
        if (hit.transform.gameObject.tag == ("Tile") && tileManager.tiles.Contains(myTile))
        {
            Debug.Log("HITTTING");
            tileManager.CallDropRPC(hit.transform.gameObject.name);
        }
    }
    private void Countdown(){
        coolDown -= Time.deltaTime;
    }
   
         
        
     
 }
    #endregion


