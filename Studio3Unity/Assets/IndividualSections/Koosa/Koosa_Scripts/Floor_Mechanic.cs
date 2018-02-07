using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Mechanic : MonoBehaviour {

    public bool fall;

    private Vector3 startpos;
    public bool movesideways;
    public float speedfactor;

    // Use this for initialization
    void Start()
    {


        startpos = transform.position;


        movesideways = false;
        fall = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (movesideways == true)
        {
            shakeground();
        }
        if (fall == true)
        {

            movedownwards();
        }
    }
    public void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // fall = true;
            StartCoroutine("ground");




        }

    }
    public void movedownwards()
    {


        transform.Translate(0, -0.1f, 0);

    }
    public void shakeground()
    {
        transform.position = startpos + new Vector3((Mathf.Sin(Time.time * speedfactor + speedfactor) * 1), 0f, 0f);

    }
    IEnumerator ground()
    {


        yield return new WaitForSeconds(1.5f);
        movesideways = true;



        fall = true;
        yield return new WaitForSeconds(0.5f);
        movesideways = false;




    }
}
