﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBT : MonoBehaviour
{
    #region Public Variables
    [HideInInspector] public Node root;
    [HideInInspector] public ZombieStats zom;

    #endregion

    #region Functions
    void Start ()
    {
        zom = GetComponent<ZombieStats>();

        Sequence sequenceNode = new Sequence();
        root = sequenceNode;

        sequenceNode.children.Add(new Chase());
        sequenceNode.children.Add(new Attack());
    }

    void Update ()
    {
        root.Execute(this);
    }
    #endregion

}