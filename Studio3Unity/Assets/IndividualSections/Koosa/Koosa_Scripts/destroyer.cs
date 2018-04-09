using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour {
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="Zombie")
		other.gameObject.SetActive(false);
	}
}
