using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GameManagerBase : Photon.MonoBehaviour
{
    public enum GameStates
    {
        Starting,
        Ongoing,
        Ending
    };

    public static GameManagerBase instance;

    private void Awake() 
    {
        instance = this;
    }

    public void Initialize()
    {
        
    }
}
