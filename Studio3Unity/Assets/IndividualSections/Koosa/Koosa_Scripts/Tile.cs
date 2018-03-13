using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile  {


#region Public Varaiables
    public GameObject myTile;
    public Transform defaultTransform;
    #endregion


#region My Functions



    public Tile (GameObject tileToPass)
    {
        myTile = tileToPass;
        defaultTransform.position=myTile.transform.position;
    }



   
	
	
}
#endregion
