using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Tile_Manager : MonoBehaviour {

    public float speed;
    public float time;
    public List<GameObject> tiles = new List<GameObject>();
   // public bool canAdd = true;
   // public Transform tile;



    private void Awake()
    {
       
    }


    // Use this for initialization
    void Start () {
       


    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (!tiles.Contains(tile))
            {

                tiles.Add(tile);
                
            }
        }















        /*

        for (int i=0; i<tile.childCount; i++)
        {
            if (i < tile.childCount && canAdd)
            {


                tiles.Add(tile.GetChild(i).transform.gameObject);
            }
            if (i > tile.childCount)
            {
                canAdd = false;
            }
            
            
            
        }
        */
        




    }
}
