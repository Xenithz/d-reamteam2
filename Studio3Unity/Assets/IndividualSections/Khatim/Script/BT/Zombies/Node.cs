using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public virtual void Execute(ZombieBT ownerBT)
    {
        Debug.Log("State is Ready");
    }
}
