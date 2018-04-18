using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settting : MonoBehaviour 
{
	public int hp;
	public GameObject[] health;


	// Use this for initialization
	void Start () 
	{

		
	}
	
	// Update is called once per frame
	void Update () 
	{


		//health[hp].SetActive(true);
		health[hp].SetActive(false);
		/* 
		if(Input.GetKey(KeyCode.P))
		{
		hp--;
		}

		for (int i = 0; i < hp; i++)
		{
			health[i].SetActive(true);
		}
		for (int i = health.Length; i < hp; i--)
		{
			health[i].SetActive(false);
			
		}
		*/




	}
}
