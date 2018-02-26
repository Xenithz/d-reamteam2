using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Tile_Manager : MonoBehaviour {

    public float countDownToFall;
    public float countDownToRise;
    public GameObject tileToDtrop;
   public  float fallPos;
    public float risePos;
    public List<GameObject> tiles = new List<GameObject>();
    





    void Update()
    {
     

       


       
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            
            if (!tiles.Contains(tile))
            

                tiles.Add(  tile);

            
        }
    }
    /*
   public   void DropTile()
    {
        StartCoroutine(DroppingTile( tileToDtrop));
    }
    */

   public IEnumerator DroppingTile(GameObject myTile)
    {




        yield return new WaitForSeconds(countDownToFall);

        //if (myTile.transform.position.y >= fallPos)

        myTile.gameObject.SetActive(false);

        //myTile.transform.Translate(0, fallPos, 0);

        yield return new WaitForSeconds(countDownToRise);

        //  if (myTile.transform.position.y <= risePos)
        myTile.gameObject.SetActive(true);
          // myTile.transform.Translate(0, risePos, 0);
    }
















    /*

    for (int i=0; i<tile.childCount; i++)
    {
        if (i < tile.childCount && canAdd)
        {


            tiles.Add(tile.GetChild(i).transform.gameObject);
        }
        if (i > tile.childCount)
        {
            canAdd = false;
        }



    }
    */






}
