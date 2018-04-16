using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyer : MonoBehaviour {
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.GetComponent<ZombieFSM>()==true)
		other.gameObject.SetActive(false);
		if(other.gameObject.GetComponent<Character_Controller>()==true ||other.gameObject.GetComponent<OfflineCharacterController>()==true)
		Destroy(other.gameObject);
		//SceneManager.LoadScene("GameOver");
	}
	
}
