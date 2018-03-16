using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile :MonoBehaviour {


#region Public Varaiables
    public GameObject myTile;
    public float time=0.05f;
    #endregion
#region My Functions
    public Tile (GameObject tileToPass)
    {
        myTile = tileToPass;
    }
}
#endregion
