using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon;

public class Tile_Manager : Photon.MonoBehaviour {
    public float delta;// how far to move it from its inital rotation
    Quaternion defaultRot;
    public float speed;
    public bool loop=true;
    public Quaternion startRot;
    public float minSpeed;
    public float maxSpeed;
    public float timeToShake;
    public float countDownToFall;
    public float countDownToRise;
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
        startRot.z+=delta*Mathf.Sin(speed*Time.time);
		tileToShake.transform.rotation=startRot;
        timeToShake-=1;
         Debug.Log("i am shaking");
        Debug.Log(tileToShake.transform.rotation);
    }
        
    public IEnumerator DroppingTile(string myTileName)
    {
        

        Debug.Log("This gets reached");
        GameObject myTile = GameObject.Find(myTileName);
        flagTest = false;
        startRot=myTile.transform.rotation;
        defaultRot=myTile.transform.rotation;
      while (true){


          for(int i=0; i<timeToShake; i++){
       ShakeTile(myTile);
       yield return new WaitForSeconds(1);
          }
          break;
      }
       

      
     
        yield return new WaitForSeconds(countDownToFall);
        Debug.Log("This gets dropped");
        myTile.gameObject.SetActive(false);
        yield return new WaitForSeconds(countDownToRise);
        Debug.Log("This gets raised");
        myTile.transform.rotation=defaultRot;
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
