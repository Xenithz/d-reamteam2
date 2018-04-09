using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

	public float delta;
    public float speed;
	   Vector3 startpos;
	 public Color shakeColor=Color.green;
	public Color shakeColor2=Color.red;
	public GameObject me;
	public float speedColour;
	public float t;
	public float deltaColour;
		// Use this for initialization
	void Start ()
	 {
		me=this.gameObject;
		startpos=transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Mathf.Clamp(t,-0.5f,0.5f);
        t+= deltaColour*Mathf.Sin(Time.time * speedColour);
		me.GetComponent<Renderer>().material.color=Color.Lerp(shakeColor,shakeColor2,t);
		startpos.x+=delta*Mathf.Sin(speed*Time.time);
		transform.position=startpos;
	}
}
