using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_test_ : MonoBehaviour
{
    #region Public Variables
    public float speed;
    public float recycletime;
    public GameObject Player;
    

    #endregion

    #region  Functions

    private void Awake()
    {

        Player = GameObject.FindWithTag("Player");

    }

    private void OnEnable()
    {
        Invoke("recycle", recycletime);
    }
    private void recycle()
    {
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        CancelInvoke();
    }


    // Update is called once per frame
    void Update()
    {
        // transform.Translate(0, 0, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed*Time.deltaTime);

    

    }
}
#endregion

