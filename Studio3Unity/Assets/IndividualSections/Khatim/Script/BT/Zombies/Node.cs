using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum Condition { Ready, Failed, Success, Running };
    public Condition currCon = Condition.Ready;

    public List<Node> children = new List<Node>();

    public virtual void Execute(ZombieBT ownerBT)
    {
        Debug.Log("State is Ready");
    }
}
