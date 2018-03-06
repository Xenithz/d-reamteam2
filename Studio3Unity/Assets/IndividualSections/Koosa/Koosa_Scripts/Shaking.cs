using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour {
public float delta;
public float speed;
public Quaternion startRot;
public int minSpeed;
public int maxSpeed;
	// Use this for initialization
	void Start () {
		startRot=transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
		speed=Random.Range(minSpeed,maxSpeed);
		startRot.z+=delta*Mathf.Sin(speed*Time.deltaTime);
		transform.rotation=startRot;
		
	}
	
}
