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
    }
   
    private void OnDisable()
    {
        StopCoroutine("ZombieCou");
    }

    void Update()
    {  
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



