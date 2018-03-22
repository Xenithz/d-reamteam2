using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile  {


#region Public Varaiables
    public GameObject myTile;
    public float timeToShake=50;
    public int countDownToFall=2;
    public int countDownToRise=2;
     public float delta=0.3f;// how far to move it from its inital postion
    public float speed=60;// how fast the object is to be moved
   public Vector3 startpos;

     public float timeToStartShake=0.05f;
    #endregion
#region My Functions
    public Tile (GameObject tileToPass)
    {
        myTile = tileToPass;
    }
     public void ShakeTile(Tile tileToShake)
    {
        startpos=tileToShake.myTile.transform.position;
        startpos.x+=delta*Mathf.Sin(speed*Time.time);
		tileToShake.myTile.gameObject.transform.position=startpos;
        tileToShake.timeToShake--;
    }
}
#endregion
