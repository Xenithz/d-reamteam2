using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_mehcanic : MonoBehaviour {

    public bool isfalling;
    public Vector3 startpos;
    public float speed;
    public float actiontime;
    public float blockcooldown;
    public Vector3 stoppos;


	// Use this for initialization
	void Start () {
        startpos = transform.position;
        isfalling = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F))
        {
            StartCoroutine("ground");
        }
	}

    public void fall()
    {
        if (transform.position != stoppos)
        {
            transform.Translate(0, speed, 0);
        }
    }
    public void rise()
    {
        if (transform.position != startpos)
        {
            transform.Translate(0, speed, 0);
        }
    }
    IEnumerator ground()
    {

        yield return new WaitForSeconds(actiontime);
        fall();
        yield return new WaitForSeconds(blockcooldown);
        rise();






    }







}
