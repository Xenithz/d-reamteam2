using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyer : MonoBehaviour 
{
	public TutorialHandler tutorial;
	void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.GetComponent<ZombieFSM>() || other.gameObject.GetComponent<OfflineZombieFSM>())
		{
            Destroy(other.gameObject);
			tutorial.zombieCount--;
		}
		if(other.gameObject.GetComponent<Character_Controller>() || other.gameObject.GetComponent<OfflineCharacterController>())
		Destroy(other.gameObject);
		SceneManager.LoadScene("Game Over");
		

	}
}
