using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//Used to determine what sort of buttons are currently being pressed
//Allows for normalisation of movement

public class KeyController : MonoBehaviour
{
    public int numberOfButtonsDown;
    public bool wDown;
    public bool aDown;
    public bool sDown;
    public bool dDown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            wDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            aDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            sDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            dDown = false;
            numberOfButtonsDown -= 1;
        }
    }
}
