using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatorcontrol : MonoBehaviour {


public Animator playerAnime;
public float inputH;
public float inputV;
public bool jump;
public bool move;
public bool dismantle;
public Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	inputH=Input.GetAxis("Horizontal");
	 inputV=Input.GetAxis("Vertical");

     if(Input.GetKeyDown(KeyCode.G))
	 {
		playerAnime.SetInteger("anim",3);
	 }
	if(Input.GetKeyDown(KeyCode.Space))
	 {
		 playerAnime.SetInteger("anim",2);
	 }
	  if(rb.velocity!=Vector3.zero)
	 {
		playerAnime.SetInteger("anim",5);
	 }
	  if(rb.velocity==Vector3.zero) 
	 playerAnime.SetInteger("anim",1);

	 
	 
		 
	 





















		
	}
}
