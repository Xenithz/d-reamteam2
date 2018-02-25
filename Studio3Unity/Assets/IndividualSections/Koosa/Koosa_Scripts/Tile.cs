using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


#region Public Varaiables
    public Vector3 position;
    public bool falling;
    #endregion


#region Unity Functions



    void Awake () {
        position = transform.position;
        falling = false;
		
	}
	
	
}
#endregion
