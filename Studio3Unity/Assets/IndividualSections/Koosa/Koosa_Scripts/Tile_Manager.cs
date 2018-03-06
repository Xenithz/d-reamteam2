using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon;

public class Tile_Manager : Photon.MonoBehaviour {
    // i suck
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
        
    public IEnumerator DroppingTile(string myTileName)
    {
        Debug.Log("This gets reached");
        GameObject myTile = GameObject.Find(myTileName);
        flagTest = false;
        yield return new WaitForSeconds(countDownToFall);
        Debug.Log("This gets dropped");
        myTile.gameObject.SetActive(false);
        yield return new WaitForSeconds(countDownToRise);
        Debug.Log("This gets raised");
        myTile.gameObject.SetActive(true);
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
