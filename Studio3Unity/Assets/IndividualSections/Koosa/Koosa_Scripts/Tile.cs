using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile  {


#region Public Varaiables
    public GameObject myTile;
    public float timeToShake=50;
    public int countDownToFall=2;
    public int countDownToRise=2;

     public float timeToStartShake=0.05f;
    #endregion
#region My Functions
    public Tile (GameObject tileToPass)
    {
        myTile = tileToPass;
    }
}
#endregion
