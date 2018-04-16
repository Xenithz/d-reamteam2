using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile  {


#region Public Varaiables
    public GameObject myTile;
    public float timeToShake=50;
    public int countDownToFall=1;
    public int countDownToRise=1;
    public float delta=0.3f;// how far to move it from its inital postion
    public float speed=60;// how fast the object is to be moved
    public Vector3 startpos;
    public Color shakeColor= new Color(10,0,0,0);
	public Color shakeColor2= new Color(10,101,101,0);
	public float speedColour=20;
	public float t;
	public float deltaColour=0.2f;

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
        t+=deltaColour*Mathf.Sin(Time.time * speedColour);
        myTile.GetComponent<Renderer>().material.color=Color.Lerp(shakeColor,shakeColor2,t);
        tileToShake.timeToShake--;
    }
}
#endregion
