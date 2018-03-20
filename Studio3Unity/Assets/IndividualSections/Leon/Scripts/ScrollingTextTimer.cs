using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollingTextTimer : MonoBehaviour {
    public float scrollingTextTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scrollingTextTimer -= Time.deltaTime;
        if (scrollingTextTimer <= 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    
}
