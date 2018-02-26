using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour {

#region Private Variables 
    [SerializeField]
    private Rigidbody playerBody;
    private BoxCollider playerCollider;
    private RaycastHit hit;
    private GameObject myTile;
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
        GameObject controlScripts = GameObject.Find("ControlScripts");
        tileManager = controlScripts.GetComponent<Tile_Manager>();
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();   
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //    DropMyTile();

        //Jump();

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
        if (Input.GetKeyDown(KeyCode.G))
            DropMyTile();

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
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance+2f);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsNotGrounded())
        {
            
         playerBody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
            
        }        
    }
    private void DropMyTile()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f);
       
        myTile = hit.transform.gameObject;
        if (hit.transform.gameObject.tag == ("Tile") && tileManager.tiles.Contains(myTile)) 
        {
            Debug.Log("HITTTING");
            tileManager.CallDropRPC(hit.transform.gameObject.name);

        }
    }





    #endregion
}

