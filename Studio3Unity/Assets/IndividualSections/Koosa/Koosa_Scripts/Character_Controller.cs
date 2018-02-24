using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour {

#region Private Variables 
    private Rigidbody playerBody;
    private BoxCollider playerCollider;
    #endregion


#region Public Variables
    public float moveSpeed;
    public float lockRot;
    public float magnitudeToClamp;
    public float jumpPower;
    #endregion


#region Unity Functions
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();   
    }

    private void FixedUpdate()
    {
        Jump();

        transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);

        Vector3 vectorOfMovement = MovementInput();

        Movement(vectorOfMovement);
       
        if (MovementInput() != Vector3.zero)
        {
            transform.rotation = Turn();
        }
    }
#endregion


#region  My Functions
    private Vector3 MovementInput()
    {
        Vector3 playerinput;
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        playerinput = new Vector3(horInput, 0f, verInput);
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
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance);
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsNotGrounded())
        {
            
         playerBody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
            
        }        
    }
    #endregion
}

