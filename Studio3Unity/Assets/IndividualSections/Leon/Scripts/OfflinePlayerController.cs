using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OfflinePlayerController : MonoBehaviour
{

    #region Private Variables 
    private Rigidbody playerbody;
    private BoxCollider playercollider;



    #endregion



    #region Public Variables
    public float movespeed;
    public float lockrot;
    public float magnitudetoclamp;
    public float jumppower;
    public float RBvelocitytoclamp;



    #endregion



    #region Execution
    private void Start()
    {
        playerbody = GetComponent<Rigidbody>();
        playercollider = GetComponent<BoxCollider>();
    }








    //To prevent the player from sliding




    private void FixedUpdate()
    {
        /*Debug.Log(playerbody.velocity.magnitude);
        Debug.Log(MovementInput());*/

        Jump();


        transform.rotation = Quaternion.Euler(lockrot, transform.rotation.eulerAngles.y, lockrot);
        Vector3 Vectorofmovement = MovementInput();



        Movement(Vectorofmovement);
        // the object will only turn if input is not zero, meanning that there was input recieved from the player, if not then we dont turn.
        if (MovementInput() != Vector3.zero)
        {
            transform.rotation = turn();
        }
        /*
        if (isnotgrounded())
        {
            playerbody.constraints = RigidbodyConstraints.None;

        }
        */


    }
    #endregion



    #region  Functions

    private Vector3 MovementInput()
    {
        Vector3 playerinput;
        float Horinput = Input.GetAxisRaw("Joystick Horizontal");
        float Verinput = Input.GetAxisRaw("Joystick Vertical");
        playerinput = new Vector3(Horinput, 0f, Verinput);
        return playerinput;
    }

    //Takes in proccessed input and uses it to move the character using AddForce












    private void Movement(Vector3 movementvector)
    {
        movementvector.x = movementvector.x * movespeed;
        movementvector.z = movementvector.z * movespeed;
        movementvector.y = 0f;
        movementvector = Vector3.ClampMagnitude(movementvector, magnitudetoclamp);

        playerbody.AddForce(movementvector, ForceMode.Impulse);
    }

















    private Quaternion turn()
    {
        Quaternion look;
        look = Quaternion.LookRotation(MovementInput());

        return look;

    }
    private bool isnotgrounded()
    {

        float grounddistance;
        grounddistance = playercollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, grounddistance);

    }
    private void Jump()
    {

        if (Input.GetAxis("Jump") != 0 && isnotgrounded())
        {
            // playerbody.constraints =  RigidbodyConstraints.None;
            playerbody.AddForce(new Vector3(0f, jumppower, 0f), ForceMode.Impulse);

        }
        else if (Input.GetAxis("Jump") == 0 && isnotgrounded())
        {
            // playerbody.constraints = RigidbodyConstraints.FreezePositionY;

        }




    }
    #endregion

}
