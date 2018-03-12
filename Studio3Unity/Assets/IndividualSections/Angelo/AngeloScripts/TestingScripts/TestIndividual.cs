using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestIndividual : Photon.MonoBehaviour
{
    Character_Controller myCharacter;
    
    private void Awake()
    {
        myCharacter = GetComponent<Character_Controller>();

        if (photonView.isMine)
        {
            myCharacter.enabled = true;
        }
        else
        {
            myCharacter.enabled = false;
        }
    }
}
