using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_test_ : MonoBehaviour
{
    #region Public Variables
    public float speed;
    public float recycletime;
    private GameObject Player;
    

    #endregion

    #region  Unity Functions

    private void Awake()
    {


        Player = GameObject.FindWithTag("Player");

    }

    private void OnEnable()
    {
        StartCoroutine("zombieon");
        //Invoke("recycle", recycletime);
    }
    private void recycle()
    {
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        StopCoroutine("zombieon");
        //CancelInvoke();
    }


    // Update is called once per frame
    void Update()
    {

        // transform.Translate(0, 0, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed*Time.deltaTime);

    

    }
    #region IEnumerators
    IEnumerator zombieon()
    {
        yield return new WaitForSeconds(recycletime);
        recycle();
    }



    #endregion
}
#endregion

