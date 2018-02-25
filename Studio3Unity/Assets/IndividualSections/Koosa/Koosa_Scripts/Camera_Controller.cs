using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {


    public Transform player;
    public Vector3 target;
    public Vector3 offset;
    public float smoothing;
    private Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () {
        

		
	}
	
	// Update is called once per frame
	void Update () {
       target = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing);

    }
}
