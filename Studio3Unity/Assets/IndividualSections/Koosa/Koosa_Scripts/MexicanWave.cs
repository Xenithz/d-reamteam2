using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MexicanWave : MonoBehaviour {
public Color[] colors=new Color[6];
 public Renderer rend;
 public Material objMaterial;
 public float time;
 public int index;
  public List<GameObject> mexican=new List<GameObject>();
	// Use this for initialization
	void Start () {

         colors[0] = Color.cyan;
         colors[1] = Color.red;
         colors[2] = Color.green;
         colors[3] = new Color(255, 165, 0);
         colors[4] = Color.yellow;
         colors[5] = Color.magenta;
		rend=GetComponent<Renderer>();
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("mexican"))
		{
			mexican.Add(obj);
			Debug.Log(obj.name);

		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		time+=Time.deltaTime;
		
		if(Input.GetKeyDown(KeyCode.G )&& time<=10){
		
			mexican[index].GetComponent<Renderer>().material.color=colors[Random.Range(0,5)];
			time=0;
			index++;
		
		}
		
	}
}
