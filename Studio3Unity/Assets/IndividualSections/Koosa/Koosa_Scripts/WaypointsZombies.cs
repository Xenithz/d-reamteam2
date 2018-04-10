using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsZombies : MonoBehaviour 
{
#region Public Variables
	public GameObject[] waypoints;
    public float speed;
	#endregion

#region Private Variables
	private int target;
	#endregion

#region Unity Functions

	// Use this for initialization
	void Start ()
    {
        target = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, waypoints[target].transform.position) < 1 && target < waypoints.Length - 1)
        target++;
        else
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].transform.position, speed);
	}
}
#endregion