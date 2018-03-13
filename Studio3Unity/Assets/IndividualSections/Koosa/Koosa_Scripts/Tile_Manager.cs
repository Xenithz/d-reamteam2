using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon;

public class Tile_Manager : Photon.MonoBehaviour {
  
    #region Public Variables
    public static Tile_Manager _instance_;
    public float delta;// how far to move it from its inital postion
    public float speed;// how fast the object is to be moved
    public float timeToStartShake;// is time between every shake, every movemnt of the object every change in its position 
    public float timeToShake;// the time we use to continue our loop for the shaking 
    public float countDownToFall;
    public float countDownToRise;
    public bool flagTest;
    public GameObject[] temp;
    #endregion Private Variables

    #region Private and Portected Variables
    private Vector3 startpos;
    private Vector3 defaultpos;
   protected internal List<GameObject> tiles = new List<GameObject>();
    #endregion
    
    #region Unity Functions
  private void Start()
   {
      // temp = GameObject.FindGameObjectsWithTag("Tile");
   }
    private void Awake()
    {
         _instance_ = this;
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (!tiles.Contains(tile))
                 tiles.Add(tile);
        }
        flagTest = false;
    }
    #endregion 
    
    #region My Functions
    public GameObject ShakeTile(GameObject tileToShake)
    {
        startpos.x+=delta*Mathf.Sin(speed*Time.time);
		tileToShake.transform.position=startpos;
        timeToShake--;
        defaultpos=tileToShake.transform.position;
        return tileToShake;
    }
    #endregion
    
    #region Coroutines
    public IEnumerator DroppingTile(string myTileName)
    {
        Debug.Log("This gets reached");
       GameObject myTile = GameObject.Find(myTileName);
        flagTest = false;
        startpos=myTile.transform.position;
        defaultpos=myTile.transform.position;
        while (true){
        for(int i=0; i<timeToShake; i++){
        ShakeTile(myTile);
        yield return new WaitForSeconds(timeToStartShake);
      }
          break;
      }
        yield return new WaitForSeconds(countDownToFall);
        Debug.Log("This gets dropped");
        myTile.gameObject.SetActive(false);
        yield return new WaitForSeconds(countDownToRise);
        Debug.Log("This gets raised");
        myTile.transform.position=defaultpos;
        myTile.gameObject.SetActive(true);
        timeToShake=50;
    }
    #endregion

    #region RPC
    [PunRPC]
    public void CallDropTile(string inputName)
    {
        StartCoroutine(DroppingTile(inputName));
    }

    public void CallDropRPC(string nameToPass)
    {
        if(flagTest == false)
        {
            flagTest = true;
            photonView.RPC("CallDropTile", PhotonTargets.All, nameToPass);
        }
    }
    
}
#endregion
