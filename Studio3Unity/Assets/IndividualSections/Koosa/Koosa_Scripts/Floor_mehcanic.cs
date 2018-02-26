using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_mehcanic : MonoBehaviour {
    // This script will not be used in the tile manager, its here for refrence. 
    public bool isFalling;
    public Vector3 startPos;
    public float speed;
    public float actionTime;
    public float blockCoolDown;
    public Vector3 stopPos;


	// Use this for initialization
	void Start () {
        startPos = transform.position;
        isFalling = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F))
        {
            StartCoroutine("ground");
        }
	}

    public void Fall()
    {
        if (transform.position != stopPos)
        {
            transform.Translate(0, speed, 0);
        }
    }
    public void Rise()
    {
        if (transform.position != startPos)
        {
            transform.Translate(0, speed, 0);
        }
    }
    IEnumerator Ground()
    {

        yield return new WaitForSeconds(actionTime);
        Fall();
        yield return new WaitForSeconds(blockCoolDown);
        Rise();






    }







}
