using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Tile_Manager : MonoBehaviour {

    public float speed;
    public float countDownToFall;
    public float countDownToRise;
   // public Vector3 fallPos;
   // public Vector3 risePos;
    public GameObject tileToDtrop;
   public  float min;
    public float max;
    public List<Tile> tiles = new List<Tile>();
    // public bool canAdd = true;
    // public Transform tile;
    public GameObject[] tempTilesHolder;





    void Update()
    {
     

        if (Input.GetKeyDown(KeyCode.G) )
        {
            DropTile();
            Debug.Log("started");
        }


        tempTilesHolder = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tempTilesHolder)
        {
            
           // if (!tiles.Contains(tile))
            

                tiles.Add( new Tile( tile));

            
        }
    }
     void DropTile()
    {
        StartCoroutine(DroppingTile( tileToDtrop));
    }

    IEnumerator DroppingTile(GameObject myTile)
    {
        yield return new WaitForSeconds(countDownToFall);
        if (transform.position.y >= min)
          myTile.  transform.Translate(Vector3.down* speed * Time.deltaTime);

        
        yield return new WaitForSeconds(countDownToRise);

        if (transform.position.y >= max)
           myTile. transform.Translate(Vector3.up * speed * Time.deltaTime);

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
