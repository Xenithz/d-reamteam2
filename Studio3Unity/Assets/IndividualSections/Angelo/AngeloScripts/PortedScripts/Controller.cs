using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public Joystick JoystickScript;
    public Vector3 MovementDirection;
    public float speed;


    public void Update()
    {

        MovementDirection = Vector3.zero;

        if (JoystickScript.Input != Vector3.zero)
        {
            //MovementDirection = JoystickScript.Input;
            //transform.position = new Vector3(+JoystickScript.Input.x, 1, +JoystickScript.Input.z);
            transform.position = transform.position + (JoystickScript.Input * speed);
        }
    }
}