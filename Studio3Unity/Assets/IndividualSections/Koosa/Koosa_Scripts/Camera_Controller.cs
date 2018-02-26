using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

#region Public Variables
    public Transform player;
    
    public Vector3 offset;
    public float smoothing;
    #endregion


#region Private Variables
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    #endregion


#region Unity Functions
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
 
	
	
	void Update () {
       target = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing);

    }
}
#endregion
