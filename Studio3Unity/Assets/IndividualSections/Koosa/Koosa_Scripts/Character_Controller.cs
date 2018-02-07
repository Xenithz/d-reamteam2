using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour {

#region Private Variables 
    private Rigidbody playerbody;
    private BoxCollider playercollider;



    #endregion

#region Public Variables
    public float movespeed;
    public float rotspeed = 100f;
    public float lockrot;
    public float clampmagnitude;
    public float jumpforce;

    #endregion


#region Execution
    private void Start()
    {
        playerbody = GetComponent<Rigidbody>();
        playercollider = GetComponent<BoxCollider>();   
    }


    private void FixedUpdate()
    {
        
        JumpExecution();
        
        transform.rotation = Quaternion.Euler(lockrot, transform.rotation.eulerAngles.y, lockrot);
        Vector3 storageVector = MovementInput();
        
        
        MovementExecution(storageVector);
        // the object will only turn if input is not zero, meanning that there was input recieved from the player, if not then we dont turn.
        if (MovementInput() != Vector3.zero)
        {
            transform.rotation = turn();
        }
        

    }
#endregion




 #region My Functions

    private Vector3 MovementInput()
    {
        Vector3 inputToReturn;
        float horizontalTrack = Input.GetAxisRaw("Horizontal");
        float verticalTrack = Input.GetAxisRaw("Vertical");
        inputToReturn = new Vector3(horizontalTrack, 0f, verticalTrack);
        return inputToReturn;
    }

    //Takes in proccessed input and uses it to move the character using AddForce
    
    private void MovementExecution(Vector3 vectorForMovement)
    {
        vectorForMovement.x = vectorForMovement.x * movespeed;
        vectorForMovement.z = vectorForMovement.z *movespeed;
        vectorForMovement.y = 0f;
        vectorForMovement = Vector3.ClampMagnitude(vectorForMovement, clampmagnitude);
        
       playerbody.AddForce(vectorForMovement, ForceMode.Acceleration);
    }

    private Quaternion turn()
    {
        Quaternion look;
         look= Quaternion.LookRotation(MovementInput());
       
        return look;

    }
    private bool JumpCheck()
    {
        
        float distanceToGround;
        distanceToGround = playercollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround );
        
    }
    private void JumpExecution()
    {
        if (Input.GetKeyDown(KeyCode.Space) && JumpCheck())
        {
           playerbody.constraints =  RigidbodyConstraints.None;
            playerbody.AddForce(new Vector3(0f, jumpforce, 0f), ForceMode.Impulse);
            
        }
        else if(!Input.GetKeyDown(KeyCode.Space)&& JumpCheck())
        {
            playerbody.constraints = RigidbodyConstraints.FreezePositionY;

        }
        
       

       
    }
#endregion
































}

