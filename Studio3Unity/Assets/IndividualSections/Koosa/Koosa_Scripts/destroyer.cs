using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour 
{
	public TutorialHandler tutorial;
	void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.tag == "zombie1")
            Destroy(other.gameObject);
	}
}
