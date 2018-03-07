using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon;

public class Tile_Manager : Photon.MonoBehaviour {
    public float delta;// how far to move it from its inital postion
    public float speed;
    public bool loop=true;
    public int timeToStartShake;
    public float timeToShake;
    public float countDownToFall;
    public float countDownToRise;
    public Vector3 startpos;
    public Vector3 defaultpos;
    public List<GameObject> tiles = new List<GameObject>();
    public bool flagTest;
    private void Awake()
    {
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (!tiles.Contains(tile))
                tiles.Add(tile);
        }
        flagTest = false;
    }
    public void ShakeTile(GameObject tileToShake){
        	startpos.x+=delta*Mathf.Sin(speed*Time.time);
		    tileToShake.transform.position=startpos;
            timeToShake--;
         Debug.Log("i am shaking");
    }
        
    public IEnumerator DroppingTile(string myTileName)
    {
        

        Debug.Log("This gets reached");
        GameObject myTile = GameObject.Find(myTileName);
        flagTest = false;
        startpos=myTile.transform.position;
        defaultpos=myTile.transform.position;
        yield return new WaitForSeconds(timeToStartShake);
      while (true){
          for(int i=0; i<timeToShake; i++){
       ShakeTile(myTile);
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
        timeToShake=15;
    }

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
