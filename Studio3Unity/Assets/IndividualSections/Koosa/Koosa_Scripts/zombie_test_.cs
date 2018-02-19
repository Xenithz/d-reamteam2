using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_test_ : MonoBehaviour
{
#region Public Variables
    public float speed;
    public float recycleTime;
    #endregion



#region Private Variables
    private GameObject player;
    #endregion



#region  Unity Functions

    private void Awake()
    {


        player = GameObject.FindWithTag("Player");

    }

    private void OnEnable()
    {
        StartCoroutine("ZombieCou");
        //Invoke("recycle", recycletime);
    }
   
    private void OnDisable()
    {
        StopCoroutine("ZombieCou");
        //CancelInvoke();
    }


    // Update is called once per frame
    void Update()
    {

        // transform.Translate(0, 0, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);

    

    }
    #endregion



#region My Functions
    private void Recycle()
    {
        gameObject.SetActive(false);

    }

    #endregion



#region IEnumerators
    IEnumerator ZombieCou()
    {
        yield return new WaitForSeconds(recycleTime);
        Recycle();
    }



    
}
#endregion



