﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class OfflineTileManager : MonoBehaviour {
  
    #region Public Variables
    public static OfflineTileManager instance;
    public float delta;// how far to move it from its inital postion
    public float speed;// how fast the object is to be moved
    public float timeToStartShake;// is time between every shake, every movemnt of the object every change in its position 
    public float timeToShake;// the time we use to continue our loop for the shaking 
    public float countDownToFall;
    public float countDownToRise;
    public GameObject[] temp;
    #endregion Private Variables

    #region Private and Portected Variables
    private Vector3 startpos;
   public List<Tile> tiles = new List<Tile>();
    #endregion
    
    #region Unity Functions
    private void Awake()
    {
		temp = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in temp)
        tiles.Add(new Tile(tile));
        instance = this;
    }
    #endregion 
    
    #region Coroutines
    public IEnumerator DroppingTile(GameObject myTileToDrop)
    { 
        Debug.Log("This gets reached");
        GameObject thisTile;
        thisTile = myTileToDrop;
        Tile myTile= new Tile (thisTile);
        Vector3 defaultpos=myTile.myTile.transform.position;

        while (true)
        {
            for(int i=0; i<myTile.timeToShake; i++)
            {
            myTile.ShakeTile(myTile);
            yield return new WaitForSeconds(myTile.timeToStartShake);
            }
        break;
        }

        yield return new WaitForSeconds(countDownToFall);
        Debug.Log("This gets dropped");
        myTile.myTile.gameObject.SetActive(false);
        yield return new WaitForSeconds(countDownToRise);

        Debug.Log("This gets raised");
        myTile.myTile.transform.position=defaultpos;
        myTile.myTile.GetComponent<Renderer>().material.color=Color.black;
        myTile.myTile.gameObject.SetActive(true);
        myTile.timeToShake=30;
    }
    #endregion
}

