using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisonavoidancetest : MonoBehaviour {
public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position=Vector3.MoveTowards(transform.position,player.transform.position,2*Time.deltaTime);
		
	}
}
