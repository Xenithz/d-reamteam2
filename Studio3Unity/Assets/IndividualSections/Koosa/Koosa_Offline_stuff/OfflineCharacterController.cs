using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OfflineCharacterController : MonoBehaviour
{
    #region Private Variables 
    private Rigidbody playerBody;
    private Collider playerCollider;
    private RaycastHit hit;
    private Tile myTile;
    private float coolDown;
    private OfflinePlayerStats offlinePlyStats;
    private GameObject coolDownImage;
    private Animator playerAnim;
    private float inputH;
    private float inputV;
    #endregion

    #region Public Variables
    public float moveSpeed;
    public float lockRot;
    public float magnitudeToClamp;
    public float jumpPower;
    public OfflineTileManager tileManager;
    public float coolDownToSet;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        coolDownImage = GameObject.FindGameObjectWithTag("DropAbility");
        coolDown = 0;
        GameObject TileManager = GameObject.Find("TileManager"); ;
        playerBody = gameObject.GetComponent<Rigidbody>();
        playerCollider = gameObject.GetComponent<BoxCollider>();

        offlinePlyStats = GameObject.FindGameObjectWithTag("OfflineStats").GetComponent<OfflinePlayerStats>();
    }
    void FixedUpdate()
    {
        Countdown();

        playerBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);

        Vector3 vectorOfMovement = MovementInput();
        Vector3 vectorOfJump = JumpInput();

        Movement(vectorOfMovement);
        if (IsNotGrounded() && vectorOfJump != Vector3.zero)
        {
            Jump(vectorOfJump);
            playerAnim.SetBool("ground", true);
        }
        else playerAnim.SetBool("ground", false);

        if (vectorOfMovement != Vector3.zero)
        {
            playerAnim.SetBool("isWalk", true);
            transform.rotation = Turn();
            playerAnim.SetInteger("anim", 0);
        }
        else playerAnim.SetBool("isWalk", false);

        float dropTile = Input.GetAxis("Joystick Tile");
        if (coolDown <= 0)
        {
            coolDownImage.SetActive(true);
            if (dropTile != 0)
            {
                DropMyTile();
                playerAnim.SetInteger("anim", 2);
                AudioManager.auidoInstance.PlaySFX(AudioManager.auidoInstance.effectSource, 10, 0, 0.8f, AudioManager.auidoInstance.effectClips);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerAnim.SetBool("death", true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealthPickup" && offlinePlyStats.healthP1 < 6)
        {
            offlinePlyStats.HealthGained(1);
            other.gameObject.SetActive(false);
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

    void Movement(Vector3 movementvector)
    {
        movementvector.x = movementvector.x * moveSpeed;
        movementvector.z = movementvector.z * moveSpeed;
        movementvector.y = 0f;
        movementvector = Vector3.ClampMagnitude(movementvector, magnitudeToClamp);
        playerBody.AddForce(movementvector, ForceMode.Impulse);
    }

    Quaternion Turn()
    {
        Quaternion look;
        look = Quaternion.LookRotation(MovementInput());
        return look;
    }

    bool IsNotGrounded()
    {
        float groundDistance;
        groundDistance = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 1f);
    }

    Vector3 JumpInput()
    {
        Vector3 jumpInput;
        float jump = Input.GetAxis("Joystick Jump");
        jumpInput = new Vector3(0, jump, 0).normalized;
        return jumpInput;
    }
    void Jump(Vector3 jumpVector)
    {
<<<<<<< HEAD
        jumpVector.y=jumpVector.y*jumpPower;
        jumpVector.x=0;
        jumpVector.z=0;
        playerBody.AddForce(jumpVector,ForceMode.Impulse);
        //playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
=======
        jumpVector.y = jumpVector.y * jumpPower;
        jumpVector.x = 0;
        jumpVector.z = 0;
        playerBody.AddForce(jumpVector, ForceMode.Impulse);
        playerBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
>>>>>>> bd557fa1b487ffbc5fb44bbabe86ab25eba9be72
    }

    void DropMyTile()
    {
        AudioManager.auidoInstance.Playeffect(5);
        coolDownImage.SetActive(false);
        coolDown = coolDownToSet;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100f);
        Debug.Log("shooting");

        if (hit.transform.gameObject.tag == "Tile")
        {
            GameObject thisTile = hit.transform.gameObject;
            StartCoroutine(tileManager.DroppingTile(thisTile));
            Debug.Log("HITTTING");
        }
    }
    void Countdown()
    {
        coolDown -= Time.deltaTime;
    }
}
#endregion


