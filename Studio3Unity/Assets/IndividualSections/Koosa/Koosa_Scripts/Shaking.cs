using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour {
public float delta;
public float speed;
Vector3 startpos;
	// Use this for initialization
	void Start ()
	 {
		 startpos=transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	//	speed=Random.Range(minSpeed,maxSpeed);
		
		startpos.x+=delta*Mathf.Sin(speed*Time.time);
		transform.position=startpos;

		
	}
	
}
